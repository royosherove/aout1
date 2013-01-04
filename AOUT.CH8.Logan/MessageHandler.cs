namespace AOUT.CH8.Logan
{
    public class MessageHandler
    {
        private readonly IEmailer emailer;
        private readonly ILogger logger;

        public MessageHandler(IEmailer emailer, ILogger logger)
        {
            this.emailer = emailer;
            this.logger = logger;
        }

        public virtual bool Handle(string message)
        {
            return true;
        }
    }
}
