using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace MelnikovAlex.RevitFavs
{
    public class PlacesList
    {
        public Dictionary<int, Place> placesDict { get; private set; }

        public PlacesList()
        {
            placesDict = new Dictionary<int, Place>();
        }


        public void Add(Place place, int position=-1, bool replace = false)
        {
            // in this case - -1 is the end of the list
            if (position < 0)
            {
                position = LastKey() + position + 1;
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
            if (searchString.Trim().Length == 0)
                throw new ArgumentException("Search string seems to be empty");

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
            Remove(oldPosition);
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

        public bool ValidateSequence()
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
            List<int> keys = placesDict.Keys.ToList();
            keys.Sort();
            foreach (int key in keys)
            {
                Place place = placesDict[key];
                sb.AppendLine($"{key} {place}");

            }
            return sb.ToString();
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
