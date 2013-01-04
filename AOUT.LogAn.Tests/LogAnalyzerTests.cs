using System;
using NUnit.Framework;

namespace AOUT.CH2.LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer=null;
        
        [SetUp]
        public void Setup()
        {
            
            m_analyzer = new LogAnalyzer();    
        }
        

        [Test]
        [Ignore("This test is broken")]
        public void IsValidFileName_validFileLowerCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("whatever.slf");
            
            Assert.IsTrue(result, "filename should be valid!");
        }
        
        [Test]
        public void IsValidFileName_validFileUpperCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("whatever.SLF");
            
            Assert.IsTrue(result, "filename should be valid!");
        }
        
        [Test]
        [ExpectedException(typeof(Exception),"No log file with that name exists")]
        public void IsValidFileName_nunintvalidFileUpperCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("whatever.SLF");
            
            Assert.IsTrue(result, "filename should be valid!");
        }


        [TearDown]
        public void TearDown()
        {
            m_analyzer = null;
        }

    }
}
