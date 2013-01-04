using AOUT.CH3.LogAn;
using NUnit.Framework;

namespace AOUT.CH3.LogAn.Tests
{
    [TestFixture]
    public class LogAnalyzerUsingFactoryMethodTests
    {
        [Test]
        public void overrideTestWithStub()
        {
            Stub3ExtensionManager stub = new Stub3ExtensionManager();
            stub.ShouldBeValid = false;

            TestableLogAnalyzer logan = new TestableLogAnalyzer();
            logan.Manager=stub;
            bool result = logan.IsValidLogFileName("file.ext");
            Assert.IsFalse(result,"File name should be too short to be considered valid");
        }
        
        [Test]
        public void overrideTestWithoutStub()
        {
            TestableLogAnalyzerWithNoStub logan = new TestableLogAnalyzerWithNoStub();
            logan.IsSupported = false;

            bool result = logan.IsValidLogFileName("file.ext");
            Assert.IsFalse(result,"File name should be too short to be considered valid");
        }
    }

    class  TestableLogAnalyzer:LogAnalyzerUsingFactoryMethod
    {
        public IExtensionManager Manager;

        protected override IExtensionManager GetManager()
        {
            return Manager;
        }
    }
    internal class Stub3ExtensionManager : IExtensionManager
    {
        public bool ShouldBeValid;

        public bool IsSupportedExtension(string fileName)
        {
            return ShouldBeValid;
        }
    }
    
    class TestableLogAnalyzerWithNoStub : LogAnalyzerUsingFactoryMethod
    {
        public bool IsSupported;
        
        protected override bool IsSupportedExtension(string fileName)
        {
            return IsSupported;
        }
    }
}
