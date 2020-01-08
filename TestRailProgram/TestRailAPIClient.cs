using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TestRail;
using TestRail.Types;

namespace TestRailProgram
{
	public class TestRailApiClient
	{
		private readonly string _testRailHost = ConfigurationManager.AppSettings.Get("testRailUrl");
		private static TestRailApiClient _client;
		private static TestRailClient _apiClient;
		private readonly string _userName = ConfigurationManager.AppSettings.Get("user");
		private readonly string _userPassword = ConfigurationManager.AppSettings.Get("password");


		public static TestRailApiClient GetClient()
		{
			return _client ??= new TestRailApiClient();
		}

		private TestRailApiClient()
		{
			_apiClient = new TestRailClient(_testRailHost, _userName, _userPassword);
		}

		public string GetEntryId(ulong planId, ulong runId)
		{
			var plan = _apiClient.GetPlan(planId);
			var entryId = plan.Entries?.FindLast(x => x.RunList.First().ID == runId)?.ID;

			if (entryId == null)
			{
				Console.WriteLine("TestRail: Invalid entryId");
				return null;
			}

			return entryId;
		}

		public List<User> GetUsers()
		{
			var user = _apiClient.GetUsers();

			return user;
		}

		public bool AddResult(ulong runId, ulong caseId, ResultStatus resultStatusId, ulong? userId = null)
		{
			var response = _apiClient.AddResultForCase(runId, caseId, resultStatusId, assignedToID: userId);

			if (!response.WasSuccessful)
			{
				Console.WriteLine($"TestRail: Error occured while reporting results for case {caseId}. Exception: {response.Exception.Message}");
			}

			return response.WasSuccessful;
		}

		public void AddCaseToRun(ulong planId, ulong runId, ulong caseId)
		{
			try
			{
				var entryId = GetEntryId(planId, runId);

				var tests = _apiClient.GetTests(runId).Where(x => x.CaseID.HasValue).Select(x => x.CaseID.Value).ToList();
				tests.Add(caseId);

				var response = _apiClient.UpdatePlanEntry(planId, entryId, caseIDs: tests);

				if (!response.WasSuccessful)
				{
					Console.WriteLine($"TestRail: Error occured while added case {caseId} to run {runId}. Exception: {response.Exception.Message}");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"TestRail: error - {e}");
			}
		}


		public List<Test> GetUntestedCases(ulong runId)
		{
			var tests = _apiClient.GetTests(runId).ToList();

			var untestedCases = new List<Test>();

			foreach (var test in tests)
			{
				if (test.Status == ResultStatus.Untested)
				{
					untestedCases.Add(test);
				}
			}

			return untestedCases;
		}

		public void WriteCasesToFile(List<Test> cases, string fileName)
		{
			using var sw = File.CreateText(fileName);

			foreach (var test in cases)
			{
				sw.WriteLine($"C{test.CaseID}, {test.Title.Replace(",", "")} ,https://webkm-tm.wbs.only.sap/testrail//index.php?/cases/view/{test.ID}");
			}
		}

		public void WriteTestsToFile(List<ulong> tests)
		{
			using var sw = File.CreateText($"EditTests.csv");

			foreach (var test in tests)
			{
				var testCase = _apiClient.GetCase(test);
				sw.WriteLine($"C{testCase.ID},{testCase.Title.Replace(",", "")},https://webkm-tm.wbs.only.sap/testrail//index.php?/cases/view/{testCase.ID}");
			}
		}


		public void DeleteUntestedCasesFromRun(ulong planId, ulong runId)
		{
			var entryId = GetEntryId(planId, runId);

			var tests = _apiClient.GetTests(runId).Where(x => x.CaseID.HasValue).Select(x => x.CaseID.Value).ToList();

			var untested = GetUntestedCases(runId);


			foreach (var untestedTest in untested)
			{
				if (untestedTest.CaseID != null)
					tests.Remove(untestedTest.CaseID.Value);
			}

			var response = _apiClient.UpdatePlanEntry(planId, entryId, caseIDs: tests);

			if (!response.WasSuccessful)
			{
				Console.WriteLine($"Error update '{planId}' entryId '{entryId}'");
			}
		}
	}
}