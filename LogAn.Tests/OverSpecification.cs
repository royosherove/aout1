using AOUT.CH7.LogAn;
using LogAn;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;


namespace AOUT.CH7.LogAn.Tests
{
    [TestFixture]
    public class OverSpecification
    {
        [Test]
        [ExpectedException(typeof(AnalyzerNotInitializedException))]
        public void Initialize_WhenCalled_SetsDefaultDelimiterIsTabDelimiterBetter()
        {
            LogAnalyzer log = new LogAnalyzer();
            log.Analyze("a\tb");
        }
        [Test]
        public void Initialize_WhenCalled_SetsDefaultDelimiterIsTabDelimiter()
        {
            LogAnalyzer log = new LogAnalyzer();

            Assert.AreEqual(null,log.GetInternalDefaultDelimiter());
            log.Initialize();
            Assert.AreEqual('\t', log.GetInternalDefaultDelimiter());
        }

        [Test]
        public void AnalyzeFile_FileWith3Lines_CallsLogProvider3Times()
        {
            MockRepository mocks = new MockRepository();
            ILogProvider mockLog = mocks.StrictMock<ILogProvider>();
            
            LogAnalyzer log = new LogAnalyzer(mockLog);
            using(mocks.Record())
            {
                mockLog.GetLineCount();
                LastCall.Return(3);

                mockLog.GetText("someFile.txt", 1, 1);
                LastCall.Return("a");
                
                mockLog.GetText("someFile.txt", 2, 2);
                LastCall.Return("b");

                mockLog.GetText("someFile.txt", 3, 3);
                LastCall.Return("c");
            }
            AnalyzeResults results = log.AnalyzeFile("someFile.txt");
            mocks.VerifyAll();
        }
        
        
        [Test]
        public void AnalyzeFile_FileWith3Lines_CallsLogProvider3Times_AAA()
        {
            MockRepository mocks  = new MockRepository();
            
            ILogProvider mockLog = mocks.StrictMock<ILogProvider>();
            
            LogAnalyzer log = new LogAnalyzer(mockLog);
            using(mocks.Record())
            {
                mockLog.GetLineCount();
                LastCall.Return(3);

                mockLog.GetText("someFile.txt", 1, 1);
                LastCall.Return("a");
                
                mockLog.GetText("someFile.txt", 2, 2);
                LastCall.Return("b");

                mockLog.GetText("someFile.txt", 3, 3);
                LastCall.Return("c");
            }
            AnalyzeResults results = log.AnalyzeFile("someFile.txt");
            mocks.VerifyAll();
        }
        [Test]
        [ExpectedException(typeof(AnalyzerNotInitializedException))]
        public void Initialize_WhenCalled_SetsDefaultDelimiterIsTabDelimiterBetter2()
        {
            LogAnalyzer log = new LogAnalyzer();
            log.Analyze("a\tb");
        }
        [Test]
        public void Initialize_WhenCalled_SetsDefaultDelimiterIsTabDelimiter3()
        {
            LogAnalyzer log = new LogAnalyzer();

            Assert.AreEqual(null,log.GetInternalDefaultDelimiter());
            log.Initialize();
            Assert.AreEqual('\t', log.GetInternalDefaultDelimiter());
        }

        [Test]
        public void AnalyzeFile_FileWith3Lines_CallsLogProvider3Times2()
        {
            MockRepository mocks = new MockRepository();
            ILogProvider mockLog = mocks.StrictMock<ILogProvider>();
            LogAnalyzer log = new LogAnalyzer(mockLog);
            using(mocks.Record())
            {
                mockLog.GetLineCount();
                LastCall.Return(3);

                mockLog.GetText("someFile.txt", 1, 1);
                LastCall.Return("a");
                
                mockLog.GetText("someFile.txt", 2, 2);
                LastCall.Return("b");

                mockLog.GetText("someFile.txt", 3, 3);
                LastCall.Return("c");
            }
            AnalyzeResults results = log.AnalyzeFile("someFile.txt");
            mocks.VerifyAll();
        }
        
        [Test]
        public void AnalyzeFile_FileWith3Lines_CallsLogProvider3TimesLessBrittle()
        {
            MockRepository mocks = new MockRepository();
            ILogProvider stubLog = mocks.Stub<ILogProvider>();
            using(mocks.Record())
            {
                SetupResult.For(stubLog.GetText("", 1, 1))
                    .IgnoreArguments()
                    .Return("a");

                SetupResult.For(stubLog.GetLineCount()).Return(3);
            }
            using(mocks.Playback())
            {
                LogAnalyzer log = new LogAnalyzer(stubLog);
               AnalyzeResults results = log.AnalyzeFile("someFile.txt");
                
                Assert.That(results.Text,Is.EqualTo("aaa"));
            }
        }

        [Test]
        public void AnalyzeFile_AssumeCallOrder()
        {

        }
    }
}