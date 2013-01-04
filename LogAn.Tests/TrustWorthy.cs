using NUnit.Framework;

namespace AOUT.CH7.LogAn.Tests
{
    [TestFixture]
    public class TrustWorthy
    {
        [Test]
        public void TestingTheWrongThing_Add1And1_FailsFirst()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            int sumResult = analyzer.Sum(1, 2);
            
            Assert.AreEqual(4,sumResult);
        }

        [Test]
        public void SemanticsChange()
        {
            LogAnalyzer logan = MakeDefaultAnalyzer();
            Assert.IsFalse(logan.IsValid("abc"));
        }

        private LogAnalyzer MakeDefaultAnalyzer()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            analyzer.Initialize();
            return analyzer;
        }


        [Test]
        public void TestWithMultipleAsserts()
        {
            LogAnalyzer logan = MakeDefaultAnalyzer();
            
            Assert.IsFalse(logan.IsValid("abc"));
            Assert.IsTrue(logan.IsValid("abcde.txt"));
        }
    }

}