using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;

namespace CodingStyleAndNamingRules.Rules
{
    [ExportCodeAnalysisRule(RuleId,
                            RuleDisplayName,
                            Description = "Table should be named with CamelCase",
                            Category = "Naming",
                            RuleScope = SqlRuleScope.Element)]
    public sealed class TableNamingRule : SqlCodeAnalysisRule
    {
        public const string RuleId = "SgRules.CodingStyleAndNamingRules.TableNamingRule.SG001";
        public const string RuleDisplayName = "SG.001";
        public const string Message = "Table {0} is not named as CamelCase";

        public TableNamingRule()
        {
            SupportedElementTypes = new[]
            {
                Table.TypeClass
            };
        }

        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
SG.
        }

        private static bool IsCamelCase(ObjectIdentifier id)
        {
            return id.HasName && ValidationHelpers.IsCamelCase(id.Parts.Last());
        }
    }
}
