namespace AOUT.CH7.LogAn
{
    public class AnalyzeResults
    {
        private readonly string text;

        public AnalyzeResults(string text)
        {
            this.text = text;
        }

        public string Text
        {
            get { return text; }
        }
    }
}