using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyProduct.BusinessLayer.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        private TransactionScope transaction;
        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();
        }

        [TestCleanup]
        public void TearDown()
        {
            transaction.Dispose();
        }


        [TestMethod]
        public void SomeDatabaseInsertTest()
        {

            //Do Insert
        }
    }
}
