using AOUT.CH9.Examples.Interfaces;
using AOUT.CH9.Examples.Interfaces;

namespace AOUT.CH9.Examples
{
    public class PersonLogicBTestable
    {
        protected IPersonValidator validator;

        public PersonLogicBTestable(IPersonValidator validator)
        {
            this.validator = validator;
        }

        public bool CanPurchase(Person p)
        {
            if (!(validator.IsValid(p)))
            {
                return false;
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
