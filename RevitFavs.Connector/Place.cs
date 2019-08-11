namespace MelnikovAlex.RevitFavs
{
    class Place
    {
        public string Path { get; }
        public string Display { get; }

        public Place(string path, string display = null)
        {
            Display = display == null ? System.IO.Path.GetFileName(path) : display;
            Path = path;
        }

        public override string ToString()
        {
            return $"Display:{Display} Path:{Path}";
        }
    }
}
