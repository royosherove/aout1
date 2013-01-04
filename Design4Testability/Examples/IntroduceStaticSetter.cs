
public class MessageRouter
{
	private MessageRouter()
	{}
	
	private static readonly MessageRouter m_instance ;
	
	public static Config instance
	{
		if(m_instance==null)
		{
			m_instance = new MessageRouter();
		}
		return m_instance;
	}
	...
}


//method under test
void Route(Message msg)
{
	...
	MessageRouter.Instance().GetDispatcher().SendMessage(msg);
}

/*concerns: MessageRouter singleton might use various heavy system resources :
	 - tests will take a long time to run
	 - test will need an environment to run in
	 - hard to simulate various modes for the system

*/



//after	

public class MessageRouter
{
	...	
	protected MessageRouter()
	{
	}
	
	[Conditional("UNIT_TEST")]
	public static void SetTestingInstance(MessageRouter router)
	{
		m_instance = router;
	}
	
	public static Config instance
	{
		if(m_instance==null)
		{
			m_instance = new MessageRouter();
		}
		return m_instance;
	}
	...
}

public class FakeMessageRouter:MessageRouter
{
	public FakeMessageRouter()
	{
		
	}
}











