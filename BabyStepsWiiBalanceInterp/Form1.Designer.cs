namespace BabyStepsWiiBalanceInterp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            connectButton = new Button();
            statusLabel = new Label();
            pb1 = new ProgressBar();
            pb2 = new ProgressBar();
            pb3 = new ProgressBar();
            pb4 = new ProgressBar();
            progressPanel = new Panel();
            sideLabel = new Label();
            killswitch = new CheckBox();
            decreaseWSpeed = new Button();
            wKeySpeed_tb = new TextBox();
            increaseWSpeed = new Button();
            progressPanel.SuspendLayout();
            SuspendLayout();
            // 
            // connectButton
            // 
            connectButton.Location = new Point(12, 12);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(390, 41);
            connectButton.TabIndex = 0;
            connectButton.Text = "Connect to Wii Balance Board";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(152, 56);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(126, 15);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Status: Not Connected";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pb1
            // 
            pb1.Location = new Point(3, 3);
            pb1.Name = "pb1";
            pb1.Size = new Size(50, 50);
            pb1.TabIndex = 2;
            // 
            // pb2
            // 
            pb2.Location = new Point(59, 3);
            pb2.Name = "pb2";
            pb2.Size = new Size(50, 50);
            pb2.TabIndex = 3;
            // 
            // pb3
            // 
            pb3.Location = new Point(3, 59);
            pb3.Name = "pb3";
            pb3.Size = new Size(50, 50);
            pb3.TabIndex = 4;
            // 
            // pb4
            // 
            pb4.Location = new Point(59, 59);
            pb4.Name = "pb4";
            pb4.Size = new Size(50, 50);
            pb4.TabIndex = 5;
            // 
            // progressPanel
            // 
            progressPanel.Controls.Add(pb1);
            progressPanel.Controls.Add(pb4);
            progressPanel.Controls.Add(pb2);
            progressPanel.Controls.Add(pb3);
            progressPanel.Location = new Point(12, 59);
            progressPanel.Name = "progressPanel";
            progressPanel.Size = new Size(112, 112);
            progressPanel.TabIndex = 6;
            // 
            // sideLabel
            // 
            sideLabel.AutoSize = true;
            sideLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sideLabel.Location = new Point(130, 71);
            sideLabel.Name = "sideLabel";
            sideLabel.Size = new Size(89, 32);
            sideLabel.TabIndex = 7;
            sideLabel.Text = "Center";
            sideLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // killswitch
            // 
            killswitch.AutoSize = true;
            killswitch.Checked = true;
            killswitch.CheckState = CheckState.Checked;
            killswitch.Location = new Point(130, 149);
            killswitch.Name = "killswitch";
            killswitch.Size = new Size(153, 19);
            killswitch.TabIndex = 9;
            killswitch.Text = "Game Control Killswitch";
            killswitch.UseVisualStyleBackColor = true;
            // 
            // decreaseWSpeed
            // 
            decreaseWSpeed.Location = new Point(130, 106);
            decreaseWSpeed.Name = "decreaseWSpeed";
            decreaseWSpeed.Size = new Size(27, 28);
            decreaseWSpeed.TabIndex = 10;
            decreaseWSpeed.Text = "-";
            decreaseWSpeed.UseVisualStyleBackColor = true;
            decreaseWSpeed.Click += decreaseWSpeed_Click;
            // 
            // wKeySpeed_tb
            // 
            wKeySpeed_tb.Location = new Point(162, 108);
            wKeySpeed_tb.Name = "wKeySpeed_tb";
            wKeySpeed_tb.Size = new Size(57, 23);
            wKeySpeed_tb.TabIndex = 11;
            wKeySpeed_tb.Text = "25";
            wKeySpeed_tb.TextAlign = HorizontalAlignment.Center;
            wKeySpeed_tb.TextChanged += wKeySpeed_tb_TextChanged;
            // 
            // increaseWSpeed
            // 
            increaseWSpeed.Location = new Point(225, 106);
            increaseWSpeed.Name = "increaseWSpeed";
            increaseWSpeed.Size = new Size(27, 28);
            increaseWSpeed.TabIndex = 12;
            increaseWSpeed.Text = "+";
            increaseWSpeed.UseVisualStyleBackColor = true;
            increaseWSpeed.Click += increaseWSpeed_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(414, 182);
            Controls.Add(increaseWSpeed);
            Controls.Add(wKeySpeed_tb);
            Controls.Add(decreaseWSpeed);
            Controls.Add(killswitch);
            Controls.Add(sideLabel);
            Controls.Add(progressPanel);
            Controls.Add(statusLabel);
            Controls.Add(connectButton);
            Name = "Form1";
            Text = "Wii Balance Board for Baby Steps";
            TopMost = true;
            Load += Form1_Load;
            progressPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected Button connectButton;
        private Label statusLabel;
        private ProgressBar pb1;
        private ProgressBar pb2;
        private ProgressBar pb3;
        private ProgressBar pb4;
        private Panel progressPanel;
        private Label sideLabel;
        private CheckBox killswitch;
        private Button decreaseWSpeed;
        private TextBox wKeySpeed_tb;
        private Button increaseWSpeed;
    }
}
