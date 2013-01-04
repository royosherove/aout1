using System;
using System.Collections.Generic;
using System.Text;

namespace AOUT.CH5.LogAn
{
    public interface IView
    {
        event EventHandler Load;
    }
}
