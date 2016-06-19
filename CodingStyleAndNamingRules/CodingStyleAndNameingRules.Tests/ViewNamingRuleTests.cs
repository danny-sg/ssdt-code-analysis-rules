using System.Linq;
using CodingStyleAndNamingRules;
using CodingStyleAndNamingRules.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class ViewNamingRuleTests : RuleTests
    {
        [TestMethod]
        public void Correct_Naming_Is_Valid()
        {
            var model = ModelFromSql(@"CREATE VIEW dbo.vViewName AS SELECT 1");

            var analysis = CreateCodeAnalysisService(model, ViewNameRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Incorrect_Naming_Is_Invalid()
        {
            var model = ModelFromSql(@"CREATE VIEW dbo.Incorrect AS SELECT 1");

            var analysis = CreateCodeAnalysisService(model, ViewNameRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsTrue(result.Problems.Any());
        }
    }
}