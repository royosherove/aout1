using System;
using AOUT.CH9.Examples.Interfaces;
using NUnit.Framework;

namespace AOUT.CH9.Examples.Tests
{
    [TestFixture]
    public class MyOverridableClassTests
    {
        [Test]
        [ExpectedException(typeof(Exception))]
        public void DoSomething_GivenInvalidInput_ThrowsException()
        {
            MyOverridableClass c = new MyOverridableClass();
            int SOME_NUMBER=1;
            
            //stub the calculation method to return "invalid"
            c.calculateMethod = delegate(int i) { return -1; };
            
            c.DoSomeAction(SOME_NUMBER);
        }
    }
}
