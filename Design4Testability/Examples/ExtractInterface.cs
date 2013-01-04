public class Storage
{
	private EmailSender m_email =null;
	public Storage(EmailSender email)
	{
		m_email = email;
	}	
	
	public bool Save(DataObject data)
	{
		...
		if(data.Size>MAX_SIZE)
		{
			m_email.SendAsync("Large object detected, could not save!" 
				+ data.GetFullDetails().ToString());
		
			return false;
		}
		...
		return true;
	} 
}


[TestFixture]
public class StorageTests
{
	[Test]
	public void Test_Save_ReturnsFalseOnLargeObject()
	{
		Storage store = new Storage(new EmailSender());
		bool result = store.Save(getLargeDataObject());
		Assert.IsFalse(result);
	}
	
	//how do you test that an email has been sent correctly?
	
}










//after extract interface
public class Storage
{
	private IEmailSender m_email =null;
	public Storage(IEmailSender email)
	{
		m_email = email;
	}	
	
	public bool Save(DataObject data)
	{
		...
		if(data.Size>MAX_SIZE)
		{
			m_email.SendAsync("Large object detected, could not save!" 
				+ data.GetFullDetails().ToString());
		
			return false;
		}
	...
	} 
}

	[Test]
	public void Test_Save_ReturnsFalseOnLargeObject()
	{
		Storage store = new Storage(new FakeEmailSender());
		bool result = store.Save(getLargeDataObject());
		Assert.IsFalse(result);
	}
	
	public class FakeEmailSender:IEmailSender
	{
		public void SendAsync(string message)
		{
			//do absolutely nothing
		}	
	}
	
}
