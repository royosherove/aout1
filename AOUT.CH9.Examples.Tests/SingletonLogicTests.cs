using NUnit.Framework;

namespace AOUT.CH9.Examples.Tests
{
    [TestFixture]
    public class SingletonLogicTests
    {
        [Test]
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
