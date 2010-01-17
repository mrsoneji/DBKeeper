using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbm
{
    class core
    {
        static string filename;
        static string filename_sha1;
        static string sha1sum;
		static bool force_commit;
		static bool bus_output;
		
		public static bool Bus_Output
		{
			set { bus_output = value; }
		}
		
		public static void begin(string file) { begin(file, false); }
        public static void begin(string file, bool forcecommit)
        {
            filename = file;
            filename_sha1 = sha1.GetSHA1(filename.ToLower());
			force_commit = forcecommit;
			
            Console.Write(@"> Checking directory");
            if (!System.IO.Directory.Exists(@".dbm/.objs/" + filename_sha1))
            {
                Console.Write("...dont exists\n");

                Console.Write(@"> Creating directory");
                System.IO.Directory.CreateDirectory(@".dbm/.objs/" + filename_sha1);
                System.IO.Directory.CreateDirectory(@".dbm/.info/" + filename_sha1);
                Console.Write("...done\n");

                getfileversion();
            }
            else
            {
                Console.Write("...done\n");

                getfileversion();
            }
       
        }

        public static void getfileversion()
        {
            Console.Write(@"> Checking file version");
            sha1sum = sha1.GetSHA1File(filename).ToLower();
            if (!force_commit && System.IO.File.Exists(@".dbm/.objs/" + filename_sha1 + @"/" + sha1sum))
            {
                Console.Write("...nothing to commit\n");
            }
            else
            {
                Console.Write("...done\n");

                Console.Write(@"> Commiting latest version");
                System.IO.File.Copy(filename, @".dbm/.objs/" + filename_sha1 + @"/" + sha1sum.ToLower(), true);
                Console.Write("...revision " + getlastrevisionfromfile() + "\n");

                generateinfofromfile();
            }
        }

        public static void viewlog(string file)
        {
            filename = file;
            filename_sha1 = sha1.GetSHA1(filename.ToLower());

			if (!bus_output)
			{
            	Console.Write("> File revisions log: " + file + "\n");
            	Console.Write("\n"); 
			}
			
			// get list of revisions and sort by Revision
			info_file_list revisions = new info_file_list();
			string[] revisionsfile = System.IO.Directory.GetFiles(@".dbm/.info/" + filename_sha1);
			foreach (string s in revisionsfile)
			{
				info_file info = getinfofromfile(s); revisions.Add(info);
			}
			revisions.Sort();

			if (!bus_output)
			{
				foreach (info_file info in revisions)
				{
					Console.Write("Filename: "); Console.Write(info.RealFileName + "\n");
					Console.Write("Type: NIY\n");
					
					//TODO: verificar si se trata de un CREATE, ALTER o DROP
					Console.Write("Author: "); Console.Write(info.UserName + " (" + info.Machine + ")\n");
					Console.Write("Date: "); Console.Write(info.Date.ToLongDateString() + " " + info.Date.ToLongTimeString() + "\n");
					Console.Write("Revision: "); Console.Write(info.Revision.ToString() + "\n");
					Console.Write("Message: "); Console.Write(info.Message + "\n");
					Console.Write("\n");		
				}				
			} else {				
				foreach (info_file info in revisions)
				{
					//TODO: verificar si se trata de un CREATE, ALTER o DROP
					Console.Write(info.RealFileName + ";" + "NIY" + ";" + info.UserName + ";" + info.Machine + ";" + tools.ConvertToUnixTimestamp(info.Date) + ";" + info.Revision.ToString() + ";" + info.Message + "\n");
				}
			}

			if (!bus_output)
			{
            	Console.Write("...done\n");
			}
        }

		public static void viewlog()
		{
			if (!bus_output) {
            	Console.Write("> Repository revisions log\n");
            	Console.Write("\n"); 
			}
			
			// get list of files in repo
			if (bus_output) {
				foreach (string f in System.IO.Directory.GetDirectories(@".dbm/.objs"))
				{
					string[] filename_splitted = f.Split('/');
		            filename = filename_splitted[filename_splitted.Length - 1];
	    	        filename_sha1 = filename;
					
					string[] revisionsfile = System.IO.Directory.GetFiles(@".dbm/.info/" + filename_sha1, @"*." + (int.Parse(getlastrevisionfromfile()) - 1));
					info_file info = getinfofromfile(revisionsfile[0]);
					
					//TODO: verificar si se trata de un CREATE, ALTER o DROP
					Console.Write(info.RealFileName + ";" + "NIY" + ";" + info.UserName + ";" + info.Machine + ";" + tools.ConvertToUnixTimestamp(info.Date) + ";" + info.Revision.ToString() + ";" + info.Message + "\n");
				}
			} else {
				foreach (string f in System.IO.Directory.GetDirectories(@".dbm/.objs"))
				{
					string[] filename_splitted = f.Split('/');
		            filename = filename_splitted[filename_splitted.Length - 1];
	    	        filename_sha1 = filename;
					
					string[] revisionsfile = System.IO.Directory.GetFiles(@".dbm/.info/" + filename_sha1, @"*." + (int.Parse(getlastrevisionfromfile()) - 1));
					info_file info = getinfofromfile(revisionsfile[0]);
					
					Console.Write("Filename: "); Console.Write(info.RealFileName + "\n");
					Console.Write("Type: NIY\n");
					Console.Write("Author: "); Console.Write(info.UserName + " (" + info.Machine + ")\n");
					Console.Write("Date: "); Console.Write(info.Date.ToLongDateString() + " " + info.Date.ToLongTimeString() + "\n");
					Console.Write("Revision: "); Console.Write(info.Revision.ToString() + "\n");
					Console.Write("Message: "); Console.Write(info.Message + "\n");
					Console.Write("\n");		
				}
			}
			
			if (!bus_output) {

            	Console.Write("...done\n");			
			}
		}
			
        public static string getlastrevisionfromfile()
        {
            return System.IO.Directory.GetFiles(@".dbm/.info/" + filename_sha1).Length.ToString();
        }

        public static void generateinfofromfile()
        {
			string info_file = sha1sum + "." + tools.ConvertToUnixTimestamp(DateTime.Now).ToString() + "." + getlastrevisionfromfile();
			
            Console.Write(@"> Generating info");
			System.IO.File.AppendAllText(@".dbm/.info/" + filename_sha1 + @"/" + info_file, filename + "\n");
            System.IO.File.AppendAllText(@".dbm/.info/" + filename_sha1 + @"/" + info_file, Environment.UserName + "\n");
            System.IO.File.AppendAllText(@".dbm/.info/" + filename_sha1 + @"/" + info_file, Environment.MachineName + "\n");
            System.IO.File.AppendAllText(@".dbm/.info/" + filename_sha1 + @"/" + info_file, "THIS IS A MESSAGE" + "\n");
            Console.Write("...done\n");
        }

		public static info_file getinfofromfile(string file)
		{
			System.IO.StreamReader reader = new System.IO.StreamReader(file);
			
			info_file info = new info_file();
			info.RealFileName = reader.ReadLine();
			info.UserName = reader.ReadLine();
			info.Machine = reader.ReadLine();
			info.Message = reader.ReadLine();
			
			// split the filename into [filename sha1].[date].[revision]
			string[] fileonly = file.Split('/');
			string[] filename_splited = fileonly[fileonly.Length - 1].Split('.');
			double unixdatetime = double.Parse(filename_splited[1]);
			int revision = int.Parse(filename_splited[2]);
			info.Date = tools.ConvertFromUnixTimestamp(unixdatetime);
			info.Revision = revision;
			
			return info;
		}
		
        public static void createrepo()
        {
            Console.Write(@"> Creating folders tree");
            System.IO.Directory.CreateDirectory(@".dbm");
            System.IO.Directory.CreateDirectory(@".dbm/.info");
            System.IO.Directory.CreateDirectory(@".dbm/.tags");
            System.IO.Directory.CreateDirectory(@".dbm/.conf");
            System.IO.Directory.CreateDirectory(@".dbm/.objs");
            Console.Write("...done\n");
        }
    }
}
