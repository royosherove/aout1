using System;
using AOUT.CH5.LogAn;
using AOUT.CH5.Logan.Tests;
using NUnit.Framework;
using NUnit.Mocks;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Rhino.Mocks.Impl;
using Rhino.Mocks.Interfaces;

namespace AOUT.Ch5.LogAn.Test
{
    [TestFixture]
    public class LogAnalyzerTestsUsingRhinoMocks
    {
       
        [Test]
        public void SimpleRhinoStub()
        {
            MockRepository mockEngine = new MockRepository();
            IWebService simulatedService = mockEngine.StrictMock<IWebService>();
            simulatedService.LogError("calling method on simulated object");
        }
        
        [Test]
        public void CreateMock_RecordSimpleExpectation_()
        {
            MockRepository mockEngine = new MockRepository();
            IWebService simulatedService = mockEngine.DynamicMock<IWebService>();
            using(mockEngine.Record())
            {
                simulatedService.LogError("expected input");
            }

            simulatedService.LogError("expected input");
            mockEngine.VerifyAll();
        }
        
        [Test]
        public void CreateMock_WithReplayAll()
        {
            MockRepository mockEngine = new MockRepository();
            IWebService simulatedService = mockEngine.DynamicMock<IWebService>();
            using (mockEngine.Record())
            {
                simulatedService.LogError("expected input");
            }
            LogAnalyzer log = new LogAnalyzer(simulatedService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);

            mockEngine.VerifyAll();
        }

        [Test]
        public void CreateMock_WithReplayAll_AAA()
        {
            MockRepository mockEngine = new MockRepository();
            IWebService simulatedService = mockEngine.DynamicMock<IWebService>();
            LogAnalyzer log = new LogAnalyzer(simulatedService);
            mockEngine.ReplayAll();
            log.Analyze("abc.ext");

            simulatedService.AssertWasCalled(svc => svc.LogError("Filename too short:abc.ext"));
        }
        
        [Test]
        public void StubThatThrowsException_RhinoMocks()
        {
            IWebService simulatedService = MockRepository.GenerateStub<IWebService>();
            simulatedService
                .Expect(t => t.LogError(""))
                .Throw(new Exception("fake exception"))
                .Constraints(Is.Anything());
            
            LogAnalyzer log = new LogAnalyzer(simulatedService);
            log.Analyze("abc.ext");
        }
        
        
        
        [Test]
        public void ReturnResultsFromMock()
        {
            MockRepository repository = new MockRepository();
            IGetRestuls resultGetter = repository.DynamicMock<IGetRestuls>();
            using(repository.Record())
            {
                resultGetter.GetSomeNumber("a");
                LastCall.Return(1);
                
                resultGetter.GetSomeNumber("a");
                LastCall.Return(2);
                
                resultGetter.GetSomeNumber("b");
                LastCall.Return(3);
                
            }

            int result = resultGetter.GetSomeNumber("b");
            Assert.AreEqual(3, result);
            
            int result2 = resultGetter.GetSomeNumber("a");
            Assert.AreEqual(1, result2);
            
            int result3 = resultGetter.GetSomeNumber("a");
            Assert.AreEqual(2, result3);
        }

        [Test]
        public void ReturnResultsFromStub()
        {
            MockRepository repository = new MockRepository();
            IGetRestuls resultGetter = repository.Stub<IGetRestuls>();
            using(repository.Record())
            {
                resultGetter.GetSomeNumber("a");
                LastCall.Return(1);
                
            }

            int result = resultGetter.GetSomeNumber("a");
            Assert.AreEqual(1, result);
            
        }
        
        
        [Test]
        public void StubNeverFailsTheTest()
        {
            MockRepository repository = new MockRepository();
            IGetRestuls resultGetter = repository.Stub<IGetRestuls>();
            using(repository.Record())
            {
                resultGetter.GetSomeNumber("A");
                LastCall.Return(1);
                
            }
            resultGetter.GetSomeNumber("B");
            repository.VerifyAll();
        }
        
        public void StubSimulatingException()
        {
            MockRepository repository = new MockRepository();
            IGetRestuls resultGetter = repository.Stub<IGetRestuls>();
            using(repository.Record())
            {
                resultGetter.GetSomeNumber("A");
                LastCall.Throw(new OutOfMemoryException("The system is out of memory!"));
            }
            resultGetter.GetSomeNumber("A");
        }
        [Test]
        public void ConstraintsAgainstObjectPropeties()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockservice = mocks.CreateMock<IWebService>();
            using (mocks.Record())
            {
                mockservice.LogError(new TraceMessage("",0,""));
                LastCall.Constraints(
                    Property.Value("Message", "expected message") &&
                    Property.Value("Severity", 100)               &&
                    Property.Value("Source", "Some Source"));
            }
            mockservice.LogError(new TraceMessage("expected message", 100, "Some Source"));
            mocks.VerifyAll();
        }

