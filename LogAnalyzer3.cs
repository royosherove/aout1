namespace AOUT.CH3.LogAn
{
    public class LogAnalyzer3
    {
        private bool wasLastFileNameValid;

        public bool WasLastFileNameValid
        {
            get { return wasLastFileNameValid; }
            set { wasLastFileNameValid = value; }
        }

        public bool IsValidLogFileName(string fileName)
        {
            if (!fileName.ToLower().EndsWith(".slf"))
            {
                
                return false;
            }


            return true;
        }
    }
}
