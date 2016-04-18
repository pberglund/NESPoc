using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace NESPoc.Controllers
{
	public class GoogleController : Controller
	{
		private readonly Random _rnd = new Random();
		private static List<int> _randomNumbers;

		public GoogleController()
		{
			if (_randomNumbers != null) return;

			_randomNumbers = new List<int>();
			for (int i = 0; i < 100; i++)
			{
				_randomNumbers.Add(_rnd.Next(0,100));
			}
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult DecliningSales()
		{
			return View();
		} 

		[HttpPost]
		public ActionResult GraphData(int type, bool random)
		{
			List<object> data = new List<object>();
			int[] years = new int[]{ 2010,
									 2011,
									 2012,
									 2013,
									 2014,
									 2015,
									 2016
								   };
			string[] branches = new[] {"Branch-1",
									   "Branch-2",
									   "Branch-3",
									   "Branch-4",
									   "Branch-5",
									  };
			
			switch (type)
			{
				default:
				case 0: // Branches and values
					data.Add(new[] { "Year", "Value" });
					// Needs to be "massaged" into google charts data format for year legend - more of a data flow type i imagine
					data.AddRange(years.Select((t, i) => new object[] {years[i], GetRandomNumber(i, random)}));
					// Does not
					//data.AddRange(years.Select((t, i) => new object[] {new {v = i, f = t.ToString()}, GetRandomNumber(i, random)}));
					break;
					
				case 1: // Years and Branches

					List<string> yearAndBranchCols = new List<string>()
					{
						"Year"
					};
					yearAndBranchCols.AddRange(branches);

					data.Add(yearAndBranchCols.ToArray());

					for (int i = 0; i < years.Length; i++)
					{
						List<object> objs = new List<object>()
						{
							years[i]
						};

						for (int j = 0; j < yearAndBranchCols.Count - 1; j++)
						{
							objs.Add(GetRandomNumber(j + (years.Length * i), random));
						}

						data.Add(objs.ToArray());
					}
					break;
				case 2:
					string[] months = new string[]
					{
						"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November",
						"December"
					};

					List<string> dateAndValueCols = new List<string>()
					{
						"Date", "Value", "tooltip"
					};

					data.Add(dateAndValueCols.ToArray());

					for (int i = 0; i < months.Length; i++)
					{
						var val = GetRandomNumber(i + (months.Length*i), random);

						List<object> objs = new List<object>
						{
							i,
							val,
							months[i] + ": " + val,
						};

						data.Add(objs.ToArray());
					}
					break;
				case 3:
					List<string> branchAndValueCols = new List<string>()
					{
						"Branch", "Value", "tooltip"
					};

					data.Add(branchAndValueCols.ToArray());
					for (int j = 0; j < branches.Length; j++)
					{
						var val = GetRandomNumber(j, random);

						data.Add(new object[]{j, val, branches[j] + ":" + val});
					}
					break;

			}

			return Json(data);
		}

		private int GetRandomNumber(int i, bool random)
		{
			if (random)
			{
				return _rnd.Next(0, 100);
			}

			if (i < _randomNumbers.Count)
			{
				return _randomNumbers[i];
			}

			return _randomNumbers[_rnd.Next(0, 100)];



		}

		public ActionResult PurchaseHistory()
		{
			return View();
		}
		public ActionResult PurchaseFluctuation()
		{
			return View();
		}
	}
}