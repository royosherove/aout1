using System;

namespace AOUT.Ch1.Examples.Tests
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                SimpleParserTests.TestReturnsZeroWhenEmptyString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
