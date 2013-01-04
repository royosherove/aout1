using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Osherove.ThreadTester.Strategies
{
    class AllThreadsShouldFinishStrategy:AbstractThreadRunStrategy
    {
        readonly AutoResetEvent allThreadsAreFinishedSignal = new AutoResetEvent(false);
        private int finishedThreadsCount = 0;

        private readonly object sync = new object();

        public override void StartAll(int timeout, List<ThreadAction> actions)
        {
            this.threadActions = actions;
            StartAllThreads(timeout);
        }


        public override void OnThreadFinished(ThreadAction threadAction)
        {
            lock (this.sync)
            {
                finishedThreadsCount++;
                if (finishedThreadsCount == threadActions.Count)
                {
                    allThreadsAreFinishedSignal.Set();
                }
            }
        }


        public void StartAllThreads(int runningTimeout)
        {
            Console.WriteLine("Starting " + threadActions.Count + " threads..");
            finishedThreadsCount = 0;
            StartAllThreadsAtOnce();

            allThreadsAreFinishedSignal.WaitOne(runningTimeout, false);
            if (finishedThreadsCount != threadActions.Count)
            {
                StopAllRunningThreads();
                Assert.Fail("Not all threads were done");
            }
        }
    }
}