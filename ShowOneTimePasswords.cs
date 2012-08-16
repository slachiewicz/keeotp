using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KeePassLib.Security;
using OtpSharp;

namespace KeeOtp
{
    public partial class ShowOneTimePasswords : Form
    {
        private KeePassLib.PwEntry entry;
        private Totp totp;
        private int lastCode;
        private int lastRemainingTime;

        public ShowOneTimePasswords(KeePassLib.PwEntry entry)
        {
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
                    this.labelOtp.Text = code.ToString().PadLeft(6, '0');
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
            if (!entry.Strings.Exists("OTP"))
            {
                this.Close();
                MessageBox.Show("Please add a one time password field");
            }
            else
            {
                try
                {
                    var otpEncodedKey = entry.Strings.Get("OTP");
                    var key = Base32.Decode(otpEncodedKey.ReadString());
                    this.totp = new Totp(key);
                    this.timerUpdateTotp.Enabled = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            //entry.Strings.Set("TOTP", new ProtectedString(true, "test"));

            //host.MainWindow.UpdateUI(false, null, true, host.Database.RootGroup,
            //true, null, true);
        }
    }
}
