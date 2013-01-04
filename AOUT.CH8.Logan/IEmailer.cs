namespace AOUT.CH8.Logan
{
    public interface IEmailer
    {
        void Send(string subject,string body,string to);
    }
}
