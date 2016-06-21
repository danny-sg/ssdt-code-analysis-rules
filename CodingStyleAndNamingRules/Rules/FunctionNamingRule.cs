using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;

namespace CodingStyleAndNamingRules.Rules
{
    [ExportCodeAnalysisRule(RuleId,
        RuleDisplayName,
        Description = "Functions should be prefixed uFn then CamelCase",
        Category = "Naming",
        RuleScope = SqlRuleScope.Element)]
    public sealed class FunctionNamingRule : SqlCodeAnalysisRule
    {
        public const string RuleId = "SGFT.Rules.TableNamingRule.SGFT.SG004";
        public const string RuleDisplayName = "SGFT.004";
        public const string Message = "Function {0} is not prefixed uSp followed by CamelCase";

        public FunctionNamingRule()
        {
            SupportedElementTypes = new[]
            {
                ScalarFunction.TypeClass,
                TableValuedFunction.TypeClass
            };
        }
        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            var problems = new List<SqlRuleProblem>();
            var context = ruleExecutionContext.ModelElement;

            if (context != null)
            {
                if (!IsValidName(context.Name))
                {
                    var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;

                    var formattedName = displayServices.GetElementName(context,
                                                                       ElementNameStyle.FullyQualifiedName);

                    var problemDescription = string.Format(Message, formattedName);

                    var problem = new SqlRuleProblem(problemDescription, context);

                    problems.Add(problem);
                }
            }

            return problems;
        }

        private static bool IsValidName(ObjectIdentifier name)
        {
            return name.HasName && ValidationHelpers.IsFunction(name.Parts.Last());
        }
    }
}