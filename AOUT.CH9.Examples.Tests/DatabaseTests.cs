using System;
using System.Transactions;
using NUnit.Framework;

namespace AOUT.CH9.Examples.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private TransactionScope transaction;
        [SetUp]
        public void Setup()
        {
            transaction = new TransactionScope();
        }

        [TearDown]
        public void TearDown()
        {
            transaction.Dispose();
        }


        [Test]
        public void SomeDatabaseInsertTest()
        {

            //Do Insert
        }
    }
}
