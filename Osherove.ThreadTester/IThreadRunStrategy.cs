using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Osherove.ThreadTester
{
    public enum ThreadRunBehavior
    {
        RunUntilAllThreadsFinish,
        RunForSpecificTime
    }
    public interface IThreadRunStrategy
    {
        void StartAll(int timeout, List<ThreadAction> actions);
        void OnThreadFinished(ThreadAction threadAction);
    }
}
