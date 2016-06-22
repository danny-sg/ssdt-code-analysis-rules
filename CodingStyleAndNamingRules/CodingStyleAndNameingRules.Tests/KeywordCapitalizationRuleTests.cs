// ReSharper disable InconsistentNaming
namespace CodingStyleAndNameingRules.Tests
{
    using System.Linq;

    using CodingStyleAndNamingRules.Rules;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class KeywordCapitalizationRuleTests : RuleTests
    {
        [TestMethod]
        public void Capitalized_Keywords_Are_Valid()
        {
            var model = ModelFromSql(@"CREATE VIEW dbo.vView AS
                                          SELECT 1");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Non_Capitalized_Keywords_Are_Invalid_View()
        {
            var model = ModelFromSql(@"create view dbo.vView AS
                                          Select 1");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());
        }

        [TestMethod]
        public void Capitalized_Keywords_Are_Valid_StoredProcedure()
        {
            var model = ModelFromSql(@"CREATE PROC dbo.uSpProcedure @Param INT AS
                                          SELECT 1 WHERE @Param=@Param");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());

            OutputProblems(result.Problems);
        }
        
        [TestMethod]
        public void Non_Capitalized_Keywords_Are_Invalid_StoredProcedure()
        {
            var model = ModelFromSql(@"create proc dbo.uSpProcedure @Param INT AS
                                          Select 1 where @Param=@Param");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());

            OutputProblems(result.Problems);
        }

        [TestMethod]
        public void Non_Capitalized_Keywords_Are_Invalid_Table()
        {
            var model = ModelFromSql(@"create table dbo.TestTable (a int)");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());

            OutputProblems(result.Problems);
        }

        [TestMethod]
        public void Partial_Non_Capitalized_Keywords_Are_Invalid_Table()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.TestTable (a int)");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());

            OutputProblems(result.Problems);
        }

        [TestMethod]
        public void Capitalized_Keywords_Are_Valid_Table()
        {
            var model = ModelFromSql(@"CREATE TABLE dbo.TestTable (a INT)");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Capitalized_Keywords_Are_Valid_Complex_View()
        {
            var model = ModelFromSql(@"
                CREATE TABLE dbo.TableA (ColumnA INT);
                GO
                CREATE TABLE dbo.TableB (ColumnB INT);
                GO
                CREATE VIEW dbo.vComplexView AS

                    SELECT *
                          ,CASE WHEN GETDATE() > '2016-01-01' THEN 1 ELSE 0 END AS Rubbish
                    FROM   dbo.TableA
                           INNER JOIN dbo.TableB ON dbo.TableA.ColumnA = dbo.TableB.ColumnB
                           LEFT JOIN dbo.TableB ON dbo.TableA.ColumnA = dbo.TableB.ColumnB
                    WHERE  ColumnB BETWEEN 100 AND 200
                GO");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());
        }

        [TestMethod]
        public void Non_Capitalized_Keywords_Are_Invalid_Complex_View()
        {
            var model = ModelFromSql(@"
                create table dbo.tablea (columna int);
                go
                create table dbo.tableb (columnb int);
                go
                create view dbo.vcomplexview as

                    select *
                          ,case when getdate() > '2016-01-01' then 1 else 0 end aS Rubbish
                    from   dbo.tablea
                           inner join dbo.tableb on dbo.tablea.columna = dbo.tableb.columnb
                           left join dbo.tableb on dbo.tablea.columna = dbo.tableb.columnb
                    where  columnb between 100 and 200
                go");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());

            OutputProblems(result.Problems);
        }



        [TestMethod]
        public void Capitalized_Keywords_Are_Valid_Function()
        {
            var model = ModelFromSql(@"CREATE FUNCTION dbo.uFnFunction() RETURNS INT AS 
                                       BEGIN
                                            DECLARE @Test INT
                                            DECLARE @Test2 VARCHAR(MAX)
                                            RETURN @Test
                                        END");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsFalse(result.Problems.Any());

            OutputProblems(result.Problems);
        }

        [TestMethod]
        public void Non_Capitalized_Keywords_Are_Invalid_Function()
        {
            var model = ModelFromSql(@"Create Function dbo.uFnFunction() returns float AS 
                                       begin
                                            declare @TEST int

                                            return @TEST
                                        end");

            var analysis = CreateCodeAnalysisService(model, KeywordCapitalizationRule.RuleId);

            var result = analysis.Analyze(model);

            Assert.IsTrue(result.Problems.Any());

            OutputProblems(result.Problems);
        }
    }
}