using System;

namespace AOUT.Misc
{
    public class MyTimeBasedClass
    {
        public bool ShouldBackupNow()
        {
            DateTime now = GetTimeNow();
            return now.Subtract(lastBackup).Days > 7;
        }

        protected virtual DateTime GetTimeNow()
        {
            return DateTime.Now;
        }

        private DateTime lastBackup;

        public DateTime LastBackup
        {
            get { return lastBackup; }
            set { lastBackup = value; }
        }
    }
}
