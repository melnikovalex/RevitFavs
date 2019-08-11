using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocoptNet;

namespace MelnikovAlex.RevitFavs
{
    /// <summary>
    /// list
    /// add path (name position)
    /// remove path/name/position
    /// move path/name/position newPosition
    /// moveup path/name/position
    /// movedown path/name/position
    /// </summary>
    class CLI
    {
        static void Main(string[] argv)
        {
            // Automatically exit(1) if invalid arguments
            var args = new CliArgs(argv);

            // list
            if (args.CmdList)
            {
                int revitYear;
                if (!Int32.TryParse(args.ArgRevit_year, out revitYear))
                {
                    Console.WriteLine("Cannot parse Revit Year parameter");
                    return;
                }
                Connector connector = new Connector(revitYear);
                connector.Read();
                Console.WriteLine("First command");

            }
        }
    }
}
