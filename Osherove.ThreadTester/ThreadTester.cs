using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Timers;
using NUnit.Framework;
using Osherove.ThreadTester.Strategies;
using Osherove.ThreadTester.Tests;
using Timer=System.Timers.Timer;

namespace Osherove.ThreadTester
{
    public delegate void Func();
    public delegate void ThreadFinishedDelegate(ThreadAction threadAction);

    public class ThreadTester
    {
        private IThreadRunStrategy runner = new AllThreadsShouldFinishStrategy();

        private readonly List<ThreadAction> threadActions = new List<ThreadAction>();
        private long lastRunTime;
        readonly Stopwatch stopper = new Stopwatch();
        private long timeOut;
        private ThreadRunBehavior runBehavior=ThreadRunBehavior.RunUntilAllThreadsFinish;

        void SignalThreadIsFinished(ThreadAction threadAction)
        {
            runner.OnThreadFinished(threadAction);

        }

        public void AddThreadAction(Func ActionCallback)
        {
            ThreadAction action = new ThreadAction(ActionCallback);
            action.SignalFinishedCallback = SignalThreadIsFinished;
            threadActions.Add(action);
        }

        public List<ThreadAction> ThreadActions
        {
            get { return threadActions; }
        }


        public long LastRunTime
        {
            get { return lastRunTime; }
        }

        public long TimeOut
        {
            get { return timeOut; }
        }

        public void StartAllThreads(int runningTimeout)
        {
            runner = CreateStrategy(RunBehavior);
            timeOut = runningTimeout;
            stopper.Reset();
            stopper.Start();
            runner.StartAll(runningTimeout, threadActions);
            stopper.Stop();
            lastRunTime = stopper.ElapsedMilliseconds;
        }


        public ThreadRunBehavior RunBehavior
        {
            get { return runBehavior; }
            set { runBehavior = value; }
        }

        protected IThreadRunStrategy CreateStrategy(ThreadRunBehavior val)
        {
            Dictionary<ThreadRunBehavior, IThreadRunStrategy> runStrategies = new Dictionary<ThreadRunBehavior, IThreadRunStrategy>();
            runStrategies.Add(ThreadRunBehavior.RunForSpecificTime, new RunForSpecificTimeStrategy());
            runStrategies.Add(ThreadRunBehavior.RunUntilAllThreadsFinish, new AllThreadsShouldFinishStrategy());

            return runStrategies[val];
        }
    }
}