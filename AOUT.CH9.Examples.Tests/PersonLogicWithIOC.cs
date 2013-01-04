using Castle.Windsor;
using AOUT.CH9.Examples.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace AOUT.CH9.Examples.Tests
{
    [TestFixture]
    public class PersonLogicWithIOCTests
    {
        [Test]
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



        
        [Test]
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
