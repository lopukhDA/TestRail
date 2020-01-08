using System.IO;
using TestRail.Types;

namespace TestRailProgram
{
	internal class Program
	{
		//private static void Main(string[] args)
		//{
		//	var tests = new List<ulong>()
		//	{
		//		6950307,
		//		6952929,
		//		6950308,
		//		6950318,
		//		7914422,
		//		7914423,
		//		7914424,
		//		6950312,
		//		7355343,
		//		7355344,
		//		6952932,
		//		7355345,  
		//		7355346,
		//		7355347,
		//		7355348,
		//		7355349,
		//		7355350,
		//		7355351,
		//		7319370,
		//		7319367,
		//		7711578,
		//		7319363,
		//		7319364,
		//		7319365,
		//		7319366,
		//		7319360,
		//		7319361,
		//		7319362,
		//		7319371,
		//		6952924,
		//		6950310,
		//		6950306,
		//		6950311,
		//		6950428,
		//		6950345,
		//		6950344,
		//		6950331,
		//		6950309,
		//		6952927,
		//		6950342,
		//		6950332,
		//		7365815,
		//		6950357,
		//		7365790,
		//		6952934,
		//		7365778,
		//		7421188,
		//		7421184, 
		//		6950291, 
		//		6950350,
		//		6950349,
		//		6950348,
		//		6952925,
		//		7355368,
		//		7355369,
		//		7355370,
		//		6950328,
		//		7355371,
		//		7355372,
		//		7355373,
		//		7355374,
		//		7355375,
		//		6950343,
		//		7361752,
		//		6950341,
		//		7365818,
		//		6950353,
		//		7355354,
		//		7355355,
		//		7355356,
		//		7355357,
		//		7355358,
		//		6950330,
		//		7355359,
		//		6952922,
		//		6950407,
		//		7365770,
		//		6950411,
		//		6950412,
		//		7365771,
		//		6950305,
		//		6952931,
		//		6950325,
		//		7711085,
		//		6950435,
		//		6950352,
		//		6950351,
		//		6950304,
		//		7361753,
		//		6950313,
		//		7361754,
		//		7361755,
		//		6950425,
		//		6950419,
		//		6950416,
		//		6950420,
		//		6950429,
		//		6950417,
		//		6950422,
		//		6950421,
		//		6950329,
		//		7355381,
		//		7355382,
		//		7355383,
		//		6950302,
		//		6950303,
		//		6952926,
		//		7365772
		//	};
		//	var client = TestRailApiClient.GetClient();

		//	client.WriteTestsToFile(tests);

		//}


		private static void Main(string[] args)
		{
			const ulong planId = 63432;
			const ulong runId = 63433;
			var file = $"{runId}.csv";

			ulong? userId = 214;

			var client = TestRailApiClient.GetClient();

			//client.AddResult(runId, 6950293, ResultStatus.Retest, userId);


			var x =  client.GetEntryId(planId, runId);


			//var untestedCases = client.GetUntestedCases(runId);
			//client.WriteCasesToFile(untestedCases, file);
			//client.DeleteUntestedCasesFromRun(planId, runId);




			//var cases = client.GetUntestedCases(runId);

			//using var sw = File.CreateText(file);

			//foreach (var @case in cases)
			//{
			//	sw.WriteLine($"{@case.CaseID}    {@case.Title}");
			//}
		}
	}
}
