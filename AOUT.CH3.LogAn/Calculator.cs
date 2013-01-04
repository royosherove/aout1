namespace AOUT.CH3.LogAn
{
    public class Calculator
    {
        private int sum=0;

        public void Add(int number)
        {
            sum+=number;
        }
        public int Sum()
        {
            int temp = sum;
            sum = 0;
            return temp;
        }
    }
}
