using CodingStyleAndNamingRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class CamelCaseTests
    {
        [TestMethod]
        public void Check_CamelCase_When_Valid()
        {
            var value = "CamelCaseTableName";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_CamelCase_When_Valid_With_Single_Name()
        {
            var value = "Table";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_CamelCase_When_Lowercase_With_Single_Name()
        {
            var value = "table";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void Check_CamelCase_When_Starts_With_Lowercase()
        {
            var value = "camelCaseTableName";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_CamelCase_When_All_Uppercase()
        {
            var value = "CAMELCASETABLENAME";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_CamelCase_When_Valid_With_Underscores()
        {
            var value = "CamelCase_TableName";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_CamelCase_When_With_Uppercase_Starts_With_Lowercase()
        {
            var value = "camelCase_TableName";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_CamelCase_When_With_Uppercase_Second_Part_Lowercase()
        {
            var value = "CamelCase_tableName";

            var result = ValidationHelpers.IsCamelCase(value);

            Assert.IsFalse(result);
        }
    }
}