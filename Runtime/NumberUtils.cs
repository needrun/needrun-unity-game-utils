
namespace NeedrunGameUtils
{
    public class NumberUtils
    {
        public static string FormatNumberWithCommas(int number)
        {
            return string.Format("{0:#,0}", number);
        }

        public static string FormatNumberWithCommas(float number)
        {
            return string.Format("{0:#,0.###}", number);
        }
    }
}