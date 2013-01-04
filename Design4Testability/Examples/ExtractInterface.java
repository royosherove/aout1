//Extract interface 



//We have our test case:
void testPayday()
{
    Transaction t = new PaydayTransaction(getTestingDatabase());
    t.run();

    assertEquals(getSampleCheck(12),
                 getTestingDatabase().findCheck(12));
}

//But we have to pass in some sort of a transactionLog 
//to make it compile. Let's create a call to a class 
//that doesn't exist yet, FakeTransactionLog.
void testPayday()
{
    FakeTransactionLog aLog = new FakeTransactionLog();
    Transaction t = new PaydayTransaction(
                            getTestingDatabase(),
                            aLog);
    t.run();

    assertEquals(getSampleCheck(12),
                 getTestingDatabase().findCheck(12));
}


interface TransactionRecorder
{
}

//Now we move back and make transactionLog implement the new interface.
public class TransactionLog implements TransactionRecorder
{
   ...
}

//Next we create FakeTransactionLog as an empty class, too.
public class FakeTransactionLog implements TransactionRecorder
{
}

//Everything should compile fine because all we've done is introduce a few new classes and change a class so that it implements an empty interface.
//At this point, we launch into the refactoring full force. We change the type of each reference in the places where we want to use the interface. PaydayTransaction uses a TRansactionLog; we need to change it so that it uses a transactionRecorder. When we've done that, when we compile, we find a bunch of cases in which methods are being called from a transactionRecorder, and we can get rid of the errors one by one by adding method declarations to the transactionRecorder interface and empty method definitions to the FakeTransactionLog.
//Here's an example:
public class PaydayTransaction extends Transaction
{
    public PaydayTransaction(PayrollDatabase db,
                             TransactionRecorder log) {
        super(db, log);
    }

    public void run() {
        for(Iterator it = db.getEmployees(); it.hasNext(); ) {
            Employee e = (Employee)it.next();
            if (e.isPayday(date)) {
                e.pay();
            }
        }
        log.saveTransaction(this);
    }
    ...
}

//In this case, the only method that we are calling on transactionRecorder is saveTransaction. Because transactionRecorder doesn't have a saveTransaction method yet, we get a compile error. We can make our test compile just by adding that method to TRansactionRecorder and FakeTransactionLog.
interface TransactionRecorder
{
    void saveTransaction(Transaction transaction);
}

public class FakeTransactionLog implements TransactionRecorder
{
    void saveTransaction(Transaction transaction) {
    }
}
