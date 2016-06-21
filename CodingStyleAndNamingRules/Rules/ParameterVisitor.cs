using System.Collections.Generic;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace CodingStyleAndNamingRules.Rules
{
    public class ParameterVisitor : TSqlFragmentVisitor
    {
        public ParameterVisitor()
        {
            DeclareVariableElements = new List<DeclareVariableElement>();
        }

        public IList<DeclareVariableElement> DeclareVariableElements { get; }

        public override void ExplicitVisit(DeclareVariableElement element)
        {
            ValidateName(element);
        }
        
        public override void ExplicitVisit(ProcedureParameter s)
        {
            ValidateName(s);
        }

        private void ValidateName(DeclareVariableElement element)
        {
            // Remove @ from the variable name
            var parameterName = element.VariableName.Value.Remove(0, 1);

            if (!ValidationHelpers.IsCamelCase(parameterName))
            {
                DeclareVariableElements.Add(element);
            }
        }
    }
}
