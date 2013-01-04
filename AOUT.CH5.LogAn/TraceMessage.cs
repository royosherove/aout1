using System;
using System.Collections.Generic;
using System.Text;

namespace AOUT.CH5.LogAn
{
    public class TraceMessage : IEquatable<TraceMessage>
    {
        public bool Equals(TraceMessage traceMessage)
        {
            if (traceMessage == null) return false;
            if (!Equals(message, traceMessage.message)) return false;
            if (severity != traceMessage.severity) return false;
            if (!Equals(source, traceMessage.source)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as TraceMessage);
        }

        public override int GetHashCode()
        {
            int result = message.GetHashCode();
            result = 29*result + severity;
            result = 29*result + source.GetHashCode();
            return result;
        }

        private string message;
        private int severity;
        private string source;

        public TraceMessage(string message, int severity, string source)
        {
            this.message = message;
            this.severity = severity;
            this.source = source;
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
