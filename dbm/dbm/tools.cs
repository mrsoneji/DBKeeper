
using System;

namespace dbm
{
	public class tools
	{	
		public tools()
		{
		}

		public static DateTime ConvertFromUnixTimestamp(double timestamp)
		{
		    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		    return origin.AddSeconds(timestamp);
		}
		
		public static double ConvertToUnixTimestamp(DateTime date)
		{
		    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		    TimeSpan diff = date - origin;
		    return Math.Floor(diff.TotalSeconds);
		}			
		
	}
}
