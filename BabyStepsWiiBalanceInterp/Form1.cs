using Microsoft.VisualBasic.Devices;
using System.Text.RegularExpressions;
using System.Timers;
using WiimoteLib;
using InputManager;

namespace BabyStepsWiiBalanceInterp
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer updateTimer = new System.Timers.Timer() { Interval = 50, Enabled = false };
        private System.Timers.Timer wKeyPressTimer = new System.Timers.Timer() { Interval = 25, Enabled = false };

        private Wiimote wiimote = new Wiimote();
        private float maxWeight = 0;
        private enum BalanceState { Center, LeftUp, RightUp }
        private BalanceState currentState = BalanceState.Center;
        private bool footIsUp = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            wKeyPressTimer.Elapsed += WKeyPressTimer_Elapsed;
        }

        private void WKeyPressTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            InputManager.Keyboard.KeyDown(Keys.W);
            InputManager.Keyboard.KeyUp(Keys.W);
        }

        private void UpdateTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var bbState = wiimote.WiimoteState.BalanceBoardState;
            float topLeft = bbState.SensorValuesKg.TopLeft;
            float topRight = bbState.SensorValuesKg.TopRight;
            float bottomLeft = bbState.SensorValuesKg.BottomLeft;
            float bottomRight = bbState.SensorValuesKg.BottomRight;

            float leftWeight = topLeft + bottomLeft;
            float rightWeight = topRight + bottomRight;
            float totalWeight = leftWeight + rightWeight;

            float threshold = 0.55f;
            float balance = (rightWeight - leftWeight) / (totalWeight > 0 ? totalWeight : 1);

            BalanceState newState = BalanceState.Center;
            if (totalWeight > 0)
            {
                if (balance > threshold)
                    newState = BalanceState.LeftUp;
                else if (balance < -threshold)
                    newState = BalanceState.RightUp;
            }

            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateUI(topLeft, topRight, bottomLeft, bottomRight);
                    TransitionToState(newState);

                    if (newState == BalanceState.LeftUp || newState == BalanceState.RightUp)
                    {
                        footIsUp = true;
                        wKeyPressTimer.Start();
                    }
                    else
                    {
                        wKeyPressTimer.Stop();
                    }
                });
            }
            catch (Exception ex)
            {
                // Usually only happens when the window is closed while executing, we don't care, the program is closed.
            }
        }

        private void TransitionToState(BalanceState newState)
        {
            if (newState == currentState)
                return;

            currentState = newState;
            sideLabel.Text = currentState switch
            {
                BalanceState.LeftUp => "Left Leg Up",
                BalanceState.RightUp => "Right Leg Up",
                _ => "Center"
            };

            if (killswitch.Checked)
            {
                InputManager.Mouse.ButtonUp(InputManager.Mouse.MouseKeys.Left);
                InputManager.Mouse.ButtonUp(InputManager.Mouse.MouseKeys.Right);
                return;
            }

            switch (newState)
            {
                case BalanceState.LeftUp:
                    InputManager.Mouse.ButtonUp(InputManager.Mouse.MouseKeys.Right);
                    InputManager.Mouse.ButtonDown(InputManager.Mouse.MouseKeys.Left);
                    break;
                case BalanceState.RightUp:
                    InputManager.Mouse.ButtonUp(InputManager.Mouse.MouseKeys.Left);
                    InputManager.Mouse.ButtonDown(InputManager.Mouse.MouseKeys.Right);
                    break;
                case BalanceState.Center:
                    InputManager.Mouse.ButtonUp(InputManager.Mouse.MouseKeys.Left);
                    InputManager.Mouse.ButtonUp(InputManager.Mouse.MouseKeys.Right);
                    break;
            }
        }

        private void UpdateUI(float tl, float tr, float bl, float br)
        {
            if (tl > maxWeight) maxWeight = tl;
            if (tr > maxWeight) maxWeight = tr;
            if (bl > maxWeight) maxWeight = bl;
            if (br > maxWeight) maxWeight = br;

            if (maxWeight > 0)
            {
                pb1.Value = ClampToProgressBar(tl / maxWeight);
                pb2.Value = ClampToProgressBar(tr / maxWeight);
                pb3.Value = ClampToProgressBar(bl / maxWeight);
                pb4.Value = ClampToProgressBar(br / maxWeight);
            }
        }

        private int ClampToProgressBar(float normalized)
        {
            if (float.IsNaN(normalized) || float.IsInfinity(normalized)) return 0;
            int value = (int)(normalized * 100);
            return Math.Max(0, Math.Min(100, value));
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                var connectedDevices = new WiimoteCollection();
                connectedDevices.FindAllWiimotes();
                for (int i = 0; i < connectedDevices.Count; i++)
                {
                    wiimote = connectedDevices[i];

                    if (connectedDevices.Count > 1)
                    {
                        var devicePathId = new Regex("e_pid&.*?&(.*?)&").Match(wiimote.HIDDevicePath).Groups[1].Value.ToUpper();

                        var response = MessageBox.Show("Connect to HID " + devicePathId + " device " + (i + 1) + " of " + connectedDevices.Count + " ?", "Multiple Wii Devices Found", MessageBoxButtons.YesNoCancel);
                        if (response == DialogResult.Cancel) return;
                        if (response == DialogResult.No) continue;
                    }

                    wiimote.Connect();
                    wiimote.SetReportType(InputReport.IRAccel, false);
                    wiimote.SetLEDs(true, false, false, false);

                    updateTimer.Start();

                    connectButton.Enabled = false;
                    statusLabel.Text = "Status: Connected";
                    statusLabel.Location = new System.Drawing.Point((this.Width / 2) - (statusLabel.Width / 2), statusLabel.Location.Y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void decreaseWSpeed_Click(object sender, EventArgs e)
        {
            if (wKeyPressTimer.Interval <= 1) return;
            wKeyPressTimer.Interval -= 1;
            wKeySpeed_tb.Text = wKeyPressTimer.Interval.ToString();
        }

        private void increaseWSpeed_Click(object sender, EventArgs e)
        {
            wKeyPressTimer.Interval += 1;
            wKeySpeed_tb.Text = wKeyPressTimer.Interval.ToString();
        }

        private void wKeySpeed_tb_TextChanged(object sender, EventArgs e)
        {
            wKeyPressTimer.Interval = int.TryParse(wKeySpeed_tb.Text, out int interval) ? Math.Max(1, interval) : 25;
        }
    }
}
