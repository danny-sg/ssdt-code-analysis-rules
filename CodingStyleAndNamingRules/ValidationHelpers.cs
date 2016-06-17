using System.Text.RegularExpressions;

namespace CodingStyleAndNamingRules
{
    public class ValidationHelpers
    {
        public static bool IsCamelCase(string value)
        {
            var regEx = new Regex(GetRegularExpression(""));

            return regEx.IsMatch(value);
        }

        public static bool IsStoredProcedure(string value)
        {
            // All Stored Procedures should start uSp

            var regEx = new Regex(GetRegularExpression("(uSp|uSp_)"));

            return regEx.IsMatch(value);
        }

        public static bool IsView(string value)
        {
            // All Views should start v

            var regEx = new Regex(GetRegularExpression("[v]"));

            return regEx.IsMatch(value);
        }

        public static bool IsFunction(string value)
        {
            // All Functions should start uFn

            var regEx = new Regex(GetRegularExpression("(uFn|uFn_)"));

            return regEx.IsMatch(value);
        }

        private static string GetRegularExpression(string prefix)
        {
            return $@"\b^{prefix}[A-Z][a-z]*((_[A-Z]|[A-Z])[a-z]+)*\b";
        }
    }
}