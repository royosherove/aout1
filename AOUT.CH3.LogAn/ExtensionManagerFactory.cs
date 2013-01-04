namespace AOUT.CH3.LogAn
{
    public class ExtensionManagerFactory
    {
        private static IExtensionManager m_manager
                        =new FileExtensionManager();

        public static IExtensionManager Create()
        {
            return m_manager;
        }

        public static void SetManagerInstance(IExtensionManager value)
        {
            m_manager = value;
        }
    }
}
