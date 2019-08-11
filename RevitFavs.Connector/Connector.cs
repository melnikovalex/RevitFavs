using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace MelnikovAlex.RevitFavs
{
    public class Connector
    {
        const string pathMask = @"Software\Autodesk\Revit\Autodesk Revit {0}\Profiles\AllAnavDialogs";
        public const string keyMask = "$PlacesOrder([0-9]+)";


        public int revitYear;
        private string registryPath;

        public Connector(int revitYear)
        {
            this.revitYear = revitYear;

            if (!ValidateYear())
                throw new ArgumentOutOfRangeException("revitYear");     
            
            registryPath = String.Format(pathMask, revitYear.ToString());
            if (!ValidateRegistryPath())
                throw new Exception("Revit installation for selected Year not found");

        }

        private bool ValidateYear()
        {
            return revitYear >= 2009;
        }

        private bool ValidateRegistryPath()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                if (key == null)
                    return false;
            }
            return true;
        }

        public List<dynamic> Read()
        {
            List<dynamic> results = new List<dynamic>();
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                var x = key.GetValueNames();
                Console.WriteLine(x.ToString());
            }
            return results;
        }
    }
}
