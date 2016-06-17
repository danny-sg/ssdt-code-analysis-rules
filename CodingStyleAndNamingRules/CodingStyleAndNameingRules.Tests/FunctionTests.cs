using CodingStyleAndNamingRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void Check_Function_When_Valid_Single_Name()
        {
            var value = "uFnFunction";

            var result = ValidationHelpers.IsFunction(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_Function_When_Valid()
        {
            var value = "uFnFunctionName";

            var result = ValidationHelpers.IsFunction(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_Function_When_Valid_With_Underscore()
        {
            var value = "uFn_Function";

            var result = ValidationHelpers.IsFunction(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_Function_When_Not_Prefixed()
        {
            var value = "function";

            var result = ValidationHelpers.IsFunction(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_Function_When_Capitalized_Prefix()
        {
            var value = "UFNFunction";

            var result = ValidationHelpers.IsFunction(value);

            Assert.IsFalse(result);
        }
    }
}
