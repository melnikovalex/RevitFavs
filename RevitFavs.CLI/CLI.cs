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

            PlacesList placesList = null;
            Connector connector = null;

            if (args.CmdList || args.CmdAdd || args.CmdMove || args.CmdRemove)
            {
                // parse RevitYear
                int revitYear;
                if (!Int32.TryParse(args.ArgRevit_year, out revitYear))
                {
                    Console.WriteLine("Cannot parse Revit Year parameter");
                    return;
                }
                // Connect to windows registry
                connector = new Connector(revitYear);
                placesList = connector.Read();
            }

            // add <path> [<title>] [(start | end | <position>) --replace --update] <revit_year>
            if (args.CmdAdd)
            {

                // check if path already exists, remove if --update
                int placeIndex = placesList.SearchForPosition(args.ArgPath);
                if (placeIndex != -1)

                {
                    if (args.OptUpdate)
                    {
                        placesList.Remove(placeIndex);
                        Console.WriteLine("Favourite already exists and will be updated");
                    } else
                        Console.WriteLine("Favourite already exists. To update it use --update option");

                }
                Place place = new Place(args.ArgPath, args.ArgTitle);

                int position;
                if (args.CmdStart)
                    position = 0;
                else if (args.CmdEnd)
                    position = -1;
                else
                    position = Int32.Parse(args.ArgPosition);

                placesList.Add(place, position, args.OptReplace);
                if (!args.OptPreview)
                    connector.Write(placesList);
            }

            else if (args.CmdMove || args.CmdRemove)
            {
                int placeIndex = placesList.SearchForPosition(args.ArgSearch_string);
                if (placeIndex == -1)
                {
                    Console.WriteLine("Favourite not found");
                    return;
                }
                // move <search_string> (up | down | end | <position>) <revit_year>
                if (args.CmdRemove)
                {
                    // loop through all suitable values if --all_occurences
                    while (placeIndex != -1) {
                        placesList.Remove(placeIndex);
                        if (args.OptAll_occurences)
                            placeIndex = placesList.SearchForPosition(args.ArgSearch_string);
                        else
                            placeIndex = -1;
                    }
                }

                // remove <search_string> <revit_year>
                else if (args.CmdMove)
                {
                    int moveValue;
                    bool shift;
                    if (args.CmdUp || args.CmdDown)
                    {
                        shift = true;
                        moveValue = args.CmdUp ? 1 : -1;
                    }
                    else
                    {
                        shift = false;
                        moveValue = args.CmdEnd ? -1 : Int32.Parse(args.ArgPosition);
                    }
                    placesList.Move(placeIndex, moveValue, shift, args.OptReplace);
                }
                if (!args.OptPreview)
                    connector.Write(placesList);
            }


            // list <revit_year>
            if (args.CmdList || args.OptPreview || args.OptList)
            {
                if (args.OptPreview)
                {
                    Console.WriteLine("PREVIEW MODE. No values were changed");
                    Console.WriteLine($"ValidateSequence={placesList.ValidateSequence()}");
                }
                    
                Console.WriteLine(placesList);
            }
                
        }
    }
}
