using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace AOUT.Misc
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void IsValid_WhenRuleAtLeast6CharsIsFalse_ReturnFalse()
        {
            tv.SetAllRulesToPass();
            tv.FlagAtLeast6Chars = false;
            bool isValid = tv.IsValid("some meaningless text as password");
            Assert.IsFalse(isValid);
        }
        
        [SetUp]
        public void Setup()
        {
        tv = new TestableValidator();    
        }

        private TestableValidator tv;

        [Test]
        public void IsValid_WhenRuleAtLeast6CharsIsTrue_ReturnTrue()
        {
            tv.SetAllRulesToPass();
            tv.FlagAtLeast6Chars = true;
            bool isValid = tv.IsValid("some meaningless text as password");
            Assert.IsTrue(isValid);
        }

        [Test]
        public void IsValid_WhenRuleHasNoSpacesIsFalse_ReturnsFalse()
        {
            tv.SetAllRulesToPass();
            tv.FlagHasNoSpaces = false;
            bool valid = tv.IsValid(null);
            Assert.IsFalse(valid);
        }
        
        [Test]
        public void IsValid_WhenRuleHasTheLetterAIsFalseAndRuleHasNoSpacesIsFalse_ReturnsTrue()
        {
            tv.SetAllRulesToPass();
            tv.FlagHasNoSpaces = false;
            tv.FlagHasTheLetterA = false;
            bool valid = tv.IsValid(null);
            Assert.IsTrue(valid);
        }

        [Test]
        public void HasNoSpaces_HasASpace_ReturnsFalse()
        {
            string password = " ";
            Validator v = new Validator();
            bool hasNoSpaces = v.HasNoSpaces(password);
            Assert.IsFalse(hasNoSpaces);
        }
    }

    internal class TestableValidator:Validator
    {
        public bool FlagAtLeast6Chars;
        public bool FlagHasNoSpaces;
        public bool FlagHasTheLetterA;

        public void SetAllRulesToPass()
        {
            FlagAtLeast6Chars = true;
            FlagHasNoSpaces= true;
            FlagHasTheLetterA= true;
        }

        protected internal override bool HasAtLeast6Chars(string password)
        {
            return FlagAtLeast6Chars;
        }
        protected internal override bool HasNoSpaces(string password)
        {
            return FlagHasNoSpaces;
        }
        protected internal override bool HasTheLetterA(string password)
        {
            return FlagHasTheLetterA;
        }

    }
}
