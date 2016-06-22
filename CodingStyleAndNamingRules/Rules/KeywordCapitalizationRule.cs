using System.Collections.Generic;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace CodingStyleAndNamingRules.Rules
{
    [ExportCodeAnalysisRule(RuleId,
                            RuleDisplayName,
                            Description = "Keywords should be in capitals",
                            Category = "Style",
                            RuleScope = SqlRuleScope.Element)]
    public sealed class KeywordCapitalizationRule : SqlCodeAnalysisRule
    {
        public const string RuleId = "SGFT.Rules.TableNamingRule.SGFT.020";
        public const string RuleDisplayName = "SGFT.020";
        public const string Message = "Keyword {0} should be in capitals";

        public KeywordCapitalizationRule()
        {
            SupportedElementTypes = new[]
            {
                ModelSchema.Procedure,
                ModelSchema.TableValuedFunction,
                ModelSchema.ScalarFunction,
                ModelSchema.View,
                ModelSchema.Table,
                ModelSchema.DatabaseDdlTrigger,
                ModelSchema.DmlTrigger,
                ModelSchema.ServerDdlTrigger
            };
        }

        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            var problems = new List<SqlRuleProblem>();
            var sqlObject = ruleExecutionContext.ModelElement;

            var sqlObjectSourceInfo = sqlObject.GetSourceInformation();

            if (ruleExecutionContext.ScriptFragment.ScriptTokenStream != null)
            {
                foreach (var token in ruleExecutionContext.ScriptFragment.ScriptTokenStream)
                {
                    if (IsKeyword(token) && token.Text?.ToUpper() != token.Text)
                    {
                        var problem = new SqlRuleProblem(string.Format(Message, token.Text), sqlObject);
                        
                        problem.SetSourceInformation(new SourceInformation(sqlObjectSourceInfo.SourceName, 
                                                                           token.Line, 
                                                                           token.Column));

                        problems.Add(problem);
                    }
                }
            }

            return problems;
        }

        private static bool IsKeyword(TSqlParserToken token)
        {
            return token.IsKeyword();
        }
    }
}
