public class BankingServices
{
    public static void updateAccountBalance(int userID,
                                            Money amount) {
        ...
    }
    ...
}

public class bankManager
{
    public void setBankStatements(int id,Money startBalance, Money endBalance) {
        ...
        BankingServices.updateAccountBalance(id, sum);
    }
}

[Test]
public void Test_setBankStatements_NoUpdateOnNegativeFunds()
{
	BankManager bank = new BankManager();
	bank.setBankStatements(0,new Money(-1), new Money(-3));
	Assert....
}





//
//Adding an instance delegator method
//
public class BankingServices
{
    public static void updateAccountBalance(int userID, Money amount) 
    {
        ...
    }

    public virtual void updateBalance(int userID, Money amount) 
    {
        BankingServices.updateAccountBalance(userID, amount);
    }
    ...
}



//
//Changing the class under test 
//
public class SomeClass
{
	//instead, consider a public setter or parameterize constructor
    public void setBankStatements(BankingServices services,
    				int id,Money startBalance, Money endBalance) {
        ...
        services.updateBalance(id,sum);
    }
    ...
}

[Test]
public void Test_setBankStatements_NoUpdateOnNegativeFunds()
{
	FakeBankServices services = new FakeBankServices();
	BankManager bank = new BankManager();
	bank.setBankStatements(services,0,new Money(-1), new Money(-3));
	Assert....
}

public class FakeBankServices:BankingServices
{
	
	public override void updateBalance(int userID, Money amount) 
	{
		//do nothing
    	}
    
}


//OR PERHAPS: check for call

[Test]
public void Test_setBankStatements_NoUpdateOnNegativeFunds()
{
	FakeBankServices services = new FakeBankServices();
	BankManager bank = new BankManager();
	bank.setBankStatements(services,0,new Money(-1), new Money(-3));
	
	Assert.IsFalse(services.WasUpdateBalanceCalled);
}

public class FakeBankServices:BankingServices
{
	public bool wasUpdateCalled;
	
	public override void updateBalance(int userID, Money amount) 
	{
        	WasUpdateBalanceCalled = true;
    	}
    
}