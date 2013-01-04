using NUnit.Framework;
using Rhino.Mocks;

namespace AOUT.CH8.Logan.Tests
{
    [TestFixture]
    public class MessageHandlerTestsWithContainer
    {
        [Test]
        public void CreatehandlerUsingMockingContainer()
        {
            MockingContainer container = new MockingContainer(new MockRepository());
            MessageHandler mw = container.ResolveRealObject<MessageHandler>();
            Assert.IsNotNull(mw);
        }
        
        [Test]
        public void Handle_SimpleMessage_ReturnsTrue()
        {
            MockingContainer container = new MockingContainer(new MockRepository());
            MessageHandler mw = container.ResolveRealObject<MessageHandler>();
            bool handle = mw.Handle("a");
            Assert.IsTrue(handle);
        }
        
        
        [Test]
        public void Handle_SimpleMessage_SendEmail()
        {
            MockRepository mocks = new MockRepository();
            MockingContainer container = new MockingContainer(mocks);
            MessageHandler mw = container.ResolveRealObject<MessageHandler>();
            using(mocks.Record())
            {
                container.Resolve<IEmailer>()
                    .Send("message","a","admin@admin.com");
            }
            bool handle = mw.Handle("a");
            mocks.VerifyAll();
        }
    }
}
