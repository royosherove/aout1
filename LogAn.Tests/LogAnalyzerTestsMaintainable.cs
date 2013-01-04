using System.IO;
using NUnit.Framework;

namespace AOUT.CH7.LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerTestsMaintainable
    {
        [SetUp]
        public void Setup()
        {
            logan=new LogAnalyzer();
            logan.Initialize();

            fileInfo=new FileInfo("c:\\someFile.txt");
        }

        private FileInfo fileInfo = null;
        private LogAnalyzer logan= null;

        [Test]
        public void IsValid_LengthBiggerThan8_IsFalse()
        {
            bool valid = logan.IsValid("123456789");
            Assert.IsFalse(valid);
        }
        
        [Test]
        public void IsValid_BadFileInfoInput_returnsFalse()
        {
            bool valid = logan.IsValid(fileInfo);
            Assert.IsFalse(valid);
        }

        [Test]
        public void IsValid_LengthSmallerThan8_IsTrue()
        {
            bool valid = logan.IsValid("1234567");
            Assert.IsTrue(valid);
        }
        
        private LogAnalyzer GetNewAnalyzer()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            analyzer.Initialize();
            return analyzer;
        }
    }
}
