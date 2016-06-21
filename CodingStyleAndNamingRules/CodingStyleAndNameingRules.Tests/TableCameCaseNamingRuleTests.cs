using System.Linq;
using CodingStyleAndNamingRules;
using CodingStyleAndNamingRules.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class TableCameCaseNamingRuleTests : RuleTests
    {
        [TestMethod]
        public void Correct_Naming_Is_Valid()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.CamelCase (Column1 INT)");

            var analysis = CreateCodeAnalysisService(model, TableNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Correct_Naming_Is_Valid_With_Underscore()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.Camel_CaseCase (Column1 INT)");

            var analysis = CreateCodeAnalysisService(model, TableNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Correct_Naming_Is_Valid_With_Known_Exception()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.CamelCase_PnL (Column1 INT)");

            var analysis = CreateCodeAnalysisService(model, TableNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Incorrect_Naming_Is_InValid()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.pascalCase (Column1 INT)");

            var analysis = CreateCodeAnalysisService(model, TableNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsTrue(result.Problems.Any());
        }
    }
}