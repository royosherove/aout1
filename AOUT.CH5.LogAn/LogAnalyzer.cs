namespace AOUT.CH5.LogAn
{
    public class LogAnalyzer
    {
        public delegate void AnalyzedDelegate(string msg);
        public event AnalyzedDelegate Analyzed=delegate {};

        private IWebService service;

        public LogAnalyzer(IWebService service)
        {
            this.service = service;
        }

        public void Analyze(string fileName)
        {
            if(fileName.Length<8)
            {
                service.LogError("Filename too short:" + fileName);
                service.LogErrorEx("aa","a");
            }
            Analyzed(fileName);
        }
    }
}
