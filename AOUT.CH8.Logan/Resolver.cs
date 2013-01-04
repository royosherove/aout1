using Castle.Windsor;

namespace AOUT.CH8.Logan
{
    public class Resolver
    {
        private static IWindsorContainer container;
        public static T Get<T>()
        {
            InitiContainer();
            return container.Resolve<T>();
        }

        private static void InitiContainer()
        {
            if (container==null)
            {
                container = new WindsorContainer();
            }
        }
    }
}
