using System.Linq;
using CodingStyleAndNamingRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class CameCaseNamingRuleTests : RuleTests
    {
        [TestMethod]
        public void Correct_Naming_Is_Valid()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.CamelCase (Column1 INT)");

            var analysis = CreateCodeAnalysisService(model, TableCamelCaseRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Incorrect_Naming_Is_InValid()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.pascalCase (Column1 INT)");

            var analysis = CreateCodeAnalysisService(model, TableCamelCaseRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());
        }
    }
}