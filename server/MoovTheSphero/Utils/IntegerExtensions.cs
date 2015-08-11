namespace Eleks.MoovTheSphero.Utils
{
    static class IntegerExtensions
    {
        public static bool IsMoreThenWithThresold(this int value, int other, int thresold = 30)
        {
            if (value > other + thresold)
            {
                return true;
            }
            return false;
        }
    }
}
