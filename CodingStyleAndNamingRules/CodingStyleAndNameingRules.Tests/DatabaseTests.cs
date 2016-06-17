using System;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var model = new TSqlModel(@"..\..\..\CodingStyleAndNamingRules.TestDatabase\bin\Debug\CodingStyleAndNamingRules.TestDatabase.dacpac"))
            {
                foreach (var sqlObject in model.GetObjects(DacQueryScopes.UserDefined, Table.TypeClass))
                {
                    System.Diagnostics.Debug.Print(sqlObject.Name.ToString());
                }
            }
        }
    }
}
