using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace KeeOtp
{
    public partial class Troubleshooting : Form
    {
        public Troubleshooting()
        {
            InitializeComponent();
        }

        private void buttonPingGoogle_Click(object sender, EventArgs e)
        {
            try
            {
                using (var wc = new WebClient())
                {
                    wc.DownloadData("https://www.google.com");
                    var dateHeader = wc.ResponseHeaders.Get("Date");
                    var date = DateTime.Parse(dateHeader);

                    var offset = date.ToUniversalTime() - DateTime.UtcNow;

                    if (offset.TotalSeconds == 0)
                        MessageBox.Show("Your time is perfect according to Google's servers");
                    else if (offset.TotalSeconds <= 5)
                        MessageBox.Show("Your time is off by five seconds or less from Google's servers.  You should be just fine.");
                    else
                        MessageBox.Show(string.Format("Your time is off by {0} seconds from Google's servers.  Try correcting the difference and try again.", offset.TotalSeconds));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonTroubleshootingWebsite_Click(object sender, EventArgs e)
        {
            // go to the troubleshooting page
            var url = "https://bitbucket.org/devinmartin/keeotp/wiki/Troubleshooting";
            Process ps = new Process();
            ps.StartInfo = new ProcessStartInfo(url);
            ps.Start();
        }
    }
}
