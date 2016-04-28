using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NESPoc.Models
{
	public class DistrictDto
	{
		public DistrictEntry District { get; set; }
		public BranchEntry Branch { get; set; }
	}

	public class DistrictEntry
	{
		public List<DistrictObject> Records { get; set; } 
	}
	public class DistrictObject
	{
		public double PercentRevenue { get; set; }
		public int Revenue { get; set; }
	}

	public class BranchEntry
	{
		public string BranchName { get; set; }

		public List<BranchRevenue> BranchRevenues { get; set; }
	}

	public class BranchRevenue
	{
		public double Date { get; set; }
		public int Revenue { get; set; }
	}
}