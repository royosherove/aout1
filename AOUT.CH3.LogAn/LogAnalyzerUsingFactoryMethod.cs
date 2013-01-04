using System;
using System.IO;

namespace AOUT.CH3.LogAn
{
    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsSupportedExtension(fileName);
        }

        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }

        public bool IsValidLogFileName2(string fileName)
        {
            int len = Path.GetFileNameWithoutExtension(fileName).Length;
            return IsSupportedExtension(fileName) && len>5;
        }

        protected virtual bool IsSupportedExtension(string fileName)
        {
            return new FileExtensionManager().IsSupportedExtension(fileName);
        }
    }
}
