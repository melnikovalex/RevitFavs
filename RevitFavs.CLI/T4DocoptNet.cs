





















using System.Collections;
using System.Collections.Generic;
using DocoptNet;

namespace MelnikovAlex.RevitFavs
{

    // Generated class for CLI.usage.txt
	public class CliArgs
	{
		public const string USAGE = @"Example usage for T4 Docopt.NET

Usage:
  revitfavs help
  revitfavs list <revit_year>
  revitfavs add <path> [<title>] [(start | end | <position>) --replace] <revit_year>
  revitfavs move <search_string> (up | down | end | <position>) <revit_year>
  revitfavs remove <search_string> <revit_year>

Explanation:
 This is an example usage file that needs to be customized.
 Every time you change this file, run the Custom Tool command
 on T4DocoptNet.tt to re-generate the MainArgs class
 (defined in T4DocoptNet.cs).
 You can then use the MainArgs classed as follows:

    class Program
    {

       static void DoStuff(string arg, bool flagO, string longValue)
       {
         // ...
       }

        static void Main(string[] argv)
        {
            // Automatically exit(1) if invalid arguments
            var args = new MainArgs(argv, exit: true);
            if (args.CmdCommand)
            {
                Console.WriteLine(""First command"");
                DoStuff(args.ArgArg, args.OptO, args.OptLong);
            }
        }
    }

";
	    private readonly IDictionary<string, ValueObject> _args;
		public CliArgs(ICollection<string> argv, bool help = true,
                                                      object version = null, bool optionsFirst = false, bool exit = false)
		{
			_args = new Docopt().Apply(USAGE, argv, help, version, optionsFirst, exit);
		}

        public IDictionary<string, ValueObject> Args
        {
            get { return _args; }
        }

public bool CmdHelp { get { return _args["help"].IsTrue; } }
		public bool CmdList { get { return _args["list"].IsTrue; } }
		public string ArgRevit_year  { get { return null == _args["<revit_year>"] ? null : _args["<revit_year>"].ToString(); } }
		public bool CmdAdd { get { return _args["add"].IsTrue; } }
		public string ArgPath  { get { return null == _args["<path>"] ? null : _args["<path>"].ToString(); } }
		public string ArgTitle  { get { return null == _args["<title>"] ? null : _args["<title>"].ToString(); } }
		public bool CmdStart { get { return _args["start"].IsTrue; } }
		public bool CmdEnd { get { return _args["end"].IsTrue; } }
		public string ArgPosition  { get { return null == _args["<position>"] ? null : _args["<position>"].ToString(); } }
		public bool OptReplace { get { return _args["--replace"].IsTrue; } }
		public bool CmdMove { get { return _args["move"].IsTrue; } }
		public string ArgSearch_string  { get { return null == _args["<search_string>"] ? null : _args["<search_string>"].ToString(); } }
		public bool CmdUp { get { return _args["up"].IsTrue; } }
		public bool CmdDown { get { return _args["down"].IsTrue; } }
		public bool CmdRemove { get { return _args["remove"].IsTrue; } }
	
	}

	
}

