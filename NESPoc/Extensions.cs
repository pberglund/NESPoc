using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NESPoc
{
	public static class Extensions
	{
		public static double UnixTicks(this DateTime dt)
		{
			DateTime d1 = new DateTime(1970, 1, 1);
			DateTime d2 = dt.ToUniversalTime();
			TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
			return ts.TotalMilliseconds;
		}

	
	}
}