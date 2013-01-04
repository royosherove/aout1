//Extract and override factory method
public class WorkflowEngine
{
    public WorkflowEngine () 
    {
        Reader reader
            = new ModelReader(ConfigurationSettings.AppSettings["DryConfiguration"]);

        Persister persister
            = new XMLStore(ConfigurationSettings.AppSettings["DryConfiguration"]);

        this.tm = new TransactionManager(reader, persister);
        ...
    }
    ...
}

/*concerns: TransactionManager class is created without our control
	 - tests might take a long time to run
	 - test will need an environment to run in
	 - hard to simulate various modes for the system

*/




//EXTRACT the Factory method
public class WorkflowEngine
{
    public WorkflowEngine () 
    {
        this.tm = this.makeTransactionManager();
        ...
    }

    //this is the factory method we will override
    protected virtual TransactionManager makeTransactionManager() 
    {
        Reader reader
            = new ModelReader(ConfigurationSettings.AppSettings["DryConfiguration"]);

        Persister persister
            = new XMLStore(ConfigurationSettings.AppSettings["DryConfiguration"]);

        return new TransactionManager(reader, persister);
    }
    ...
}


//our test class
public class TestWorkflowEngine : WorkflowEngine
{
    protected override TransactionManager makeTransactionManager() 
    {
        return new FakeTransactionManager();
    }
}


	[Test]
	public void Test_WorkFlowEngine_Something()
	{
		TestWorkflowEngine engine = new TestWorkflowEngine();
		...
	}


