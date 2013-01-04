using System;
using System.Collections.Generic;
using System.Threading;

namespace Osherove.ThreadTester.Strategies
{
    internal abstract class AbstractThreadRunStrategy : IThreadRunStrategy
    {
        
        protected List<ThreadAction> threadActions;

        public abstract void StartAll(int timeout,List<ThreadAction> actions);

        public abstract void OnThreadFinished(ThreadAction threadAction);

        protected void StartAllThreadsAtOnce()
        {
            Console.WriteLine("Preparing "+ threadActions.Count+  " threads..");
            ManualResetEvent threadStartSignal = new ManualResetEvent(false);
            foreach (ThreadAction action in threadActions)
            {
                action.StartWhenSignaled(threadStartSignal);
            }
            Console.WriteLine("Starting all threads..");
            threadStartSignal.Set();
        }

        protected void StopAllRunningThreads()
        {
            Console.WriteLine("Stopping running threads...");
            foreach (ThreadAction action in threadActions)
            {
                try
                {
                    action.Stop();
                }
                catch (ThreadAbortException e)
                {
                    
                }
            }
        }
    }
}