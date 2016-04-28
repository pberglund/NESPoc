using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NESPoc.Models
{
	public class DistrictDto
	{
		public DistrictEntry District { get; set; }
		public CompanyEntry Company { get; set; }
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

	public class CompanyEntry
	{
		public string CompanyName { get; set; }

		public List<CompanyRevenue> CompanyRevenues { get; set; }
	}

	public class CompanyRevenue
	{
		public double Date { get; set; }
		public int Revenue { get; set; }
	}
}