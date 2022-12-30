using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrAlt
{
    public class NetworkHelper
    {

        public static readonly Regex validHostnameRegex = new Regex(@"^(([a-z]|[a-z][a-z0-9-]*[a-z0-9]).)*([a-z]|[a-z][a-z0-9-]*[a-z0-9])$", RegexOptions.IgnoreCase);

        public static readonly Regex validIpRegex = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");


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
    }
}
