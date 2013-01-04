using System;
using NUnit.Framework;

namespace AOUT.CH9.Examples.Tests
{
    [TestFixture]
    public class CalcTests
    {
        [SetUp]
        public void Setup()
        {
            c = new Calc();
        }

        private Calc c;


        [Test]
        public void GetSomeNumber_EmtpyString_ReturnZero()
        {
            int result = c.GetSomeNumber("");
            Assert.AreEqual(0,result);
        }
        
        
        [Test]
        public void GetSomeNumber_NegativeNumber_ReturnsAsPositive()
        {
            int result = c.GetSomeNumber("-1");
            Assert.AreEqual(1,result);
        }
        
        [Test]
        public void GetSomeNumber_NegativeNumber_ReturnsAsPositive2()
        {

            int result = c.GetSomeNumber("-2");
            Assert.AreEqual(2,result);
        }
        
        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetSomeNumber_StringWithSpace_ThrowsException()
        {
            c.GetSomeNumber(" ");
        }

     
    }
}
