using AOUT.CH3.LogAn;
using NUnit.Framework;

namespace AOUT.CH3.LogAn.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator calc;

        [SetUp]
        public void Setup()
        {
            calc = new Calculator();
        }
        
        [Test]
        public void Sum_NoAddCalls_DefaultsToZero()
        {
            int lastSum = calc.Sum();
            Assert.AreEqual(0,lastSum);
        }
        
        [Test]
        public void Add_CalledOnce_SavesNumberForSum()
        {
            calc.Add(1);
            int lastSum = calc.Sum();
            Assert.AreEqual(1,lastSum);
        }

        [Test]
        public void Sum_AfterCall_ResetsToZero()
        {
            calc.Add(1);
            calc.Sum();
            int lastSum = calc.Sum();
            Assert.AreEqual(0, lastSum);
        }
        
    }
}
