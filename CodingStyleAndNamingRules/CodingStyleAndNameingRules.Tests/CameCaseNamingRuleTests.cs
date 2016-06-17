using CodingStyleAndNamingRules;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class CameCaseNamingRuleTests
    {
        

        [TestMethod]
        public void Correct_Naming_Is_Valid()
        {
            var model = new TSqlModel(SqlServerVersion.Sql130, new TSqlModelOptions());
            model.AddObjects(@"CREATE TABLE dbo.xamelCase (Column1 INT)");

            var analysis = AnalysisTestHelper.CreateCodeAnalysisService(model, TableCamelCaseRule.RuleId);

            var result = analysis.Analyze(model);

            foreach (var sqlRuleProblem in result.Problems)
            {
                System.Diagnostics.Debug.Print(sqlRuleProblem.ErrorMessageString);
            }

        }
    }
}