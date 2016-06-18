using System.Linq;
using CodingStyleAndNamingRules;
using CodingStyleAndNamingRules.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class ParameterNamingRuleTests : RuleTests
    {
        [TestMethod]
        public void Stored_Procedure_Correct_Naming_Is_Valid_With_Body_Variable()
        {
            var model = ModelFromSql(@"CREATE PROC dbo.uSpTestProc AS
                                            DECLARE @Param VARCHAR(100)
                                            
                                            SELECT @Param");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);
            
            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Stored_Procedure_Correct_Naming_Is_Valid_With_Parameter()
        {
            var model = ModelFromSql(@"CREATE PROC dbo.uSpTestProc @Param INT AS

                                            SELECT @Param");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Stored_Procedure_Incorrect_Naming_Is_Invalid_With_Body_Variable()
        {
            var model = ModelFromSql(@"CREATE PROC dbo.uSpTestProc AS
                                            DECLARE @param VARCHAR(100)
                                            
                                            SELECT @param");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());
        }
        
        [TestMethod]
        public void Stored_Procedure_Incorrect_Naming_Is_Invalid_With_Parameter()
        {
            var model = ModelFromSql(@"CREATE PROC dbo.uSpTestProc @param INT AS

                                            SELECT @param");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsTrue(result.Problems.Any());
        }

        //Function
        [TestMethod]
        public void Function_Correct_Naming_Is_Valid_With_Body_Variable()
        {
            var model = ModelFromSql(@"CREATE FUNCTION dbo.uFnFunction() RETURNS INT AS 
                                       BEGIN
                                            DECLARE @Test INT

                                            RETURN @Test
                                        END");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);
            
            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Function_Correct_Naming_Is_Valid_With_Parameter()
        {
            var model = ModelFromSql(@"CREATE FUNCTION dbo.uFnFunction(@Test INT) RETURNS INT AS 
                                       BEGIN
                                            RETURN @Test
                                        END");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Function_Incorrect_Naming_Is_Invalid_With_Body_Variable()
        {
            var model = ModelFromSql(@"CREATE FUNCTION dbo.uFnFunction() RETURNS INT AS 
                                       BEGIN
                                            DECLARE @test INT

                                            RETURN @test
                                        END");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsTrue(result.Problems.Any());
        }

        [TestMethod]
        public void Function_Incorrect_Naming_Is_Invalid_With_Parameter()
        {
            var model = ModelFromSql(@"CREATE FUNCTION dbo.uFnFunction(@test INT) RETURNS INT AS 
                                       BEGIN
                                            RETURN @test
                                        END");

            var analysis = CreateCodeAnalysisService(model, ParameterNamingRule.RuleId);

            var result = analysis.Analyze(model);

            OutputProblems(result.Problems);

            Assert.IsTrue(result.Problems.Any());
        }
    }
}