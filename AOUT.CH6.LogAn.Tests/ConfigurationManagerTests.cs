using AOUT.CH6.LogAN;
using NUnit.Framework;

namespace AOUT.CH6.LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerTests : BaseTestClass
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            LogAnalyzer la = new LogAnalyzer();
            la.Analyze("myemptyfile.txt");
            //rest of test
        }
    }

    internal class StubLogger : ILogger
    {
        public void Log(string text)
        {
            //do nothing
        }
    }
}