        [Test]
        public void ComplexConstraintsWithCallbacks()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockservice = mocks.CreateMock<IWebService>();
            using (mocks.Record())
            {
                mockservice.LogError(new TraceMessage("", 0, ""));
                LastCall.Constraints(
                    Is.Matching<ComplexTraceMessage>(verifyComplexMessage));
            }
        }

        private bool verifyComplexMessage(ComplexTraceMessage msg)
        {
            if (msg.InnerMessage.Severity < 50
                && msg.InnerMessage.Message.Contains("a"))
            {
                return false;
            }
            return true;
                                                                
        }

        [Test]
        public void VerifyAttachesToViewEvents()
        {
            MockRepository mocks = new MockRepository();
            IView viewMock = (IView)mocks.CreateMock(typeof(IView));
            using (mocks.Record())
            {
                viewMock.Load += null;
                LastCall.IgnoreArguments();
            }
            new Presenter(viewMock);
            mocks.VerifyAll();
        }
        
        [Test]
        public void TriggerAndVerifyRespondingToEvents()
        {
            MockRepository mocks = new MockRepository();
            IView viewStub = mocks.Stub<IView>();
            IWebService serviceMock = mocks.CreateMock<IWebService>();
            using (mocks.Record())
            {
                serviceMock.LogInfo("view loaded");
            }
            new Presenter(viewStub,serviceMock);
            IEventRaiser eventer = new EventRaiser((IMockedObject) viewStub, "Load");
            eventer.Raise(null,EventArgs.Empty);

            mocks.VerifyAll();
        }

        [Test]
        public void TestingObjectPropertiesWithObjects()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockservice = mocks.CreateMock<IWebService>();
            using (mocks.Record())
            {
                mockservice.LogError(new TraceMessage("Some Message",100,"Some Source"));
            }
            mockservice.LogError(new TraceMessage("Some Message",100,"Some Source"));
            mocks.VerifyAll();
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            MockRepository mocks = new MockRepository();
            IWebService stubService = mocks.Stub<IWebService>();
            IEmailService mockEmail =mocks.CreateMock<IEmailService>();

            using(mocks.Record())
            {
                stubService.LogError("whatever");
                LastCall.Constraints(Is.Anything());
                LastCall.Throw(new Exception("fake exception"));

                mockEmail.SendEmail("a","subject","fake exception");
            }

            LogAnalyzer2 log = new LogAnalyzer2();
            log.Service = stubService;
            log.Email = mockEmail;

            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            
            mocks.VerifyAll();
        }

        [Test]
        public void SimpleStringConstraints()
        {
            MockRepository mocks = new MockRepository();
            IWebService mockService = mocks.CreateMock<IWebService>();
            using (mocks.Record())
            {
                mockService.LogError("ignored string");
                LastCall.Constraints(new Contains("abc"));
            }

            mockService.LogError(Guid.NewGuid() + " abc");
            mocks.VerifyAll();
        }


        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            DynamicMock mockController = new DynamicMock(typeof (IWebService));
            mockController.Expect("LogError", "Filename too short:abc.ext");
            
            IWebService mockService = mockController.MockInstance as IWebService;
            
            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName="abc.ext";
            log.Analyze(tooShortFileName);

            mockController.Verify();
        }

        [Test]
        public void EventFiringManual()
        {
            bool loadFired = false;

            SomeView view = new SomeView();
            view.Load+=delegate
                           {
                                loadFired = true;
                           };
            view.TriggerLoad(null,null);
            Assert.IsTrue(loadFired);
        }

        [Test]
        public void EventFiringWithEventsVerifier()
        {
            EventsVerifier verifier = new EventsVerifier();
            SomeView view = new SomeView();
            verifier.Expect(view, "Load",null,EventArgs.Empty);
            view.TriggerLoad(null, EventArgs.Empty);

            verifier.Verify();
        }

        [Test]
        public void UsingStubsWithProperties()
        {
            MockRepository mocks = new MockRepository();
            ISomeInterfaceWithProperties stub =
                mocks.Stub<ISomeInterfaceWithProperties>();

            stub.Age = 1;
            stub.Name = "Itamar";
            Assert.AreEqual("Itamar",stub.Name);
        }

        [Test]
        public void UsingStubsWithCustomPropertyValues()
        {
            MockRepository mocks = new MockRepository();
            ISomeInterfaceWithProperties stub =
                mocks.Stub<ISomeInterfaceWithProperties>();
            stub.Age = 10; //a stub actually implements standard properties
            Assert.AreEqual(10, stub.Age);
        }

    }



}
