namespace KeeOtp
{
    partial class OtpInformation
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
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.radioButtonSix = new System.Windows.Forms.RadioButton();
            this.labelKey = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.radioButtonEight = new System.Windows.Forms.RadioButton();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonBase32 = new System.Windows.Forms.RadioButton();
            this.radioButtonBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonHex = new System.Windows.Forms.RadioButton();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.checkBoxCustomSettings = new System.Windows.Forms.CheckBox();
            this.radioButtonSha256 = new System.Windows.Forms.RadioButton();
            this.radioButtonSha512 = new System.Windows.Forms.RadioButton();
            this.radioButtonSha1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(31, 85);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(340, 20);
            this.textBoxKey.TabIndex = 0;
            this.textBoxKey.TextChanged += new System.EventHandler(this.textBoxKey_TextChanged);
            // 
            // textBoxStep
            // 
            this.textBoxStep.Location = new System.Drawing.Point(140, 138);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(23, 20);
            this.textBoxStep.TabIndex = 1;
            // 
            // radioButtonSix
            // 
            this.radioButtonSix.AutoSize = true;
            this.radioButtonSix.Location = new System.Drawing.Point(140, 174);
            this.radioButtonSix.Name = "radioButtonSix";
            this.radioButtonSix.Size = new System.Drawing.Size(60, 17);
            this.radioButtonSix.TabIndex = 2;
            this.radioButtonSix.Text = "6 Digits";
            this.radioButtonSix.UseVisualStyleBackColor = true;
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(28, 69);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(25, 13);
            this.labelKey.TabIndex = 3;
            this.labelKey.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time Step (Seconds)";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(79, 174);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(55, 13);
            this.labelSize.TabIndex = 5;
            this.labelSize.Text = "Code Size";
            // 
            // radioButtonEight
            // 
            this.radioButtonEight.AutoSize = true;
            this.radioButtonEight.Location = new System.Drawing.Point(140, 197);
            this.radioButtonEight.Name = "radioButtonEight";
            this.radioButtonEight.Size = new System.Drawing.Size(60, 17);
            this.radioButtonEight.TabIndex = 6;
            this.radioButtonEight.Text = "8 Digits";
            this.radioButtonEight.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(215, 241);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(296, 241);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonBase32
            // 
            this.radioButtonBase32.AutoSize = true;
            this.radioButtonBase32.Checked = true;
            this.radioButtonBase32.Location = new System.Drawing.Point(31, 143);
            this.radioButtonBase32.Name = "radioButtonBase32";
            this.radioButtonBase32.Size = new System.Drawing.Size(64, 17);
            this.radioButtonBase32.TabIndex = 9;
            this.radioButtonBase32.TabStop = true;
            this.radioButtonBase32.Text = "Base 32";
            this.radioButtonBase32.UseVisualStyleBackColor = true;
            this.radioButtonBase32.Visible = false;
            // 
            // radioButtonBase64
            // 
            this.radioButtonBase64.AutoSize = true;
            this.radioButtonBase64.Location = new System.Drawing.Point(31, 166);
            this.radioButtonBase64.Name = "radioButtonBase64";
            this.radioButtonBase64.Size = new System.Drawing.Size(64, 17);
            this.radioButtonBase64.TabIndex = 10;
            this.radioButtonBase64.Text = "Base 64";
            this.radioButtonBase64.UseVisualStyleBackColor = true;
            this.radioButtonBase64.Visible = false;
            // 
            // radioButtonHex
            // 
            this.radioButtonHex.AutoSize = true;
            this.radioButtonHex.Location = new System.Drawing.Point(31, 189);
            this.radioButtonHex.Name = "radioButtonHex";
            this.radioButtonHex.Size = new System.Drawing.Size(44, 17);
            this.radioButtonHex.TabIndex = 11;
            this.radioButtonHex.Text = "Hex";
            this.radioButtonHex.UseVisualStyleBackColor = true;
            this.radioButtonHex.Visible = false;
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, -1);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(383, 58);
            this.pictureBoxBanner.TabIndex = 12;
            this.pictureBoxBanner.TabStop = false;
            // 
            // checkBoxCustomSettings
            // 
            this.checkBoxCustomSettings.AutoSize = true;
            this.checkBoxCustomSettings.Location = new System.Drawing.Point(31, 112);
            this.checkBoxCustomSettings.Name = "checkBoxCustomSettings";
            this.checkBoxCustomSettings.Size = new System.Drawing.Size(124, 17);
            this.checkBoxCustomSettings.TabIndex = 13;
            this.checkBoxCustomSettings.Text = "Use Custom Settings";
            this.checkBoxCustomSettings.UseVisualStyleBackColor = true;
            this.checkBoxCustomSettings.CheckedChanged += new System.EventHandler(this.checkBoxCustomSettings_CheckedChanged);
            // 
            // radioButtonSha256
            // 
            this.radioButtonSha256.AutoSize = true;
            this.radioButtonSha256.Location = new System.Drawing.Point(6, 49);
            this.radioButtonSha256.Name = "radioButtonSha256";
            this.radioButtonSha256.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSha256.TabIndex = 14;
            this.radioButtonSha256.TabStop = true;
            this.radioButtonSha256.Text = "Sha-256";
            this.radioButtonSha256.UseVisualStyleBackColor = true;
            // 
            // radioButtonSha512
            // 
            this.radioButtonSha512.AutoSize = true;
            this.radioButtonSha512.Location = new System.Drawing.Point(6, 72);
            this.radioButtonSha512.Name = "radioButtonSha512";
            this.radioButtonSha512.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSha512.TabIndex = 15;
            this.radioButtonSha512.TabStop = true;
            this.radioButtonSha512.Text = "Sha-512";
            this.radioButtonSha512.UseVisualStyleBackColor = true;
            // 
            // radioButtonSha1
            // 
            this.radioButtonSha1.AutoSize = true;
            this.radioButtonSha1.Location = new System.Drawing.Point(6, 26);
            this.radioButtonSha1.Name = "radioButtonSha1";
            this.radioButtonSha1.Size = new System.Drawing.Size(53, 17);
            this.radioButtonSha1.TabIndex = 16;
            this.radioButtonSha1.TabStop = true;
            this.radioButtonSha1.Text = "Sha-1";
            this.radioButtonSha1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonSha1);
            this.groupBox1.Controls.Add(this.radioButtonSha512);
            this.groupBox1.Controls.Add(this.radioButtonSha256);
            this.groupBox1.Location = new System.Drawing.Point(215, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 102);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hash Algorithm";
            // 
            // OtpInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 276);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxCustomSettings);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.radioButtonHex);
            this.Controls.Add(this.radioButtonBase64);
            this.Controls.Add(this.radioButtonBase32);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.radioButtonEight);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.radioButtonSix);
            this.Controls.Add(this.textBoxStep);
            this.Controls.Add(this.textBoxKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "OtpInformation";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OtpInformation_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.RadioButton radioButtonSix;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.RadioButton radioButtonEight;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonBase32;
        private System.Windows.Forms.RadioButton radioButtonBase64;
        private System.Windows.Forms.RadioButton radioButtonHex;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.CheckBox checkBoxCustomSettings;
        private System.Windows.Forms.RadioButton radioButtonSha256;
        private System.Windows.Forms.RadioButton radioButtonSha512;
        private System.Windows.Forms.RadioButton radioButtonSha1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}