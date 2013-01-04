//BEFORE
public class ARMDispatcher
{
    public void populate(HttpServletRequest request) 
    {
        String [] values= request.getParameterValues(pageStateName);
        if (values != null && values.length  > 0)
        {
            marketBindings.put(pageStateName + getDateStamp(),values[0]);
        }
        ...
    }
    ...
}

/*concerns: HttpServletRequest might use various heavy system resources :
	 - tests will take a long time to run
	 - test will need an environment to run in
	 - hard to simulate various modes for the system
	 - hard to test the "populate" method

*/





//AFTER
public class ARMDispatcher
    public void populate(IParameterSource source) 
    {
        String values = source.getParameterForName(pageStateName);
        if (value != null) 
        {
            marketBindings.put(pageStateName + getDateStamp(),value);
        }
        ...
    }
}

//class ServletParameterSource 
class ServletParameterSource implements IParameterSource
{
    private HttpServletRequest request;

    public ServletIParameterSource(HttpServletRequest request) {
        this.request = request;
    }

    String getParameterValue(String name) {
        String [] values = request.getParameterValues(name);
        if (values == null || values.length < 1)
            return null;
        return values[0];
    }
}



//testing

//class FakeParameterSource 
class FakeParameterSource implements IParameterSource
{
    public String valueToReturn;

    public String getParameterForName(String name) {
        return valueToReturn;
    }
}

public void testSomething()
{
	ARMDispatcher disp = new ARMDispatcher();
	FakeParameterSource source = new FakeParameterSource();
	source.valueToReturn="myReturnValue";
	
	disp.populate(source);
	...
}



