using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace CodingStyleAndNamingRules
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
            // Remove @ from the variable name
            ValidateName(element);
        }

        private void ValidateName(DeclareVariableElement element)
        {
            var parameterName = element.VariableName.Value.Remove(0, 1);

            if (!ValidationHelpers.IsCamelCase(parameterName))
            {
                DeclareVariableElements.Add(element);
            }
        }

        public override void ExplicitVisit(ProcedureParameter s)
        {
            ValidateName(s);
        }
    }
}
