using System.Collections.Generic;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;

namespace CodingStyleAndNamingRules.Rules
{
    [ExportCodeAnalysisRule(RuleId,
                            RuleDisplayName,
                            Description = "Parameter should be named with CamelCase",
                            Category = "Naming",
                            RuleScope = SqlRuleScope.Element)]
    public sealed class ParameterNamingRule : SqlCodeAnalysisRule
    {
        public const string RuleId = "SgRules.CodingStyleAndNamingRules.TableNamingRule.SG010";
        public const string RuleDisplayName = "SG.010";
        public const string Message = "Parameter {0} in {1} is not named as CamelCase";

        public ParameterNamingRule()
        {
            SupportedElementTypes = new[]
            {
                ModelSchema.ExtendedProcedure,
                ModelSchema.Procedure,
                ModelSchema.TableValuedFunction,
                ModelSchema.ScalarFunction,
                ModelSchema.DatabaseDdlTrigger,
                ModelSchema.DmlTrigger,
                ModelSchema.ServerDdlTrigger,
            };
        }

        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            IList<SqlRuleProblem> problems = new List<SqlRuleProblem>();

            var modelElement = ruleExecutionContext.ModelElement;

            var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;

            var objectName = displayServices.GetElementName(ruleExecutionContext.ModelElement, 
                                                            ElementNameStyle.FullyQualifiedName);

            var fragment = ruleExecutionContext.ScriptFragment;

            var visitor = new ParameterVisitor();

            fragment.Accept(visitor);

            foreach (var element in visitor.DeclareVariableElements)
            {
                var problemDescription = string.Format(Message, element.VariableName.Value, objectName);

                var problem = new SqlRuleProblem(problemDescription, modelElement, element);

                problems.Add(problem);
            }

            return problems;
        }
    }
}