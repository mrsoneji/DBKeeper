using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to dbm (database management) v1.0");
            Console.WriteLine("");
            Console.WriteLine("Author: Sebastián Pucheta");
            Console.WriteLine("Maintainers: Leandro Ascaino, Sebastián Pucheta");
            Console.WriteLine("");

            if (args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "commit":
                        core.begin(args[1]);
                        break;
                    case "forcecommit":
                        core.begin(args[1], true);
                        break;
                    case "init":
                        core.createrepo();
                        break;
                    case "log":
                        core.viewlog(args[1]);
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
		                Console.WriteLine("need to specify parameters");
		                Console.WriteLine("dbm.exe [commit|init|log|tag] {filename}");
		                Console.WriteLine("     commit:              commit a file");
		                Console.WriteLine("     init:                 init a repository");
		                Console.WriteLine("     log:              show the commits log for the file or the entire latest version");
		                
		                Console.WriteLine("     file:           filename to commit");
						break;
                }
            }
            else
            {
                Console.WriteLine("need to specify parameters");
                Console.WriteLine("dbm.exe [commit|init|log|tag] {filename}");
                Console.WriteLine("     commit:              commit a file");
                Console.WriteLine("     init:                 init a repository");
                Console.WriteLine("     log:              show the commits log for the file or the entire latest version");
                
                Console.WriteLine("     file:           filename to commit");
            }
			Console.WriteLine("");
        }
    }
}
