using System.IO;
using NUnit.Framework;

namespace AOUT.CH3.LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerUsingFactoryTests
    {
        [Test]
        public void IsValidFileName_NameShorterThan6CharsButSupportedExtension_ReturnsFalse()
        {
            //setup the stub to use, make sure it returns true
            Stub2ExtensionManager myFakeManager = new Stub2ExtensionManager();
            myFakeManager.ShouldBeValid = true;
            ExtensionManagerFactory.SetManagerInstance(myFakeManager);

            //create analyzer and inject stub
            LogAnalyzerConstructorStub log = 
                new LogAnalyzerConstructorStub();
            
            //Assert logic assuming extension is supprted
            bool result = log.IsValidLogFileName("short.ext");
            Assert.IsFalse(result,"File name with less than 5 chars should have failed the method, even if the extension is supported");
        }
    }

    internal class Stub2ExtensionManager : IExtensionManager
    {
        public bool ShouldBeValid;

        public bool IsSupportedExtension(string fileName)
        {
            return ShouldBeValid;
        }
    }
}
