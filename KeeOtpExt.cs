using System;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.Util;
using KeePassLib;
using OtpSharp;

namespace KeeOtp
{
    public sealed class KeeOtpExt : Plugin
    {
        private IPluginHost host = null;
        private ToolStripItem otpSeperatorToolStripItem;
        private ToolStripItem otpDialogToolStripItem;
        private ToolStripItem otpCopyToolStripItem;

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
                return false;
            this.host = host;

            this.otpSeperatorToolStripItem = new ToolStripSeparator();
            host.MainWindow.EntryContextMenu.Items.Add(this.otpSeperatorToolStripItem);

            this.otpDialogToolStripItem = host.MainWindow.EntryContextMenu.Items.Add("Timed One Time Password");
            this.otpDialogToolStripItem.Click += new EventHandler(otpDialogToolStripItem_Click);

            this.otpCopyToolStripItem = host.MainWindow.EntryContextMenu.Items.Add("Copy TOTP to Clipboard");
            this.otpCopyToolStripItem.Click += otpCopyToolStripItem_Click;

            return true; // Initialization successful
        }

        public override void Terminate()
        {
            // Remove all of our menu items
            ToolStripItemCollection menu = host.MainWindow.EntryContextMenu.Items;
            menu.Remove(otpSeperatorToolStripItem);
            menu.Remove(otpDialogToolStripItem);
            menu.Remove(otpCopyToolStripItem);
        }

        void otpDialogToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (GetSelectedSingleEntry(out entry))
            {
                ShowOneTimePasswords form = new ShowOneTimePasswords(entry, host);
                form.ShowDialog();
            }
        }

        void otpCopyToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (this.GetSelectedSingleEntry(out entry))
            {
                if (!entry.Strings.Exists(OtpAuthData.StringDictionaryKey))
                {
                    if (MessageBox.Show("Must configure TOTP on this entry.  Do you want to do this now?", "Not Configured", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ShowOneTimePasswords form = new ShowOneTimePasswords(entry, host);
                        form.ShowDialog();
                    }
                }
                else
                {
                    var data = OtpAuthData.FromString(entry.Strings.Get(OtpAuthData.StringDictionaryKey).ReadString());
                    var totp = new Totp(data.Key, step: data.Step, totpSize: data.Size);
                    var text = totp.ComputeTotp().ToString().PadLeft(data.Size, '0');

                    if (ClipboardUtil.CopyAndMinimize(new KeePassLib.Security.ProtectedString(true, text), true, this.host.MainWindow, entry, this.host.Database))
                        this.host.MainWindow.StartClipboardCountdown();
                }
            }
        }

        private bool GetSelectedSingleEntry(out PwEntry entry)
        {
            entry = null;

            var entries = this.host.MainWindow.GetSelectedEntries();
            if (entries.Length > 1)
            {
                MessageBox.Show("Please select only one entry");
                return false;
            }
            else if (entries.Length == 0)
            {
                MessageBox.Show("Please select an entry");
                return false;
            }

            // grab the entry that we care about
            entry = entries[0];
            return true;
        }

        public override string UpdateUrl
        {
            get { return "https://s3.amazonaws.com/KeeOtp/version_manifest.txt"; }
        }
    }
}
