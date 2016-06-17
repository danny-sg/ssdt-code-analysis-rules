using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace CodingStyleAndNamingRules
{
    public class ParameterVisitor : TSqlConcreteFragmentVisitor
    {
        public ParameterVisitor()
        {
            DeclareVariableElements = new List<DeclareVariableElement>();
        }

        public IList<DeclareVariableElement> DeclareVariableElements { get; }

        public override void ExplicitVisit(DeclareVariableElement element)
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
