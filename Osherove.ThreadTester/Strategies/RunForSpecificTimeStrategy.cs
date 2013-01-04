using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Osherove.ThreadTester.Strategies
{
    class RunForSpecificTimeStrategy : AbstractThreadRunStrategy
    {
        private bool isFinishing;

        public override void StartAll(int timeout,List<ThreadAction> actions)
        {
            this.threadActions = actions;
            StartAllThreads(timeout);
        }

        public override void OnThreadFinished(ThreadAction threadAction)
        {
            if(isFinishing)
            {
                return;
            }
            ThreadAction action = new ThreadAction(threadAction.DoCallback);
            action.SignalFinishedCallback = threadAction.SignalFinishedCallback;
            action.Start();
        }


        public void StartAllThreads(int runningTimeout)
        {
            isFinishing = false;
            Console.WriteLine("Starting " + threadActions.Count + " threads..");
            StartAllThreadsAtOnce();
            AutoResetEvent timout = new AutoResetEvent(false);
            timout.WaitOne(runningTimeout, false);
            isFinishing=true;
            StopAllRunningThreads();
        }
    }
}