namespace AOUT.CH6.LogAN
{
    public class LogAnalyzer
    {
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                LoggingFacility.Log("aFilename too short:" + fileName);
            }
            //rest of the method here
        }
    }
}
