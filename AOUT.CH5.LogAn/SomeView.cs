using System;
using System.Collections.Generic;
using System.Text;

namespace AOUT.CH5.LogAn
{
    public class SomeView:IView
    {
        public event EventHandler Load;

        public void TriggerLoad(object o, EventArgs args)
        {
            Load(o,args);

        }
    }
}
