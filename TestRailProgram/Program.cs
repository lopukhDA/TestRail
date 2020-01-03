using System.IO;

namespace TestRailProgram
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			const ulong planId = 63432;
			const ulong runId = 63433;
			var file = $"{runId}.txt";

			var client = TestRailApiClient.GetClient();

			var cases = client.GetUntestedCases(planId, runId);

			using var sw = File.CreateText(file);
			
			foreach (var @case in cases)
			{
				sw.WriteLine($"{@case.CaseID}    {@case.Title}");
			}
		}
	}
}
