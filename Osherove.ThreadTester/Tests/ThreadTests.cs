using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Osherove.ThreadTester.Strategies;

namespace Osherove.ThreadTester.Tests
{
    [TestFixture]
    public class ThreadTests
    {

        class Singlton
        {
            public Guid guid;
            private static Singlton instance;

            public Guid Guid
            {
                get { return guid; }
            }

            public Singlton()
            {
                guid = Guid.NewGuid();
            }
            static object sync = new object();
            public static Singlton Instance
            {
                get
                {
//                    Monitor.Enter(sync);
                    if(instance==null)
                    {
                        Thread.Sleep(new Random(15).Next(10,100));
                        instance=new Singlton();
                    }
//                    Monitor.Exit(sync);
                    return instance;
                }
            }
        }
        class Counter
        {
            public string tempo;
            private int count = 0;
            public void Increment()
            {
                count++;
                int x = count*50;
                string temp = x.ToString() + Guid.NewGuid().ToString();
                this.tempo = temp;
            }


            public int Count
            {
                get { return count; }
            }
        }
        
        class CounterEx
        {
            private int count = 0;
            public void Increment()
            {
                count++;

//                Console.WriteLine("Incremented to " + count);
            }

            public int Count
            {
                get { return count; }
            }
        }

        [Test]
        public void Singlton_MultiThreaded_SameInstance()
        {
            ThreadTester tt = new ThreadTester();
            Guid guid1 = Guid.Empty, 
                guid2 = Guid.Empty;

            tt.AddThreadAction(delegate
                                   {
                                       guid1 = Singlton.Instance.guid;
                                   });
            tt.AddThreadAction(delegate
                                   {
                                       guid2 = Singlton.Instance.guid;
                                   });

            tt.StartAllThreads(1000);
            Assert.AreEqual(guid1.ToString(),guid2.ToString());
        }
        [Test]
        public void SingleThread()
        {
            Counter c = new Counter();
            ThreadTester tt = new ThreadTester();
            tt.AddThreadAction(
                delegate
                    {
                        c.Increment();
                    });

            tt.RunBehavior=ThreadRunBehavior.RunUntilAllThreadsFinish;
            tt.StartAllThreads(500);
            Assert.IsTrue(tt.LastRunTime<tt.TimeOut);
        }
        
        
        [Test]
        public void SingleThreadWithVerifier()
        {
            CounterEx c = new CounterEx();
            ThreadTester tt = new ThreadTester();
            tt.AddThreadAction(
                delegate
                    {
                        c.Increment();
                    });

            tt.RunBehavior=ThreadRunBehavior.RunUntilAllThreadsFinish;
            tt.StartAllThreads(500);
            Assert.IsTrue(tt.LastRunTime<tt.TimeOut);
        }
        
        [Test]
        public void SingleThreadForSpecifiedTimeStrategy()
        {
            Counter c = new Counter();
            ThreadTester tt = new ThreadTester();
            tt.AddThreadAction(
                delegate
                    {
                        c.Increment();
                    });

            tt.RunBehavior = ThreadRunBehavior.RunForSpecificTime;
            tt.StartAllThreads(500);
            Assert.IsTrue(tt.LastRunTime >499,"runtime was "+ tt.LastRunTime);
        }
        
        
        [Test]
        public void TryToCreateARaceCondition()
        {
            Counter c = new Counter();
            ThreadTester tt = new ThreadTester();
            for (int i = 0; i < 1000; i++)
            {
                tt.AddThreadAction(
                    delegate
                        {
//                            Console.WriteLine(Thread.CurrentThread.Name);
                            for (int j = 0; j < 1000; j++)
                            {
                                c.Increment();
                            }
                        });
            }

            
          

            tt.StartAllThreads(15000);
            Assert.AreEqual(1000000,c.Count);
        }
        
        [Test]
        public void HundredThreads()
        {
            Counter c = new Counter();
            ThreadTester tt = new ThreadTester();
            for (int i = 0; i < 100; i++)
            {
                tt.AddThreadAction(delegate
                                       {
                                           for (int j = 0; j < 10; j++)
                                           {
                                               c.Increment();
                                               Thread.Sleep(new Random(j+1).Next(100,300));
                                           }
                                       });
            }
          
            //this test will run for 22.5 seconds
            tt.RunBehavior=ThreadRunBehavior.RunForSpecificTime;
            tt.StartAllThreads(22500);
        }
    }
}
