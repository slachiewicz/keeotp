using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OtpSharp;

namespace KeeOtp
{
    public partial class OtpInformation : Form
    {
        const string validKeyChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        public OtpAuthData Data { get; set; }

        public OtpInformation()
        {
            InitializeComponent();
            this.Shown += (sender, e) => FormWasShown();
        }

        private void FormWasShown()
        {
            if (this.Data != null)
            {
                this.textBoxKey.Text = Base32.Encode(this.Data.Key);
                this.textBoxStep.Text = this.Data.Step.ToString();

                if (this.Data.Size == 8)
                {
                    this.radioButtonSix.Checked = false;
                    this.radioButtonEight.Checked = true;
                }
                else
                {
                    this.radioButtonSix.Checked = true;
                    this.radioButtonEight.Checked = false;
                }
            }
            else
                this.textBoxStep.Text = "30";
        }

        private void OtpInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            try
            {
                if (string.IsNullOrEmpty(this.textBoxKey.Text))
                {
                    MessageBox.Show("A key must be set");
                    e.Cancel = true;
                    return;
                }

                if (this.textBoxKey.Text.Length < 8)
                {
                    MessageBox.Show("Key must be at least 8 characters.  If you are provided less data then fill it out with '='s");
                    e.Cancel = true;
                    return;
                }

                var size = (this.radioButtonEight.Checked) ? 8 : 6;
                var step = int.Parse(this.textBoxStep.Text);
                var key = Base32.Decode(this.textBoxKey.Text);

                this.Data = new OtpAuthData()
                {
                    Key = key,
                    Size = size,
                    Step = step
                };
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void textBoxStep_TextChanged(object sender, EventArgs e)
        {
            var text = this.textBoxStep.Text;
            int value;
            if (!int.TryParse(text, out value))
                this.textBoxStep.Text = "30";
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {
            var unpaddedBase32 = this.textBoxKey.Text.ToUpperInvariant().TrimEnd('=');

            bool invalid = false;
            foreach (var c in unpaddedBase32)
            {
                if (validKeyChars.IndexOf(c) < 0)
                {
                    invalid = true;
                    break;
                }
            }

            if (invalid)
            {
                if (this.textBoxKey.Text.Length > 1)
                    this.textBoxKey.Text = this.textBoxKey.Text.Substring(0, this.textBoxKey.Text.Length - 1);
                else
                    this.textBoxKey.Text = string.Empty;
            }
        }
    }
}
