using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LogAn;

namespace AOUT.CH7.LogAn
{
    public class LogAnalyzer
    {
        public LogAnalyzer()
        {
        }

        private ILogProvider logReader;
        private string defaultDelimiter;

        public LogAnalyzer(ILogProvider logReader)
        {
            this.logReader = logReader;
        }

        public bool IsValid(string fileName)
        {
            if (fileName.Length < 8)
            {
                return true;
            }
            return false;
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }

        public void Initialize()
        {
            
        }

        public bool IsValid(FileInfo info)
        {
            return false;
        }

        public AnalyzedOutput Analyze(string logText)
        {
            AnalyzedOutput output = new AnalyzedOutput();
            output.AddLine(logText.Split('\t'));
            return output;
        }

        public string GetInternalDefaultDelimiter()
        {
            return defaultDelimiter;
        }


        public AnalyzeResults AnalyzeFile(string fileName)
        {
            int lineCount = logReader.GetLineCount();
            string text = "";
            for (int i = 0; i < lineCount; i++)
            {
                text += logReader.GetText(fileName, i, i);
            }
            return new AnalyzeResults(text);
        }

        public int GetLineCount(string filename)
        {
            return -1;
        }
    }

    public class AnalyzedOutput : IEquatable<AnalyzedOutput>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (LineInfo line in lines)
            {
                sb.Append(line.ToString());
            }
            return sb.ToString();
        }

        public LineInfo GetLine(int lineIndex)
        {
            return null;
        }

        List<LineInfo> lines = new List<LineInfo>();
        public int LineCount
        {
            get { return lines.Count; }
        }

        public bool Equals(AnalyzedOutput analyzedOutput)
        {
            if (analyzedOutput == null) return false;
            return Equals(lines, analyzedOutput.lines);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as AnalyzedOutput);
        }

        public override int GetHashCode()
        {
            return lines.GetHashCode();
        }

        public void AddLine(params string[] fieldInfo)
        {
            lines.Add(new LineInfo(fieldInfo));
        }
    }

    public class LineInfo : IEquatable<LineInfo>
    {
        public LineInfo(params string[] fieldData)
        {
            fields = fieldData;
        }

        private string[]  fields = new string[]{};

        public bool Equals(LineInfo lineInfo)
        {
            if (lineInfo == null) return false;
            return Equals(fields, lineInfo.fields);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.fields.Length; i++)
            {
                sb.Append(this[i]);
                sb.Append(",");
            }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as LineInfo);
        }

        public override int GetHashCode()
        {
            return fields != null ? fields.GetHashCode() : 0;
        }

        public string this[int index]
        {
            get
            {
                return fields[index];
            }
        }
    }
}
