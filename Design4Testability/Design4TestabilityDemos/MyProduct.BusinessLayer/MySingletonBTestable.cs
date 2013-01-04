namespace MyProduct.BusinessLayer
{
    public class MySingletonBHolder
    {
        private static RealSingletonLogic _instance;
        public static RealSingletonLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RealSingletonLogic();
                }

                return _instance;
            }
        }
    }

    public class RealSingletonLogic
    {
        public void Foo()
        {
            //lots of logic here
        }
    }
}
