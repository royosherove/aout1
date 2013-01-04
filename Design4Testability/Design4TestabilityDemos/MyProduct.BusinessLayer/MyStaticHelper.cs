using System;
using System.Threading;

namespace MyProduct.BusinessLayer
{
    public static class MyStaticHelper
    {
        public static void LogPersonCreate(Person person)
        {
            string error = "Log file not configured!";
            Thread.Sleep(2 * 1000);
            throw new Exception(error);
        }

        public static bool CheckValue(Person p)
        {
            return false;
        }
    }
}
