namespace AOUT.CH3.LogAn
{
    public class LogAnalyzerUsingFactory
    {
        private IExtensionManager manager;

        public LogAnalyzerUsingFactory()
        {
            manager = ExtensionManagerFactory.Create();
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
