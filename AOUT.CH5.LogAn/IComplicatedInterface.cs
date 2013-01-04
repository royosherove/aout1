using System;
using System.Collections.Generic;
using System.Text;

namespace AOUT.CH5.LogAn
{
    public interface IComplicatedInterface
    {
        void Method1(string a, string b, bool c, int x, object o);
        void Method2(string b, bool c, int x, object o);
        void Method3(bool c, int x, object o);
    }
}
