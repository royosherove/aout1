namespace AOUT.CH3.LogAn
{
    public class LogAnalyzerPropertyStub
    {
        private IExtensionManager manager;

        public LogAnalyzerPropertyStub()
        {
            manager = new FileExtensionManager();
        }

        public IExtensionManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        public bool IsValidLogFileName(string fileName)
        {
            return manager.IsSupportedExtension(fileName);
        }
    }
}
