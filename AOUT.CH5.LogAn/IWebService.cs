namespace AOUT.CH5.LogAn
{
    public interface IWebService
    {
        void LogError(string message);
        void LogInfo(string message);
        void LogError(TraceMessage message);
        void LogErrorEx(string message,string sender);
    }
}
