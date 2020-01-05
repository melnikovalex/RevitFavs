namespace MelnikovAlex.RevitFavs
{
    public class Place
    {
        public string Path { get; }
        public string Display { get; }
        public string Ext { get; }

        public Place(string path, string display = null, string ext = null)
        {
            Display = display == null ? System.IO.Path.GetFileName(path) : display;
            Path = path != null ? path : "";
            Ext = ext != null ? ext : "";
        }

        public override string ToString()
        {
            return $"Display={Display} Path={Path}";
        }
    }
}
