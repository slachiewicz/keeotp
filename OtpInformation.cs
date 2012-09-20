using System;
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
                    MessageBox.Show("Key must be at least 8 characters.  If you are provided with less data then pad it up to 8 characters with '='s");
                    e.Cancel = true;
                    return;
                }

                var size = (this.radioButtonEight.Checked) ? 8 : 6;
                int step;
                
                if (int.TryParse(this.textBoxStep.Text, out step))
                {
                    if (step != 30)
                    {
                        if (step <= 0)
                        {
                            this.textBoxStep.Text = "30";
                            MessageBox.Show("The time step must be a non-zero positive integer.  The standard value is 30.  If you weren't specifically given an alternate value just use 30.");
                            e.Cancel = true;
                            return;
                        }
                        else if (MessageBox.Show("You have selected a non-standard time step.  30 is the standard recommended value.  You should only proceed if you were specifically told to use this time step size.  Do you wish to proceed?", "Non-standard time step size", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    this.textBoxStep.Text = "30";
                    MessageBox.Show("The time step must be a non-zero positive integer.  The standard value is 30.  If you weren't specifically given an alternate value just use 30.");
                    e.Cancel = true;
                    return;
                }

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
