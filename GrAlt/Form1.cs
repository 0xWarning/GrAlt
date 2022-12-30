using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GrAlt
{


    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        WebClient doGrab = new WebClient();


        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string supIP = textEdit1.Text;
            NetworkHelper.checkIP(supIP);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string supIPHost = textEdit2.Text;

            dynamic stuff = JObject.Parse(doGrab.DownloadString("http://ip-api.com/json/" + supIPHost));
            string countryCode = stuff.countryCode;
            string country = stuff.country;



#if (DEBUG)
            Console.WriteLine("The supplied ip is " + "(" + supIPHost + ")");
            Console.WriteLine("Country Geo " + "(" + country + ")");

            // Too long for console only uncomment if you don't want to read it
            // Console.WriteLine("Full Resolved Geo " + "(" + geoRes + ")"); 
#endif


            if (comboBoxEdit1.Text == "IP")
            {
                if (NetworkHelper.checkIP(supIPHost))
                {
                    DialogResult dr = MessageBox.Show("Please confirm that you wish to geolocate this IP.",
                          "Confirmation", MessageBoxButtons.YesNo);

                    switch (dr)
                    {
                        case DialogResult.Yes:
#if (DEBUG)
                            Console.WriteLine("Dialog result " + "(YES)");
#endif
                            MessageBox.Show(countryCode, "Geolocation Result **Flag API Can Be Wierd Sometimes**");
                            var s = new MemoryStream(doGrab.DownloadData("https://countryflagsapi.com/png/" + countryCode));
                            pictureEdit2.Image = new System.Drawing.Bitmap(s);
#if (DEBUG)
                            Console.WriteLine("Flag downloaded and set " + "(" + countryCode + ")");
#endif
                            break;
                        case DialogResult.No:
#if (DEBUG)
                            Console.WriteLine("Dialog result " + "(NO)");
#endif
                            MessageBox.Show("Aborted that one phew", "Validation");
                            break;
                    }
                }
            }
            else if (comboBoxEdit1.Text == "Hostname")
            {
                if (NetworkHelper.checkHostname(supIPHost))
                {
                    DialogResult dr = MessageBox.Show("Please confirm that you wish to geolocate this Hostname.",
                          "Confirmation", MessageBoxButtons.YesNo);

                    switch (dr)
                    {
                        case DialogResult.Yes:
#if (DEBUG)
                            Console.WriteLine("Dialog result " + "(YES)");
#endif
                            MessageBox.Show(countryCode, "Geolocation Result **Flag API Can Be Wierd Sometimes**");
                            var s = new MemoryStream(doGrab.DownloadData("https://countryflagsapi.com/png/" + countryCode));
                            pictureEdit2.Image = new System.Drawing.Bitmap(s);
#if (DEBUG)
                            Console.WriteLine("Flag downloaded and set " + "(" + countryCode + ")");
#endif
                            break;
                        case DialogResult.No:
#if (DEBUG)
                            Console.WriteLine("Dialog result " + "(NO)");
#endif
                            MessageBox.Show("Aborted that one phew", "Validation");
                            break;
                    }
                }
            }

        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string supIP = textEdit3.Text;
            NetworkHelper.checkHostname(supIP);
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.IPHostEntry ip = System.Net.Dns.GetHostEntry(textEdit4.Text);
                string hostname = ip.HostName;
                MessageBox.Show(hostname, "Ip to Hostname Result");
            }
            catch {
                MessageBox.Show("An Error Has Occured | Please Report To The Developer !");
            }
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {


            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                // Network does not available.
                MessageBox.Show("Website Not Available", "Is Up ?");
                return;
            }

            Uri uri = new Uri(textEdit5.Text);

            Ping ping = new Ping();
            try
            {
                PingReply pingReply = ping.Send(uri.Host);
                if (pingReply.Status != IPStatus.Success)
                {
                    // Website does not available.
                    MessageBox.Show("Website not Available", "Is Up ?");
                    return;
                }
                else
                { MessageBox.Show("Website Is Available", "Is Up ?"); }
            }
            catch { MessageBox.Show("Uknown", "Is Up ?"); }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
       
        }
    }
}
