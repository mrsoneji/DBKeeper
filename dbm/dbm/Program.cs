using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbm
{
    class Program
    {
		static bool BusOutput;
		static List<string> ProcessArgs(string[] args)
		{
			List<string> tmp = new List<string>();
			
			foreach (string s in args) { 
				if (s == "--busoutput") { 
					BusOutput = true; 
				} else if (s == "--otheroption") {
				} else if (s.StartsWith(@"--")) {
					Console.WriteLine(@"ERROR: Option " + s + " doesn't exists.\n");
					return null;
				} else {
					tmp.Add(s);
				}
			}

			return tmp;
		}

        static void Main(string[] args)
        {
			List<string> arg = ProcessArgs(args);
			
			if (arg == null) { return; }
			
			core.Bus_Output = BusOutput;
			
			if (!BusOutput)
			{
	            Console.WriteLine("Welcome to dbm (database management) v1.0");
	            Console.WriteLine("");
	            Console.WriteLine("Author: Sebastián Pucheta");
	            Console.WriteLine("Maintainers: Leandro Ascaino, Sebastián Pucheta");
	            Console.WriteLine("");
			}
			
            if (arg.Count > 0)
            {
                switch (arg[0].ToLower())
                {
                    case "commit":
                        core.begin(arg[1]);
                        break;
                    case "forcecommit":
                        core.begin(arg[1], true);
                        break;
                    case "init":
                        core.createrepo();
                        break;
                    case "log":
						if (arg.Count > 1)
						{
	                        core.viewlog(arg[1]);
						} else {
	                        core.viewlog();
						}
						break;
                    case "tag":
						// if exists parameter args[2] the user is trying to add a file into a tag create a new tag
						if (args.Length > 2)
						{ 
							// add a file into a tag
							Console.WriteLine(@"add a file into a tag");
						} else {
							if (args.Length > 1)
							{
							    // view a previously created tag
	                        	// core.viewtag(args[1]);
								Console.WriteLine(@"view the list of files for a tag");
							} else {
								// view a list of created tags
								Console.WriteLine(@"view all tags");
							}
						}						
                        break;
				default:
					if (!BusOutput)
					{
		                Console.WriteLine("need to specify parameters");
		                Console.WriteLine("dbm.exe [commit|init|log|tag|forcecommit] {filename}");
		                Console.WriteLine("     commit:              commit a file");
		                Console.WriteLine("     init:                 init a repository");
		                Console.WriteLine("     log:              show the commits log for the file or the entire latest version");
		                
		                Console.WriteLine("     file:           filename to commit");

					}
					break;					
                }
            }
            else
            {
				if (!BusOutput)
				{				
	                Console.WriteLine("need to specify parameters");
	                Console.WriteLine("dbm.exe [commit|init|log|tag] {filename}");
	                Console.WriteLine("     commit:              commit a file");
	                Console.WriteLine("     init:                 init a repository");
	                Console.WriteLine("     log:              show the commits log for the file or the entire latest version");
	                
	                Console.WriteLine("     file:           filename to commit");
				}
            }
			// Console.WriteLine("");
        }
    }
}
