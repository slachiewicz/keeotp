using System;
using System.Diagnostics;
using System.Windows.Forms;
using KeePass.Plugins;

namespace KeeOtp
{
    public sealed class KeeOtpExt : Plugin
    {
        private IPluginHost host = null;
        private ToolStripItem otpSeperatorToolStripItem;
        private ToolStripItem otpDialogToolStripItem;
        
        public override bool Initialize(IPluginHost host)
        {
            Debug.Assert(host != null);
            if (host == null) return false;
            this.host = host;

            this.otpSeperatorToolStripItem = new ToolStripSeparator();
            host.MainWindow.EntryContextMenu.Items.Add(this.otpSeperatorToolStripItem);

            this.otpDialogToolStripItem = host.MainWindow.EntryContextMenu.Items.Add("One Time Passwords");
            this.otpDialogToolStripItem.Click += new EventHandler(otpDialogToolStripItem_Click);
            return true; // Initialization successful
        }

        public override void Terminate()
        {
            // Remove all of our menu items
            ToolStripItemCollection menu = host.MainWindow.EntryContextMenu.Items;
            menu.Remove(otpSeperatorToolStripItem);
            menu.Remove(otpDialogToolStripItem);
        }

        void otpDialogToolStripItem_Click(object sender, EventArgs e)
        {
            var entries = this.host.MainWindow.GetSelectedEntries();
            if (entries.Length > 1)
                MessageBox.Show("Please select only one entry");
            else if (entries.Length == 0)
                MessageBox.Show("Please select an entry");

            // grab the entry that we care about
            var entry = entries[0];

            ShowOneTimePasswords form = new ShowOneTimePasswords(entry);
            form.ShowDialog();
        }
    }
}
