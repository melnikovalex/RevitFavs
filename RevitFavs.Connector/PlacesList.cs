using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace MelnikovAlex.RevitFavs
{
    class PlacesList
    {
        Dictionary<int, Place> placesDict;

        public PlacesList()
        {
            placesDict = new Dictionary<int, Place>();
        }

        public void Add(string registryKey, Place place)
        {
            int position = ParseRegistryKey(registryKey);
            // In this case - -1 is invalid value
            if (position == -1)
                throw new ArgumentException("Cannot parse registryKey");
            Add(place, position);
        }

        public void Add(Place place, int position=-1, bool replace = false)
        {
            // in this case - -1 is the end of the list
            if (position == -1)
            {
                position = LastKey();
                if (!replace)
                    position++;
            }
            if (!replace)
                Shift(position);
            else
            {
                if (placesDict.Keys.Contains(position))
                    placesDict.Remove(position);
            }
            placesDict.Add(position, place);
        }

        public Place Search(string searchString)
        {
            int i = SearchForPosition(searchString);
            if (i >= 0)
                return placesDict[i];
            else
                return null;
        }

        public int SearchForPosition(string searchString)
        {
            searchString = searchString.ToLower();
            foreach (var item in placesDict)
            {
                int key = item.Key;
                Place place = item.Value;
                if (key.ToString() == searchString ||
                    place.Path.ToLower() == searchString ||
                    place.Display.ToLower() == searchString)
                    return key;
            }
            return -1;
        }

        public void Move(int oldPosition, int moveValue, bool shift = false, bool replace=false)
        {
            Place place = placesDict[oldPosition];
            placesDict.Remove(oldPosition);
            int newPosition = shift ? oldPosition + moveValue : moveValue;
            Add(place, newPosition, replace);
        }

        public void Remove(int position)
        {
            placesDict.Remove(position);
            Shift(position, -1);
        }

        private void Shift(int from, int offset = 1)
        {
            Dictionary<int, Place> _placedDict = new Dictionary<int, Place>();
            foreach (int position in placesDict.Keys)
            {
                if (position < from)
                    _placedDict.Add(position, placesDict[position]);
                else
                    _placedDict.Add(position + offset, placesDict[position]);
            }
            placesDict = _placedDict;
        }

        private int LastKey()
        {
            return placesDict.Keys.Max();
        }
        private int ParseRegistryKey(string key)
        {
            Match match = Regex.Match(key, Connector.keyMask);
            if (match.Success)
            {
                if (Int32.TryParse(match.Groups[1].Value, out int result))
                {
                    return result;
                }
            }
            return -1;
        }

        private bool VerifySequence()
        {
            List<int> sortedKeys = placesDict.Keys.ToList();
            sortedKeys.Sort();
            int lastKey = -1;
            foreach (int key in sortedKeys)
            {
                if (key != lastKey + 1)
                    return false;
                lastKey = key;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in placesDict)
            {
                sb.AppendLine($"{item.Key} {item.Value}");

            }
            return sb.ToString();
        }
    }
}
