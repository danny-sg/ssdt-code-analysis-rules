using System.Collections.Generic;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using System.Linq;

namespace CodingStyleAndNamingRules.Rules
{
    [ExportCodeAnalysisRule(RuleId,
                            RuleDisplayName,
                            Description = "Stored Procedure should be prefixed uSp then CamelCase",
                            Category = "Naming",
                            RuleScope = SqlRuleScope.Element)]
    public sealed class StoredProcedureNamingRule : SqlCodeAnalysisRule
    {
        public const string RuleId = "SgRules.CodingStyleAndNamingRules.StoredProcedureNamingRule.SG003";
        public const string RuleDisplayName = "SG.003";
        public const string Message = "Stored Procedure {0} is not prefixed uSp followed by CamelCase";

        public StoredProcedureNamingRule()
        {
            SupportedElementTypes = new[]
  {
                Procedure.TypeClass
            };
        }
        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            var problems = new List<SqlRuleProblem>();
            var storedProcedure = ruleExecutionContext.ModelElement;

            if (storedProcedure != null)
            {
                if (!IsValidName(storedProcedure.Name))
                {
                    var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;

                    var formattedName = displayServices.GetElementName(storedProcedure, 
                                                                       ElementNameStyle.FullyQualifiedName);

                    var problemDescription = string.Format(Message, formattedName);

                    var problem = new SqlRuleProblem(problemDescription, storedProcedure);

                    problems.Add(problem);
                }
            }

            return problems;
        }

        private static bool IsValidName(ObjectIdentifier name)
        {
            return name.HasName && ValidationHelpers.IsStoredProcedure(name.Parts.Last());
        }
    }
}