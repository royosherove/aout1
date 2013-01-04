using System.Configuration;

namespace MyProduct.BusinessLayer
{
    public class MyConfigBasedClass
    {
        public bool IsConnectionStringValid()
        {
            string connString =
                getConnectionString("conString");

            //do some stuff
            //...
            return connString.Contains("catalog");
        }

        protected virtual string getConnectionString(string constring)
        {
            return ConfigurationSettings.AppSettings[constring];
        }
    }
}
