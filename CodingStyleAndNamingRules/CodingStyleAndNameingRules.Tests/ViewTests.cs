using CodingStyleAndNamingRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class ViewTests
    {
        [TestMethod]
        public void Check_View_When_Valid_Single_Name()
        {
            var value = "vView";

            var result = ValidationHelpers.IsView(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_View_When_Valid()
        {
            var value = "vViewName";

            var result = ValidationHelpers.IsView(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_View_When_Not_Prefixed()
        {
            var value = "Name";

            var result = ValidationHelpers.IsView(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_View_When_Capitalized_Prefix()
        {
            var value = "VName";

            var result = ValidationHelpers.IsView(value);

            Assert.IsFalse(result);
        }
    }
}