using System;
using AOUT.CH6.LogAN;
using NUnit.Framework;

namespace AOUT.CH6.LogAn.Tests
{
    public class BaseTestClass
    {
        [SetUp]
        public virtual void Setup()
        {
            Console.WriteLine("in setup");
            LoggingFacility.Logger = new StubLogger();
        }
    }
}