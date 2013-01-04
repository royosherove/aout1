using AOUT.CH5.LogAn;

namespace AOUT.CH5.Logan.Tests
{
    public class MytestableComplicatedInterface:IComplicatedInterface
    {
        public string meth1_a;
        public string meth1_b,meth2_b;
        public bool meth1_c,meth2_c,meth3_c;
        public int meth1_x,meth2_x,meth3_x;
        public int meth1_0,meth2_0,meth3_0;

        public void Method1(string a, string b, bool c, int x, object o)
        {
            meth1_a = a;   
            meth1_b = b;   
            meth1_c = c;   
            meth1_x = x;   
            meth1_0 = 0;   
        }

        public void Method2(string b, bool c, int x, object o)
        {
            meth2_b = b;
            meth2_c = c;
            meth2_x = x;
            meth2_0 = 0;
        }

        public void Method3(bool c, int x, object o)
        {
            meth3_c = c;
            meth3_x = x;
            meth3_0 = 0;
        }
    }
}
