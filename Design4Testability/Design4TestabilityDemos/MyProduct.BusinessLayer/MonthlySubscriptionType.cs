using MyProduct.BusinessLayer.Interfaces;

namespace MyProduct.BusinessLayer
{
    public class MonthlySubscriptionType:ISubscriptionType
    {
        #region ISubscriptionType Members

        public string name
        {
            get { return "Monthly"; }
        }

        #endregion
    }
}
