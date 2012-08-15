using System;
using System.Diagnostics;
using System.Windows.Forms;
using KeePass.Plugins;

namespace KeeOtp
{
    public sealed class KeeOtpExt : Plugin
    {
        private IPluginHost host = null;
        private ToolStripItem otpDialogToolStripItem;

        /// <summary>
        /// The <c>Initialize</c> function is called by KeePass when
        /// you should initialize your plugin (create menu items, etc.).
        /// </summary>
        /// <param name="host">Plugin host interface. By using this
        /// interface, you can access the KeePass main window and the
        /// currently opened database.</param>
        /// <returns>You must return <c>true</c> in order to signal
        /// successful initialization. If you return <c>false</c>,
        /// KeePass unloads your plugin (without calling the
        /// <c>Terminate</c> function of your plugin).</returns>
        public override bool Initialize(IPluginHost host)
        {
            Debug.Assert(host != null);
            if (host == null) return false;
            this.host = host;
            this.otpDialogToolStripItem = host.MainWindow.EntryContextMenu.Items.Add("One Time Passwords");
            this.otpDialogToolStripItem.Click += new EventHandler(otpDialogToolStripItem_Click);
            return true; // Initialization successful
        }

        /// <summary>
        /// The <c>Terminate</c> function is called by KeePass when
        /// you should free all resources, close open files/streams,
        /// etc. It is also recommended that you remove all your
        /// plugin menu items from the KeePass menu.
        /// </summary>
        public override void Terminate()
        {
            // Remove all of our menu items
            ToolStripItemCollection tsMenu = host.MainWindow.EntryContextMenu.Items;
            tsMenu.Remove(otpDialogToolStripItem);
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
            form.Show();
        }
    }
}
