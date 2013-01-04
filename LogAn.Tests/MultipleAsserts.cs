using AOUT.CH7.LogAn.Tests;
using AOUT.CH7.LogAn;
using NUnit.Framework;

namespace AOUT.CH7.LogAn.Tests
{
    [TestFixture]
    public class MultipleAsserts
    {
        [Test]
        public void UsingMultipleAsserts_CanHarmYou()
        {
            Assert.AreEqual(3, Sum(1001, 1, 2));
            Assert.AreEqual(3, Sum(1, 1001, 2));
            Assert.AreEqual(3, Sum(1, 2, 1001));
        }

        [Test]
        public void Analyze_SimpleStringLine_UsesDefaulTabDelimiterToParseFields()
        {
            LogAnalyzer log = new LogAnalyzer();
            AnalyzedOutput output =
                log.Analyze("10:05\tOpen\tRoy");

            Assert.AreEqual(1,output.LineCount);
            Assert.AreEqual("10:05",output.GetLine(1)[0]);
            Assert.AreEqual("Open",output.GetLine(1)[1]);
            Assert.AreEqual("Roy",output.GetLine(1)[2]);
        }
        
        [Test]
        public void Analyze_SimpleStringLine_UsesDefaulTabDelimiterToParseFields2()
        {
            LogAnalyzer log = new LogAnalyzer();
            AnalyzedOutput expected = new AnalyzedOutput();
            expected.AddLine("10:05", "Open", "Roy");

            AnalyzedOutput output =
                log.Analyze("10:05\tOpen\tRoy");

            Assert.AreEqual(expected,output);
        }
        private int Sum(int x, int y, int z)
        {
            return 0;
        }
    }
}

public class PersonFactory
{
    public static Person GetDefault()
    {
        return new Person();
    }
    public static Person GetDefaultWithCellNumber(string number)
    {
        Person p = new Person();
        p.AddNumber(number);
        return new Person();
    }
}
