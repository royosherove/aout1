using System;
using AOUT.CH4.LogAn;
using NUnit.Framework;

namespace AOUT.Ch4.LogAn.Test
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            MockService mockService = new MockService();
            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName="abc.ext";
            log.Analyze(tooShortFileName);

            Assert.AreEqual("Filename too short:abc.ext",mockService.LastError);
        }
    }

    public class MockService:IWebService
    {
        public string LastError;

        public void LogError(string message)
        {
            LastError = message;
        }
    }
}
