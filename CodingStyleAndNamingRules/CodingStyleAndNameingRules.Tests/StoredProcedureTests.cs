using CodingStyleAndNamingRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class StoredProcedureTests
    {
        [TestMethod]
        public void Check_StoredProcedure_When_Valid_Single_Name()
        {
            var value = "uSpProc";

            var result = ValidationHelpers.IsStoredProcedure(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_StoredProcedure_When_Valid()
        {
            var value = "uSpStoredProc";

            var result = ValidationHelpers.IsStoredProcedure(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_StoredProcedure_When_Valid_With_Underscore()
        {
            var value = "uSp_StoredProc";

            var result = ValidationHelpers.IsStoredProcedure(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_StoredProcedure_When_Not_Prefixed()
        {
            var value = "storedProc";

            var result = ValidationHelpers.IsStoredProcedure(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_StoredProcedure_When_Capitalized_Prefix()
        {
            var value = "StoredProc";

            var result = ValidationHelpers.IsStoredProcedure(value);

            Assert.IsFalse(result);
        }
    }
}