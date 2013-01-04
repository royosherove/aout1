using System;
using System.Reflection;
using AOUT.CH1.Examples;

namespace AOUT.Ch1.Examples.Tests
{
    class SimpleParserTests
    {
        public static void TestReturnsZeroWhenEmptyString()
        {
            //use reflection to get the current method's name
            string testName = MethodBase.GetCurrentMethod().Name;
            try
            {
                SimpleParser p = new SimpleParser();
                int result = p.ParseAndSum("1");
                if(result!=0)
                {
                    TestUtil.ShowProblem(testName, "Parse and sum should have returned 0 on an empty string");
                }
            }
            catch (Exception e)
            {
                TestUtil.ShowProblem(testName, e.ToString());
            }
        }
    }
}
