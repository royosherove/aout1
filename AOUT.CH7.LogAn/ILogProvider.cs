namespace LogAn
{
    public interface ILogProvider
    {
        string GetText(string file,int fromLine,int toLine);
        int GetLineCount();
    }
}