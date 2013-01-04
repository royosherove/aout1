using System;

namespace AOUT.CH9.Examples.Interfaces
{
    
    public class MyOverridableClass
    {
        public Func<int,int> calculateMethod=delegate(int i)
                                                  {
                                                      return i*2;
                                                  };
        public void DoSomeAction(int input)
        {
            int result = calculateMethod(input);
            if (result==-1)
            {
                throw new Exception("input was invalid");
            }
            //do some other work
        }
    }
}
