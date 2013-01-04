//Parameterize constructor
public class MailChecker
{
    public MailChecker (int checkPeriodSeconds) 
    {
        this.receiver = new MailReceiver();
        this.checkPeriodSeconds = checkPeriodSeconds;
    }
    ...
}

/*concerns: MailReciever class may take a long time to process :
	 - tests will take a long time to run
	 - test will need an environment to run in
	 - hard to test the logic for MailChecker class

*/










//Adding a new constructor
public class MailChecker
{
    public MailChecker (int checkPeriodSeconds) 
    {
        this.receiver = new MailReceiver();
        this.checkPeriodSeconds = checkPeriodSeconds;
    }

    public MailChecker (MailReceiver receiver,
                        int checkPeriodSeconds) 
	{
        this.receiver = receiver;
        this.checkPeriodSeconds = checkPeriodSeconds;
    	}
    ...
}
