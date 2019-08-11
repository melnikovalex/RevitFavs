using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace MelnikovAlex.RevitFavs
{
    public class Connector
    {
        const string pathMask = @"Software\Autodesk\Revit\Autodesk Revit {0}\Profiles\AllAnavDialogs";
        public const string keyRegex = "^PlacesOrder([0-9]+)";


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

        public PlacesList Read()
        {
            PlacesList results = new PlacesList();
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                string[] availableValues = key.GetValueNames();
                List<int> availableIndices = FilterAvailableIndices(availableValues);
                foreach (int i in availableIndices)
                {
                    Place place = new Place(
                        key.GetValue($"PlacesOrder{i}").ToString(),
                        key.GetValue($"PlacesOrder{i}Display").ToString());
                    results.Add(place, i);
                }
            }
            return results;
        }

        public void Write(PlacesList placesList)
        {
            // validate sequence
            if (!placesList.ValidateSequence())
                throw new Exception("PlacesList sequence is not valid and cannot be written");

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true))
            {
                // Erase existing names
                foreach(string name in key.GetValueNames())
                {
                    key.DeleteValue(name);
                }
                foreach (var item in placesList.placesDict)
                {
                    int i = item.Key;
                    Place place = item.Value;
                    key.SetValue($"PlacesOrder{i}", place.Path);
                    key.SetValue($"PlacesOrder{i}Display", place.Display);
                    key.SetValue($"PlacesOrder{i}Ext", place.Ext);
                }
            }
        }
        private List<int> FilterAvailableIndices(string[] availableNames)
        {
            HashSet<int> allIndices = new HashSet<int>();
            foreach (string name in availableNames)
            {
                int i = ParseRegistryKey(name);
                if (i != -1)
                    allIndices.Add(i);
            }
            // Filter complete items
            List<int> result = new List<int>();
            foreach (int i in allIndices)
            {
                if (availableNames.Contains($"PlacesOrder{i}") &&
                    availableNames.Contains($"PlacesOrder{i}Display") &&
                    availableNames.Contains($"PlacesOrder{i}Ext"))
                    result.Add(i);
            }
            return result;
        }


        public static int ParseRegistryKey(string key)
        {
            Match match = Regex.Match(key, Connector.keyRegex);
            if (match.Success)
            {
                if (Int32.TryParse(match.Groups[1].Value, out int result))
                {
                    return result;
                }
            }
            return -1;
        }
    }
}
