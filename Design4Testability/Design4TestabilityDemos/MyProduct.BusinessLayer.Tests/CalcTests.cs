using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyProduct.BusinessLayer.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestInitialize]
        public void Setup()
        {
            c = new Calc();
        }

        private Calc c;


        [TestMethod]
        public void GetSomeNumber_EmtpyString_ReturnZero()
        {
            int result = c.GetSomeNumber("");
            Assert.AreEqual(0,result);
        }
        
        
        [TestMethod]
        public void GetSomeNumber_NegativeNumber_ReturnsAsPositive()
        {
            int result = c.GetSomeNumber("-1");
            Assert.AreEqual(1,result);
        }
        
        [TestMethod]
        public void GetSomeNumber_NegativeNumber_ReturnsAsPositive2()
        {

            int result = c.GetSomeNumber("-2");
            Assert.AreEqual(2,result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetSomeNumber_StringWithSpace_ThrowsException()
        {
            c.GetSomeNumber(" ");
        }

     
    }
}
