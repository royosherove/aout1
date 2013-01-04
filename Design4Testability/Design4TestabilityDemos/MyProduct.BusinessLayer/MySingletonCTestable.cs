using Microsoft.Practices.Unity;

namespace MyProduct.BusinessLayer
{
    public class SingltonLogicC
    {
        public string SomeStringProperty { get; set; }
    }

    public class Resolver
    {
        private static IUnityContainer container = new UnityContainer();    
        public static T Get<T>()
        {
            return (T)container.Resolve(typeof (T)) ;
        }



        public static void ConfigureForSingleton()
        {
            container.RegisterType<SingltonLogicC>(new ContainerControlledLifetimeManager());
        }
    }
}
