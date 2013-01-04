namespace AOUT.CH3.LogAn
{
    public class LogAnalyzerConstructorStub
    {
        private IExtensionManager manager;

        public LogAnalyzerConstructorStub()
        {
            manager = new FileExtensionManager();
        }
        public LogAnalyzerConstructorStub(IExtensionManager extentionMgr)
        {
            manager = extentionMgr;
        }

        public bool IsValidLogFileName(string fileName)
        {
            return manager.IsSupportedExtension(fileName);
        }
    }
}
