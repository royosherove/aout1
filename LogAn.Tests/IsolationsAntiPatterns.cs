using System.Collections.Generic;
using AOUT.CH7.LogAn;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace AOUT.CH7.LogAn.Tests
{
    [TestFixture]
    public class IsolationsAntiPatterns
    {
        private LogAnalyzer logan;
        [Test]
        public void CreateAnalyzer_BadFileName_ReturnsFalse()
        {
            logan = new LogAnalyzer();
            logan.Initialize();
            bool valid = logan.IsValid("abc");
            Assert.That(valid, Is.False);
        }

        [Test]
        public void CreateAnalyzer_GoodFileName_ReturnsTrue()
        {
            bool valid = logan.IsValid("abcdefg");
            Assert.That(valid, Is.True);
        }
    }
    
    
    [TestFixture]
    public class HiddenTestCall
    {
        private LogAnalyzer logan;
        [Test]
        public void CreateAnalyzer_GoodNAmeAndBadNameUsage()
        {
            logan = new LogAnalyzer();
            logan.Initialize();
            bool valid = logan.IsValid("abc");
            Assert.That(valid, Is.False);
            
            CreateAnalyzer_GoodFileName_ReturnsTrue();
        }

        [Test]
        public void CreateAnalyzer_GoodFileName_ReturnsTrue()
        {
            bool valid = logan.IsValid("abcdefg");
            Assert.That(valid, Is.True);
        }
    }
    
    
    [TestFixture]
    public class SharedStateCorruption
    {
        Person person = new Person();
        
        [Test]
        public void CreateAnalyzer_GoodFileName_ReturnsTrue()
        {
            person.AddNumber("055-4556684(34)");
            string found = person.FindPhoneStartingWith("055");
            Assert.AreEqual("055-4556684(34)", found);
        }
        
        [Test]
        public void FindPhoneStartingWith_NoNumbers_ReturnsNull()
        {
            string found = person.FindPhoneStartingWith("0");
            Assert.IsNull(found);
        }
    }

    public class Person
    {
        public void AddNumber(string number)
        {
            phoneNumbers.Add(number);
        }
        private List<string> phoneNumbers = new List<string>();
        public string FindPhoneStartingWith(string start)
        {
            foreach (string number in phoneNumbers)
            {
                if(number.StartsWith(start))
                {
                    return number;
                }
            }
            return null;
        }
    }
}
