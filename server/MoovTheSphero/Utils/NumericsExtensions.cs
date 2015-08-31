using System.Globalization;

namespace Eleks.MoovTheSphero.Utils
{
    static class NumericsExtensions
    {
        public static string ToInvString(this double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
