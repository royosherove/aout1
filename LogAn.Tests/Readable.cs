using AOUT.CH7.LogAn;
using NUnit.Framework;

namespace AOUT.CH7.LogAn.Tests
{
    [TestFixture]
    public class Readable
    {
        [Test]
        public void BadlyNamedTest()
        {
            LogAnalyzer log = new LogAnalyzer();
            int result= log.GetLineCount("abc.txt");
            const int COULD_NOT_READ_FILE = -100;
            Assert.AreEqual(COULD_NOT_READ_FILE,result);
        }
        [Test]
        public void BadAssertMessage()
        {
            LogAnalyzer log = new LogAnalyzer();
            int result= log.GetLineCount("abc.txt");
            const int COULD_NOT_READ_FILE = -100;
            Assert.AreEqual(COULD_NOT_READ_FILE,result,"result was {0} instead of {1}",result,COULD_NOT_READ_FILE);
        }
    
    }

    
}
