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

        public bool checkIP(string ip)
        {
            if (Regex.IsMatch(ip, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
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
            string supIP = textEdit2.Text;
            string geoRes = doGrab.DownloadString("http://ip-api.com/line/" + supIP).Replace("success", "");
            string geoCon = doGrab.DownloadString("http://ip-api.com/line/" + supIP + "?fields=2");
#if (DEBUG)
            Console.WriteLine("The supplied ip is " + "(" + supIP + ")");
            Console.WriteLine("Country Geo " + "(" + geoCon + ")");

            // Too long for console only uncomment if you don't want to read it
            // Console.WriteLine("Full Resolved Geo " + "(" + geoRes + ")"); 
#endif
            if (checkIP(supIP))
            {
                DialogResult dr = MessageBox.Show("Please confirm that you wish to geolocate this IP.",
                      "Confirmation", MessageBoxButtons.YesNo);

                switch (dr)
                {
                    case DialogResult.Yes:
                        MessageBox.Show(geoRes, "Geolocation Result");
                        var s = new MemoryStream(doGrab.DownloadData("https://countryflagsapi.com/png/" + geoCon));
                        pictureEdit2.Image = new System.Drawing.Bitmap(s);
                        break;
                    case DialogResult.No:
                        MessageBox.Show("Aborted that one phew", "Validation");
                        break;
                }
            }
        }
    }
}
