using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GrAlt
{


    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {

        private static readonly Regex validHostnameRegex = new Regex(@"^(([a-z]|[a-z][a-z0-9-]*[a-z0-9]).)*([a-z]|[a-z][a-z0-9-]*[a-z0-9])$", RegexOptions.IgnoreCase);

        private static readonly Regex validIpRegex = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        WebClient doGrab = new WebClient();


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates a host name.
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static bool checkHostname(string hostname)
        {
            if (validHostnameRegex.IsMatch(hostname))
            {
#if (DEBUG)
                Console.WriteLine("The supplied hostname is valid " + "(" + hostname + ")");
#endif
                MessageBox.Show("hostname Is Valid", "Validation");
                return true;
            }
            else
            {
#if (DEBUG)
                Console.WriteLine("The supplied hostname is Invalid " + "(" + hostname + ")");
#endif
                MessageBox.Show("hostname Is Invalid", "Validation");
                return false;
            }
        }

        /// <summary>
        /// Validates a IP address.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>

        public static bool checkIP(string ip)
        {
            if (validIpRegex.IsMatch(ip))
            {
#if (DEBUG)
                Console.WriteLine("The supplied ip is valid " + "(" + ip + ")");
#endif
                MessageBox.Show("IP Is Valid", "Validation");
                return true;
            }
            else
            {
#if (DEBUG)
                Console.WriteLine("The supplied ip is Invalid " + "(" + ip + ")");
#endif
                MessageBox.Show("IP Is Invalid", "Validation");
                return false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string supIP = textEdit1.Text;
            checkIP(supIP);
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
                if (checkIP(supIPHost))
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
                if (checkHostname(supIPHost))
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
            checkHostname(supIP);
        }
    }
}
