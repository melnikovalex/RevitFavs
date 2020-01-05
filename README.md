# RevitFavs
Command line interface for managing Revit favourite places (Open dialog)

 - aimed to make adding places faster (can be used with .cmd or .lnk files), 
 - favourite places can be added centrally for organisations
 - link title isn't necessarily identical with folder name!

```
Usage:
  revitfavs list <revit_year>
  revitfavs add <path> [<title>] [(start | end | <position>) --replace --update] <revit_year> [--list --preview]
  revitfavs move <search_string> (up | down | end | <position>) <revit_year> [--list --preview]
  revitfavs remove <search_string> <revit_year> [--all_occurences --list --preview]

Options:
  -h --help     Show this screen.
  --version     Show version.
  --list     Show updated list after execution
  --preview  Show updated list but not write values into the registry

Explanation:
  If path, that you're trying to add, already exists it won't be updated until you using --update option. Use --replace to overwrite item on the same position.
  To move or remove a place simply use index, path or title of it as <search_string>.
```
