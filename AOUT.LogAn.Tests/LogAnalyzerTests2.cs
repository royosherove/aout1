using System;
using NUnit.Framework;

namespace AOUT.CH2.LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerTests2
    {
        private LogAnalyzer m_analyzer=null;
        private DateTime myDate;
        [SetUp]
        public void Setup()
        {
            m_analyzer = new LogAnalyzer();    
            myDate = new DateTime(2006,12,28,5,32,0);
        }
        
        [Test]
        public void IsValidFileName_validFileLowerCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("whatever.slf");
            
            Assert.IsTrue(result, "filename should be valid!");
        }
        
        [Test]
        public void IsValidFileName_validFileWithDateUpperCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName(myDate.ToShortDateString() + ".SLF");
            Assert.IsTrue(result, "filename should be valid!");
        }

    }
}
