namespace MyProduct.BusinessLayer
{
    public class MySingleton
    {
        private static MySingleton _instance;
        public static MySingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MySingleton();
                }

                return _instance;
            }
        }
    }
}
