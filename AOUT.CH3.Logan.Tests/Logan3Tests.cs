using AOUT.CH3.LogAn;
using NUnit.Framework;

namespace AOUT.CH3.LogAn.Tests
{
    [TestFixture]
    public class Logan3Tests
    {
        [Test]
        public void IsValidFileName_ValidName_RemembersTrue()
        {
            LogAnalyzer3 log = new LogAnalyzer3();
            log.IsValidLogFileName("somefile.slf");
            Assert.IsTrue(log.WasLastFileNameValid);
        }
    }
}
