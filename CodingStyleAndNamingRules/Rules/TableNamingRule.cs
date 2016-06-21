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
        public const string RuleId = "SGFT.Rules.TableNamingRule.SGFT.001";
        public const string RuleDisplayName = "SGFT.001";
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
            var problems = new List<SqlRuleProblem>();
            var table = ruleExecutionContext.ModelElement;

            if (table != null)
            {
                if (!IsCamelCase(table.Name))
                {
                    var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;

                    var formattedName = displayServices.GetElementName(table, ElementNameStyle.FullyQualifiedName);

                    var problemDescription = string.Format(Message, formattedName);

                    var problem = new SqlRuleProblem(problemDescription, table);

                    problems.Add(problem);
                }
            }

            return problems;
        }

        private static bool IsCamelCase(ObjectIdentifier id)
        {
            return id.HasName && ValidationHelpers.IsCamelCase(id.Parts.Last());
        }
    }
}
