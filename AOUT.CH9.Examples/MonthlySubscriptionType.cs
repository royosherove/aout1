using AOUT.CH9.Examples.Interfaces;

namespace AOUT.CH9.Examples
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