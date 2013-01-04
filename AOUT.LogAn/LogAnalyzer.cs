using System;
using System.IO;

namespace AOUT.CH2.LogAn
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception("No log file with that name exists");
            }
            if(!fileName.ToLower().EndsWith(".slf"))
            {
                return false;
            }
           

            return true;
        }
    }
}
