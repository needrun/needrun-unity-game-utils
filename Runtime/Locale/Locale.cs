using System.Globalization;

namespace NeedrunGameUtils
{
    public enum Locale
    {
        KR,
        EN,
        JP
    }

    static class LocaleMethod
    {
        private static CultureInfo koreanCultureInfo = new CultureInfo("ko-KR");
        private static CultureInfo englishCultureInfo = new CultureInfo("en-US");
        private static CultureInfo japaneseCultureInfo = new CultureInfo("ja-JP");

        public static string GetCurrency(this Locale locale)
        {
            switch (locale)
            {
                case Locale.KR:
                    return "₩";
                case Locale.JP:
                    return "¥";
                default:
                case Locale.EN:
                    return "$";
            }
        }

        public static string GetLocaleText(this Locale locale)
        {
            switch (locale)
            {
                default:
                case Locale.EN:
                    return "English";
                case Locale.KR:
                    return "한국어";
                case Locale.JP:
                    return "日本語";
            }
        }

        public static string GetLocaleCode(this Locale locale)
        {
            switch (locale)
            {
                default:
                case Locale.EN:
                    return "en";
                case Locale.KR:
                    return "kr";
                case Locale.JP:
                    return "jp";
            }
        }

        public static CultureInfo GetCultureInfo(this Locale locale)
        {
            switch (locale)
            {
                default:
                case Locale.EN:
                    return englishCultureInfo;
                case Locale.KR:
                    return koreanCultureInfo;
                case Locale.JP:
                    return japaneseCultureInfo;
            }
        }
    }
}