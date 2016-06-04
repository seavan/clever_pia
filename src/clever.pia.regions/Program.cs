using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace clever.pia.regions
{
    class Program
    {
        static bool ForkAdministrative()
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool administrativeMode = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!administrativeMode)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "runas";
                startInfo.FileName = Application.ExecutablePath;
                Process.Start(startInfo);
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            if (!ForkAdministrative()) return;

            Console.WriteLine("Running in administrative mode");
            var dictionary = new Dictionary<string, string>();
            dictionary["www.privateinternetaccess.com"] = "127.0.0.1";
            dictionary["privateinternetaccess.com"] = "127.0.0.1";
            Hosts.Merge(dictionary);
            RegionData.Exclude("turkey");
            Console.WriteLine("All updated");
            Console.ReadLine();
          
        }
    }
}
