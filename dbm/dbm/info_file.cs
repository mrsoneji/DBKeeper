
using System;

namespace dbm
{
	public class info_file_list: System.Collections.Generic.List<info_file>
	{
		
	}
	
	public class info_file: IComparable<info_file>
	{
		string realfilename;
		string username;
		string machine;
		string message;
		DateTime date; 
		int revision; // we are going to order the files using this field		
		
		public info_file()
		{			
		}
		
		public string RealFileName 
		{
			get { return realfilename; }
			set { realfilename = value; }
		}
		
		public string UserName
		{
			get { return username; }
			set { username = value; }
		}
		
		public string Machine
		{
			get { return machine; }
			set { machine = value; }
		}
		
		public string Message
		{
			get { return message; }
			set { message = value; }
		}
		
		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}
		
		public int Revision
		{
			get { return revision; }
			set { revision = value; }
		}
		
		public int CompareTo(info_file other)
		{
			return other.Revision.CompareTo(this.Revision);
		}
	}
}
