using System;
using AOUT.CH6.LogAN;
using NUnit.Framework;

namespace AOUT.CH6.LogAn.Tests
{
    [TestFixture]
    public class ConfigurationManagerTests :BaseTestClass
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Console.WriteLine("in derived");
            LoggingFacility.Logger = new StubLogger();
        }

        [Test]
        public void IsConfigured_not_ReturnsFalse()
        {
            ConfigurationManager cm = new ConfigurationManager();
            bool configured = cm.IsConfigured("something");
            //rest of test
        }
    }

    internal class StubLogger2 : ILogger
    {
        public void Log(string text)
        {
            //do nothing
        }
    }
}
