using System;
using System.Collections.Generic;
using System.Text;

namespace AOUT.CH5.LogAn
{
    public class ComplexTraceMessage
    {
        private string message;
        private int severity;
        private string source;
        private TraceMessage innerMessage;

        public TraceMessage InnerMessage
        {
            get { return innerMessage; }
            set { innerMessage = value; }
        }


        public ComplexTraceMessage(string message, int severity, string source, TraceMessage innerMessage)
        {
            this.message = message;
            this.severity = severity;
            this.source = source;
            this.innerMessage = innerMessage;
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public int Severity
        {
            get { return severity; }
            set { severity = value; }
        }

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
    }
}
