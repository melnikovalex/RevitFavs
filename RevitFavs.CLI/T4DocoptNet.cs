





















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
  revitfavs list <revit_year>
  revitfavs add <path> [<title>] [(start | end | <position>) --replace --update] <revit_year> [--list --preview]
  revitfavs move <search_string> (up | down | end | <position>) <revit_year> [--list --preview]
  revitfavs remove <search_string> <revit_year> [--all_occurences --list --preview]

Options:
  -h --help     Show this screen.
  --version     Show version.
  --list     Show updated list after execution
  --preview  Show updated list but not write values into the registry";
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

public bool CmdList { get { return _args["list"].IsTrue; } }
		public string ArgRevit_year  { get { return null == _args["<revit_year>"] ? null : _args["<revit_year>"].ToString(); } }
		public bool CmdAdd { get { return _args["add"].IsTrue; } }
		public string ArgPath  { get { return null == _args["<path>"] ? null : _args["<path>"].ToString(); } }
		public string ArgTitle  { get { return null == _args["<title>"] ? null : _args["<title>"].ToString(); } }
		public bool CmdStart { get { return _args["start"].IsTrue; } }
		public bool CmdEnd { get { return _args["end"].IsTrue; } }
		public string ArgPosition  { get { return null == _args["<position>"] ? null : _args["<position>"].ToString(); } }
		public bool OptReplace { get { return _args["--replace"].IsTrue; } }
		public bool OptUpdate { get { return _args["--update"].IsTrue; } }
		public bool OptList { get { return _args["--list"].IsTrue; } }
		public bool OptPreview { get { return _args["--preview"].IsTrue; } }
		public bool CmdMove { get { return _args["move"].IsTrue; } }
		public string ArgSearch_string  { get { return null == _args["<search_string>"] ? null : _args["<search_string>"].ToString(); } }
		public bool CmdUp { get { return _args["up"].IsTrue; } }
		public bool CmdDown { get { return _args["down"].IsTrue; } }
		public bool CmdRemove { get { return _args["remove"].IsTrue; } }
		public bool OptAll_occurences { get { return _args["--all_occurences"].IsTrue; } }
	
	}

	
}

