using System;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePassLib.Security;
using OtpSharp;

namespace KeeOtp
{
    public partial class ShowOneTimePasswords : Form
    {
        private readonly KeePassLib.PwEntry entry;
        private readonly IPluginHost host ;
        private Totp totp;
        private int lastCode;
        private int lastRemainingTime;

        OtpAuthData data;

        public ShowOneTimePasswords(KeePassLib.PwEntry entry, IPluginHost host)
        {
            this.host = host;
            this.entry = entry;
            InitializeComponent();
            this.Shown += (sender, e) => FormWasShown();
            this.timerUpdateTotp.Tick += (sender, e) => UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            var totp = this.totp;
            if (totp != null)
            {
                var code = totp.ComputeTotp();
                var remaining = totp.RemainingSeconds();

                if (code != lastCode)
                {
                    lastCode = code;
                    this.labelOtp.Text = code.ToString().PadLeft(this.data.Size, '0');
                }
                if (remaining != lastRemainingTime)
                {
                    lastRemainingTime = remaining;
                    this.labelRemaining.Text = remaining.ToString();
                }
            }
            else
            {
                MessageBox.Show("Please add a one time password field");
                this.Close();
            }
        }

        private void FormWasShown()
        {
            if (!entry.Strings.Exists(OtpAuthData.StringDictionaryKey))
            {
                this.AddEdit();
            }
            else
            {
                try
                {
                    this.data = OtpAuthData.FromString(entry.Strings.Get(OtpAuthData.StringDictionaryKey).ReadString());

                    ShowCode();
                }
                catch
                {
                    this.AddEdit();
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.AddEdit();
        }

        private void ShowCode()
        {
            this.totp = new Totp(this.data.Key, step: this.data.Step, totpSize: this.data.Size);
            this.timerUpdateTotp.Enabled = true;
        }

        private void AddEdit()
        {
            this.timerUpdateTotp.Enabled = false;
            this.labelRemaining.Text = "x";
            this.labelOtp.Text = "xxxxxx";
            this.totp = null;

            var addEditForm = new OtpInformation();
            addEditForm.Data = this.data;

            var result = addEditForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.data = addEditForm.Data;
                // set the data
                entry.Strings.Set(OtpAuthData.StringDictionaryKey, new ProtectedString(true, this.data.EncodedString));

                // indicate that a change was made, must save
                host.MainWindow.UpdateUI(false, null, true, host.Database.RootGroup, true, null, true);

                this.Show();
            }
            else
                this.Close();
        }
    }
}
