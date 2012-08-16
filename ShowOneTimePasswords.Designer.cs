namespace KeeOtp
{
    partial class ShowOneTimePasswords
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerUpdateTotp = new System.Windows.Forms.Timer(this.components);
            this.labelOtp = new System.Windows.Forms.Label();
            this.labelRemaining = new System.Windows.Forms.Label();
            this.labelRemainingLabel = new System.Windows.Forms.Label();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelOtp
            // 
            this.labelOtp.AutoSize = true;
            this.labelOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOtp.Location = new System.Drawing.Point(24, 51);
            this.labelOtp.Name = "labelOtp";
            this.labelOtp.Size = new System.Drawing.Size(248, 73);
            this.labelOtp.TabIndex = 0;
            this.labelOtp.Text = "012345";
            // 
            // labelRemaining
            // 
            this.labelRemaining.AutoSize = true;
            this.labelRemaining.Location = new System.Drawing.Point(145, 124);
            this.labelRemaining.Name = "labelRemaining";
            this.labelRemaining.Size = new System.Drawing.Size(35, 13);
            this.labelRemaining.TabIndex = 1;
            this.labelRemaining.Text = "label2";
            // 
            // labelRemainingLabel
            // 
            this.labelRemainingLabel.AutoSize = true;
            this.labelRemainingLabel.Location = new System.Drawing.Point(53, 124);
            this.labelRemainingLabel.Name = "labelRemainingLabel";
            this.labelRemainingLabel.Size = new System.Drawing.Size(86, 13);
            this.labelRemainingLabel.TabIndex = 2;
            this.labelRemainingLabel.Text = "Time Remaining:";
            // 
            // labelInstructions
            // 
            this.labelInstructions.Location = new System.Drawing.Point(31, 9);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(226, 42);
            this.labelInstructions.TabIndex = 3;
            this.labelInstructions.Text = "Enter this code in the verification system.  If problems occur contunually double" +
    " check that the clock on your computer is set precisely.  Be sure to double chec" +
    "k the time zone as well.";
            // 
            // ShowOneTimePasswords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.labelRemainingLabel);
            this.Controls.Add(this.labelRemaining);
            this.Controls.Add(this.labelOtp);
            this.Name = "ShowOneTimePasswords";
            this.Text = "Timed Passwords";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdateTotp;
        private System.Windows.Forms.Label labelOtp;
        private System.Windows.Forms.Label labelRemaining;
        private System.Windows.Forms.Label labelRemainingLabel;
        private System.Windows.Forms.Label labelInstructions;
    }
}