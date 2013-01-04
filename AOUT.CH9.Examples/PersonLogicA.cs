using System;
using System.Collections;
using System.Diagnostics;

namespace AOUT.CH9.Examples
{
    public class PersonLogicA
    {
        public bool CanPurchase(Person p)
        {
            
            if (!(PersonValidatorStatic.IsValid(p)))
            {
                throw new Exception("validation failed");
            }
            if (p.SSID!=null &&
                p.SubscriptionType!=null &&
                p.CreditOnFile>0)
            {
                return true;
            }
            return false;
        }
    }
}
