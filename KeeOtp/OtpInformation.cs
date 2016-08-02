using System;
using System.Windows.Forms;
using KeePass.Plugins;
using OtpSharp;

namespace KeeOtp
{
    public partial class OtpInformation : Form
    {
        const string validKeyChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        public OtpAuthData Data { get; set; }
        bool fullyLoaded = false;

        public OtpInformation(IPluginHost host)
        {
            InitializeComponent();
            this.Shown += (sender, e) => FormWasShown();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.lock_key.GetThumbnailImage(32, 32, null, IntPtr.Zero),
                "Configuration",
                "Set up the key for generating one time passwords");

            this.Icon = host.MainWindow.Icon;
        }

        private void FormWasShown()
        {
            if (this.Data != null)
            {
                this.Data.Key.UsePlainKey(key => this.textBoxKey.Text = Base32.Encode(key));
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

                if (this.Data.Size != 6 || this.Data.Step != 30)
                {
                    this.checkBoxCustomSettings.Checked = true;
                }

                if (this.Data.OtpHashMode == OtpHashMode.Sha256)
                {
                    this.radioButtonSha1.Checked = false;
                    this.radioButtonSha256.Checked = true;
                    this.radioButtonSha512.Checked = false;
                }
                else if (this.Data.OtpHashMode == OtpHashMode.Sha512)
                {
                    this.radioButtonSha1.Checked = false;
                    this.radioButtonSha256.Checked = false;
                    this.radioButtonSha512.Checked = true;
                }
                else
                {
                    this.radioButtonSha1.Checked = true;
                    this.radioButtonSha256.Checked = false;
                    this.radioButtonSha512.Checked = false;
                }
            }
            else
            {
                this.textBoxStep.Text = "30";
                this.radioButtonSha1.Checked = true;
                this.radioButtonSha256.Checked = false;
                this.radioButtonSha512.Checked = false;
            }

            SetCustomSettingsState(false);
            this.fullyLoaded = true;
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

                // set the default settings
                int size = 6;
                int step = 30;
                OtpHashMode hashMode = OtpHashMode.Sha1;
                Key key = null;

                if (this.checkBoxCustomSettings.Checked)
                {
                    size = (this.radioButtonEight.Checked) ? 8 : 6;
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
                    // need to do encoding here
                    key = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(Base32.Decode(this.textBoxKey.Text.Replace(" ", string.Empty).Replace("-", string.Empty)));
                }
                else
                    key = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(Base32.Decode(this.textBoxKey.Text.Replace(" ", string.Empty).Replace("-", string.Empty)));

                // hashmode
                if (this.radioButtonSha1.Checked)
                    hashMode = OtpHashMode.Sha1;
                else if (this.radioButtonSha256.Checked)
                    hashMode = OtpHashMode.Sha256;
                else if (this.radioButtonSha512.Checked)
                    hashMode = OtpHashMode.Sha512;

                this.Data = new OtpAuthData()
                {
                    Key = key,
                    Size = size,
                    Step = step,
                    OtpHashMode = hashMode
                };
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {
            var unpaddedBase32 = this.textBoxKey.Text.Replace(" ", string.Empty).Replace("-", string.Empty).ToUpperInvariant().TrimEnd('=');

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

        private void checkBoxCustomSettings_CheckedChanged(object sender, EventArgs e)
        {
            SetCustomSettingsState(this.fullyLoaded);
        }

        private void SetCustomSettingsState(bool showWarning)
        {
            var useCustom = this.checkBoxCustomSettings.Checked;

            if (useCustom && showWarning)
                MessageBox.Show("Most providers use the default settings. Be sure that the provider requires these custom settings.", "Custom Settings");

            this.radioButtonBase32.Enabled =
                this.radioButtonBase64.Enabled =
                this.radioButtonHex.Enabled =
                this.radioButtonSix.Enabled =
                this.radioButtonEight.Enabled =
                this.textBoxStep.Enabled = useCustom;
        }
    }
}
