using System;
using AOUT.CH4.LogAn;
using NUnit.Framework;

namespace AOUT.Ch4.LogAn.Test
{
    [TestFixture]
    public class LogAnalyzer2Tests
    {
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            StubService stubService = new StubService();
            stubService.ToThrow=  new Exception("fake exception");
            
            MockEmailService mockEmail = new MockEmailService();
            
            LogAnalyzer2 log = new LogAnalyzer2();
            log.Service = stubService;
            log.Email=mockEmail;

            string tooShortFileName="abc.ext";
            log.Analyze(tooShortFileName);

            Assert.AreEqual("a",mockEmail.To);
            Assert.AreEqual("fake exception",mockEmail.Body);
            Assert.AreEqual("subject",mockEmail.Subject);
        }
    }

    public class StubService:IWebService
    {
        public Exception ToThrow;
        public void LogError(string message)
        {
            if(ToThrow!=null)
            {
                throw ToThrow;
            }
        }
    }

    public class MockEmailService:IEmailService
    {
        public string To;
        public string Subject;
        public string Body;

        public void SendEmail(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
