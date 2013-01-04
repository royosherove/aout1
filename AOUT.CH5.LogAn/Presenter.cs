using System;
using System.Collections.Generic;
using System.Text;

namespace AOUT.CH5.LogAn
{
    public class Presenter
    {
        private IView view;
        private IWebService service;

        public Presenter(IView view)
        {
            this.view = view;
            view.Load += view_Load;
        }

        public Presenter(IView view,IWebService ws)
        {
            this.view = view;
            this.service= ws;
            view.Load += view_Load;
        }

        void view_Load(object sender, EventArgs e)
        {
            service.LogInfo("view loaded");
        }
    }
}
