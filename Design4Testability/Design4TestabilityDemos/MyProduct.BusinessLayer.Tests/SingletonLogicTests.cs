using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyProduct.BusinessLayer.Tests
{
    [TestClass]
    public class SingletonLogicTests
    {
        [TestMethod]
        public void MySingletonC_GottenTwice_ReturnsSameInstance()
        {
            Resolver.ConfigureForSingleton();
            SingltonLogicC c1 = Resolver.Get<SingltonLogicC>();
            SingltonLogicC c2 = Resolver.Get<SingltonLogicC>();
            
            c1.SomeStringProperty = "aaa";
            
            Assert.AreSame(c1,c2);
            Assert.AreEqual(c1.SomeStringProperty,
                            c2.SomeStringProperty);
        }
    }
}
