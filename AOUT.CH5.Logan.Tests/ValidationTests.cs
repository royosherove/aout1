using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace AOUT.CH5.Logan.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void Validate_EnptyString_False()
        {
            Validator v = new Validator();
            bool valid= v.Validate("");
            Assert.IsFalse(valid);
        }
        
        [Test]
        public void Validate_NoAInside_False()
        {
            Validator v = new Validator();
            bool valid= v.Validate("c");
            Assert.IsFalse(valid);
        }
        
        [Test]
        public void Validate_NonEnptyString_True()
        {
            Validator v = new Validator();
            bool valid= v.Validate(" ");
            Assert.IsTrue(valid);
        }

    }

    //1. Validate false first
        // there is a problem if you write a test and fix it as simple as possible
        //it might break the next rules
    //2. validate true first
    class Validator
    {
        public bool Validate(string input)
        {
            if(input!=String.Empty)
            {
                return true;
            }
            //valid if string is not empty
            //valid if string contains "a"
            //valid if string does not contain "b"
            //valid if string length more than 8
            //valid if string length less than 20
            return false;
        }
    }
}
