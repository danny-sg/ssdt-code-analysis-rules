using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;

namespace CodingStyleAndNamingRules.Rules
{
    [ExportCodeAnalysisRule(RuleId,
                            RuleDisplayName,
                            Description = "View name should be prefixed v then CamelCase",
                            Category = "Naming",
                            RuleScope = SqlRuleScope.Element)]
    public sealed class ViewNamingRule : SqlCodeAnalysisRule
    {
        public const string RuleId = "SGFT.Rules.TableNamingRule.SGFT.002";
        public const string RuleDisplayName = "SGFT.002";
        public const string Message = "View {0} is not prefixed v followed by CamelCase";

        public ViewNamingRule()
        {
            SupportedElementTypes = new[]
            {
                View.TypeClass
            };
        }

        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            var problems = new List<SqlRuleProblem>();
            var objectName = ruleExecutionContext.ModelElement;

            if (objectName != null)
            {
                if (!IsView(objectName.Name))
                {
                    var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;

                    var formattedName = displayServices.GetElementName(objectName, ElementNameStyle.FullyQualifiedName);

                    var problemDescription = string.Format(Message, formattedName);

                    var problem = new SqlRuleProblem(problemDescription, objectName);

                    problems.Add(problem);
                }
            }

            return problems;
        }

        private static bool IsView(ObjectIdentifier id)
        {
            return id.HasName && ValidationHelpers.IsView(id.Parts.Last());
        }
    }
}
