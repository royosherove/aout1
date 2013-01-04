using System;

namespace MyProduct.BusinessLayer
{

    public class Calc
    {
        public int GetSomeNumber(string key)
        {
            if(key.Contains(" "))
            {
                throw new NotImplementedException("dude, no spaces please");
            }
            if(key==String.Empty)
            {
                return 0;
            }
            return Math.Abs(int.Parse(key));
        }
    }
}
