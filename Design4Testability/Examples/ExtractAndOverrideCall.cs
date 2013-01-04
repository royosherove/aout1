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






//after extract and override
public class Storage
{
	...	
	public bool Save(DataObject data)
	{
		...
		if(data.Size>MAX_SIZE)
		{
			sendEmail("Large object detected, could not save!" 
				+ data.GetFullDetails().ToString());
		
			return false;
		}
		...
		return true;
	} 
	
	protected virtual void sendEmail(string message)
	{
		m_email.SendAsync(message);
	}
}



	public class TestReadyStorage:Storage
	{

		//Inherit constructor...
		...
		
		protected override void sendEmail(string message)
		{
			//do absolutely nothing
		}	
	}
	
	
	public void test_Save_ReturnsFalseOnLargeObject()
	{
		TestReadyStorage store = new TestReadyStorage(new FakeEmailSender());
		bool result = store.Save(getLargeDataObject());
		Assert.IsFalse(result);
	}
	
	
	
}
