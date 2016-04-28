using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NESPoc.Models;

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

		public ActionResult TimelineChart()
		{
			return View();
		}

		public ActionResult TimelineBranchChart()
		{
			return View();
		}
		
		public ActionResult TrendlineTimelineChart()
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

					DistrictDto districtDto = new DistrictDto()
					{
						District = new DistrictEntry()
						{
							Records = new List<DistrictObject>()
							{
								new DistrictObject {PercentRevenue = 0.07, Revenue = 2925987,},
								new DistrictObject {PercentRevenue = 0.07, Revenue = 3161657,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3488980,},
								new DistrictObject {PercentRevenue = 0.09, Revenue = 3873309,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3772323,},
								new DistrictObject {PercentRevenue = 0.09, Revenue = 3965592,},
								new DistrictObject {PercentRevenue = 0.09, Revenue = 4249127,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3667705,},
								new DistrictObject {PercentRevenue = 0.09, Revenue = 3934838,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3436256,},
								new DistrictObject {PercentRevenue = 0.07, Revenue = 2956282,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3411471,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3500552,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3622254,},
								new DistrictObject {PercentRevenue = 0.09, Revenue = 4041303,},
								new DistrictObject {PercentRevenue = 0.09, Revenue = 4204165,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3766098,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3835411,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3906205,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3709012,},
								new DistrictObject {PercentRevenue = 0.08, Revenue = 3848036,},
								new DistrictObject {PercentRevenue = 0.07, Revenue = 3385292,},
								new DistrictObject {PercentRevenue = 0.06, Revenue = 2881898,},
								new DistrictObject {PercentRevenue = 0.06, Revenue = 3082204,},
							}
						},
						Company = new CompanyEntry()
						{
							CompanyName = "21st Century Salvage",
							CompanyRevenues = new List<CompanyRevenue>()
							{

								new CompanyRevenue {Date = new DateTime(2014, 4, 1).UnixTicks(), Revenue = 25095},
								new CompanyRevenue {Date = new DateTime(2014, 5, 1).UnixTicks(), Revenue = 28165},
								new CompanyRevenue {Date = new DateTime(2014, 6, 1).UnixTicks(), Revenue = 10645},
								new CompanyRevenue {Date = new DateTime(2014, 7, 1).UnixTicks(), Revenue = 57910},
								new CompanyRevenue {Date = new DateTime(2014, 8, 1).UnixTicks(), Revenue = 20030},
								new CompanyRevenue {Date = new DateTime(2014, 9, 1).UnixTicks(), Revenue = 18915},
								new CompanyRevenue {Date = new DateTime(2014, 10, 1).UnixTicks(), Revenue = 26391},
								new CompanyRevenue {Date = new DateTime(2014, 11, 1).UnixTicks(), Revenue = 25155},
								new CompanyRevenue {Date = new DateTime(2014, 12, 1).UnixTicks(), Revenue = 27811},
								new CompanyRevenue {Date = new DateTime(2015, 1, 1).UnixTicks(), Revenue = 23245},
								new CompanyRevenue {Date = new DateTime(2015, 2, 1).UnixTicks(), Revenue = 46880},
								new CompanyRevenue {Date = new DateTime(2015, 3, 1).UnixTicks(), Revenue = 30720},
								new CompanyRevenue {Date = new DateTime(2015, 4, 1).UnixTicks(), Revenue = 33695},
								new CompanyRevenue {Date = new DateTime(2015, 5, 1).UnixTicks(), Revenue = 29370},
								new CompanyRevenue {Date = new DateTime(2015, 6, 1).UnixTicks(), Revenue = 21210},
								new CompanyRevenue {Date = new DateTime(2015, 7, 1).UnixTicks(), Revenue = 48835},
								new CompanyRevenue {Date = new DateTime(2015, 8, 1).UnixTicks(), Revenue = 41680},
								new CompanyRevenue {Date = new DateTime(2015, 9, 1).UnixTicks(), Revenue = 31700},
								new CompanyRevenue {Date = new DateTime(2015, 10, 1).UnixTicks(), Revenue = 10165},
								new CompanyRevenue {Date = new DateTime(2015, 11, 1).UnixTicks(), Revenue = 10165},
								new CompanyRevenue {Date = new DateTime(2015, 12, 1).UnixTicks(), Revenue = 17760},
								new CompanyRevenue {Date = new DateTime(2016, 1, 1).UnixTicks(), Revenue = 5060},
								new CompanyRevenue {Date = new DateTime(2016, 2, 1).UnixTicks(), Revenue = 4515},
								new CompanyRevenue {Date = new DateTime(2016, 3, 1).UnixTicks(), Revenue = 2700},
							}
						}
					};

					data = districtDto;

					break;
				case 2:
					BranchDto dto = new BranchDto()
					{
						CompanyName = "21st Century Salvage",
						BranchRecords = new List<BranchRecord>()
						{
							new BranchRecord() {BranchName = "B052", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "B057", PreviousYearRevenue = 7595, CurrentYearRevenue = 65602,},
							new BranchRecord() {BranchName = "B063", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "B068", PreviousYearRevenue = 0, CurrentYearRevenue = 10205,},
							new BranchRecord() {BranchName = "B087", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "B089", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "B301", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "B307", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "B309", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "B339", PreviousYearRevenue = 249260, CurrentYearRevenue = 240060,},
							new BranchRecord() {BranchName = "Other", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
							new BranchRecord() {BranchName = "0", PreviousYearRevenue = 0, CurrentYearRevenue = 0,},
						}

					};
					data = dto;
					break;
				case 3:

					#region Case 3

					obj = new object[]
					{
						new
						{
							BranchValues = new int[]
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
							},
							DistrictValues = new int[]
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
							},
							Date = new DateTime(2016, 3, 1).UnixTicks(),
							DistrictPercentage = 0.0,
							CustomerPercentage = -3.1,
							PercentageDifference = -3.1 - 0.0,
						},
						new
						{
							BranchValues = new int[]
							{
								36985,
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

							},
							DistrictValues = new int[]
							{
								2746373,
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

							},
							Date = new DateTime(2016, 2, 1).UnixTicks(),

							DistrictPercentage = 0.4,
							CustomerPercentage = -2.5,
							PercentageDifference = -2.5 - 0.4,
						},
						new
						{
							BranchValues = new int[]
							{
								10620,
								36985,
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


							},
							DistrictValues = new int[]
							{
								2380292,
								2746373,
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
							},
							Date = new DateTime(2016, 1, 1).UnixTicks(),

							DistrictPercentage = 0.9,
							CustomerPercentage = -1.0,
							PercentageDifference = -1.0 - 0.9,
						},
						new
						{
							BranchValues = new int[]
							{
								1045,
								10620,
								36985,
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
							},
							DistrictValues = new int[]
							{
								2860191,
								2380292,
								2746373,
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
							},
							Date = new DateTime(2015, 12, 1).UnixTicks(),

							DistrictPercentage = 1.2,
							CustomerPercentage = 0.9,
							PercentageDifference = 0.9 - 1.2,
						},
						new
						{
							BranchValues = new int[]
							{
								1720,
								1045,
								10620,
								36985,
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
							},
							DistrictValues = new int[]
							{
								3085937,
								3085937,
								2860191,
								2380292,
								2746373,
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
							},
							Date = new DateTime(2015, 11, 1).UnixTicks(),

							DistrictPercentage = 1.2,
							CustomerPercentage = 2.2,
							PercentageDifference = 2.2 - 1.2,
						},
						new
						{
							BranchValues = new int[]
							{
								3000,
								1720,
								1045,
								10620,
								36985,
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

							},
							DistrictValues = new int[]
							{
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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
							},
							Date = new DateTime(2015, 10, 1).UnixTicks(),

							DistrictPercentage = 1.3,
							CustomerPercentage = 3.8,
							PercentageDifference = 3.8 - 1.3,
						},
						new
						{
							BranchValues = new int[]
							{
								13885,
								3000,
								1720,
								1045,
								10620,
								36985,
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
							},
							DistrictValues = new int[]
							{
								3431411,
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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
							},
							Date = new DateTime(2015, 9, 1).UnixTicks(),

							DistrictPercentage = 1.2,
							CustomerPercentage = 4.9,
							PercentageDifference = 4.9 - 1.2,
						},
						new
						{
							BranchValues = new int[]
							{
								13725,
								13885,
								3000,
								1720,
								1045,
								10620,
								36985,
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

							},
							DistrictValues = new int[]
							{
								3122274,
								3431411,
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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

							},
							Date = new DateTime(2015, 8, 1).UnixTicks(),

							DistrictPercentage = 1.2,
							CustomerPercentage = 5.3,
							PercentageDifference = 5.3 - 1.2,
						},
						new
						{
							BranchValues = new int[]
							{
								26905,
								13725,
								13885,
								3000,
								1720,
								1045,
								10620,
								36985,
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


							},
							DistrictValues = new int[]
							{
								3301553,
								3122274,
								3431411,
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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


							},
							Date = new DateTime(2015, 7, 1).UnixTicks(),

							DistrictPercentage = 1.2,
							CustomerPercentage = 4.6,
							PercentageDifference = 4.6 - 1.2,
						},
						new
						{
							BranchValues = new int[]
							{
								21730,
								26905,
								13725,
								13885,
								3000,
								1720,
								1045,
								10620,
								36985,
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



							},
							DistrictValues = new int[]
							{
								3301553,
								3122274,
								3431411,
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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


							},
							Date = new DateTime(2015, 6, 1).UnixTicks(),

							DistrictPercentage = 0.9,
							CustomerPercentage = 3.7,
							PercentageDifference = 3.7 - 0.9,
						},
						new
						{
							BranchValues = new int[]
							{
								16830,
								21730,
								26905,
								13725,
								13885,
								3000,
								1720,
								1045,
								10620,
								36985,
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




							},
							DistrictValues = new int[]
							{
								2869472,
								3552066,
								3301553,
								3122274,
								3431411,
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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



							},
							Date = new DateTime(2015, 5, 1).UnixTicks(),

							DistrictPercentage = 0.8,
							CustomerPercentage = 4.1,
							PercentageDifference = 0.8 - 4.1,
						},
						new
						{
							BranchValues = new int[]
							{
								11785,
								16830,
								21730,
								26905,
								13725,
								13885,
								3000,
								1720,
								1045,
								10620,
								36985,
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





							},
							DistrictValues = new int[]
							{
								2847086,
								2869472,
								3552066,
								3301553,
								3122274,
								3431411,
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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




							},
							Date = new DateTime(2015, 4, 1).UnixTicks(),

							DistrictPercentage = 0.9,
							CustomerPercentage = 4.5,
							PercentageDifference = 4.5 - 0.9,
						},
						new
						{
							BranchValues = new int[]
							{
								4235,
								11785,
								16830,
								21730,
								26905,
								13725,
								13885,
								3000,
								1720,
								1045,
								10620,
								36985,
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






							},
							DistrictValues = new int[]
							{
								2651354,
								2847086,
								2869472,
								3552066,
								3301553,
								3122274,
								3431411,
								3103984,
								3085937,
								2860191,
								2380292,
								2746373,
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





							},
							Date = new DateTime(2015, 3, 1).UnixTicks(),

							DistrictPercentage = 1.1,
							CustomerPercentage = 5.0,
							PercentageDifference = 5.0 - 1.1,
						},

					};

					data = obj;

					#endregion

					break;
			}

			return Json(data);
		}
	}
}