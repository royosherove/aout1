using System;

namespace MyProduct.BusinessLayer
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
