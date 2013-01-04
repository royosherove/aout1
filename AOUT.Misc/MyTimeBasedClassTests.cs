using System;
using NUnit.Framework;

namespace AOUT.Misc
{
    [TestFixture]
    public class MyTimeBasedClassTests
    {
        [Test]
        public void ShouldBackupNow_LessThan7DaysAway_ReturnsFalse()
        {
            TestableMyTimeBasedClass backup = new TestableMyTimeBasedClass();
            backup.LastBackup = new DateTime(2000,1,1);
            backup.CurrentTime = new DateTime(2000,1,5);

            bool backupNow = backup.ShouldBackupNow();
            Assert.IsFalse(backupNow);
        }
        
        [Test]
        public void ShouldBackupNow_MoreThan7DaysAway_ReturnsTrue()
        {
            TestableMyTimeBasedClass backup = new TestableMyTimeBasedClass();
            backup.LastBackup = new DateTime(2000,1,1);
            backup.CurrentTime = new DateTime(2000,1,9);

            bool backupNow = backup.ShouldBackupNow();
            Assert.IsTrue(backupNow);
        }
    }


    class TestableMyTimeBasedClass:MyTimeBasedClass
    {
        public DateTime CurrentTime;

        protected override DateTime GetTimeNow()
        {
            return CurrentTime;
        }
    }
}
