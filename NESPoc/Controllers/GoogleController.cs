﻿using System;
using System.Collections.Generic;
using System.Linq;
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

		public ActionResult TestPage()
		{
			return View();
		}

		public ActionResult DistrictChart()
		{
			return View();
		}

		public ActionResult BranchChart()
		{
			return View();
		}

		[HttpPost]
		public ActionResult MidwestData(int? type)
		{
			int typeToSwitch = type == null ? 0 : (int) type;
			dynamic data = null;
			object[] obj = null;

			switch (typeToSwitch)
			{
				default:
				case 0:
					obj = new object[]
					{
						new double[]
						{
							0.007,
							0.007,
							0.008,
							0.009,
							0.008,
							0.009,
							0.009,
							0.008,
							0.009,
							0.008,
							0.007,
							0.008,
							0.008,
							0.008,
							0.009,
							0.009,
							0.008,
							0.008,
							0.008,
							0.008,
							0.008,
							0.007,
							0.006,
							0.006,
						},
						new int[]
						{
							2925987,
							3161657,
							3488980,
							3873309,
							3772323,
							3965592,
							4249127,
							3667705,
							3934838,
							3436256,
							2956282,
							3411471,
							3500552,
							3622254,
							4041303,
							4204165,
							3766098,
							3835411,
							3906205,
							3709012,
							3848036,
							3385292,
							2881898,
							3082204,
						}
					};

					data = obj;

					break;
				case 1:
					obj = new object[]
					{
						new double[]
						{
							new DateTime(2014, 4, 1).UnixTicks(),
							new DateTime(2014, 5, 1).UnixTicks(),
							new DateTime(2014, 6, 1).UnixTicks(),
							new DateTime(2014, 7, 1).UnixTicks(),
							new DateTime(2014, 8, 1).UnixTicks(),
							new DateTime(2014, 9, 1).UnixTicks(),
							new DateTime(2014, 10, 1).UnixTicks(),
							new DateTime(2014, 11, 1).UnixTicks(),
							new DateTime(2014, 12, 1).UnixTicks(),
							new DateTime(2015, 1, 1).UnixTicks(),
							new DateTime(2015, 2, 1).UnixTicks(),
							new DateTime(2015, 3, 1).UnixTicks(),
							new DateTime(2015, 4, 1).UnixTicks(),
							new DateTime(2015, 5, 1).UnixTicks(),
							new DateTime(2015, 6, 1).UnixTicks(),
							new DateTime(2015, 7, 1).UnixTicks(),
							new DateTime(2015, 8, 1).UnixTicks(),
							new DateTime(2015, 9, 1).UnixTicks(),
							new DateTime(2015, 10, 1).UnixTicks(),
							new DateTime(2015, 11, 1).UnixTicks(),
							new DateTime(2015, 12, 1).UnixTicks(),
							new DateTime(2016, 1, 1).UnixTicks(),
							new DateTime(2016, 2, 1).UnixTicks(),
							new DateTime(2016, 3, 1).UnixTicks(),

						},
						new int[]
						{
							25095,
							28165,
							10645,
							57910,
							20030,
							18915,
							26391,
							25155,
							27811,
							23245,
							46880,
							30720,
							33695,
							29370,
							21210,
							48835,
							41680,
							31700,
							10165,
							10165,
							17760,
							5060,
							4515,
							2700,
						}
					};

					data = obj;
					break;
				case 2:
					obj = new object[]
					{
						new string[]
						{
							"B052",
							"B057",
							"B063",
							"B068",
							"B087",
							"B089",
							"B301",
							"B307",
							"B309",
							"B339",
							"Other",
							"0",
						},
						new int[]
						{
							0,
							7595,
							0,
							0,
							0,
							0,
							0,
							0,
							0,
							249260,
							0,
							0,
						},
						new int[]
						{
							0,
							65602,
							0,
							10205,
							0,
							0,
							0,
							0,
							0,
							240060,
							0,
							0,
						}
					};
					data = obj;
					break;
				case 3:
					break;
			}

			return Json(data);
		}
	}
}