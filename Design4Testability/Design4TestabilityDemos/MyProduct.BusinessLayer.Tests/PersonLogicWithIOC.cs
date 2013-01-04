using Castle.Windsor;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProduct.BusinessLayer.Interfaces;

namespace MyProduct.BusinessLayer.Tests
{
    [TestClass]
    public class PersonLogicWithIOCTests
    {
        [TestMethod]
        public void IOC_Castle()
        {
            IWindsorContainer container = new WindsorContainer();
                container.AddComponent<PersonLogicBTestable>();
                container.AddComponent<ILogger,FakeLogger>();
                container.AddComponent<IPersonValidator,MyFakeValidator>();
            
            PersonLogicBTestable logic =
                container.Resolve<PersonLogicBTestable>();

            bool canPurchase = logic.CanPurchase(new Person());
            Assert.IsFalse(canPurchase);
        }



        
        [TestMethod]
        public void IOC_Unity()
        {
            IUnityContainer unityContainer = new UnityContainer();
            unityContainer
                .RegisterType<ILogger, FakeLogger>()
                .RegisterType<IPersonValidator, FakeTestingValidator>();

            PersonLogicBTestable logic =
                unityContainer.Resolve<PersonLogicBTestable>();

            bool canPurchase = logic.CanPurchase(new Person());
            Assert.IsFalse(canPurchase);
        }
    }

    class MyFakeValidator:IPersonValidator
    {
        public bool IsValid(Person p)
        {
            return true;
        }

        public MyFakeValidator()
        {
        }
    }

    class FakeLogger:ILogger
    {
        public FakeLogger()
        {
        }

        public void Log(string text)
        {
            //do nothing
        }
    }
}
