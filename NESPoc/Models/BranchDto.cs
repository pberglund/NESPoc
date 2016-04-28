using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NESPoc.Models
{
	public class BranchDto
	{
		public string CompanyName { get; set; }
		public List<BranchRecord> BranchRecords { get; set; }
	}

	public class BranchRecord
	{
		public string BranchName { get; set; }
		public decimal CurrentYearRevenue { get; set; }
		public decimal PreviousYearRevenue { get; set; }
	}
}