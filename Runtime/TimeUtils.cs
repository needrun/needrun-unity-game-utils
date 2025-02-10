
using System;

namespace NeedrunGameUtils
{
    public class TimeUtils
    {
        public static TimeZoneInfo KOREA_TIME_ZONE = TimeZoneInfo.CreateCustomTimeZone("UTC+09:00", TimeSpan.FromHours(9), "UTC+09:00", "UTC+09:00");
        public static readonly string MIN_FORMAT_VALUE = Iso8601Parser.MIN_FORMAT_VALUE;
        public static readonly long MIN_TIMESTAMP_VALUE = Iso8601Parser.MIN_TIMESTAMP_VALUE;
        public static readonly string HHMMSS = @"hh\:mm\:ss";
        public static readonly string MMSS = @"mm\:ss";
        public static readonly string MSS = @"m\:ss";
        public static DateTimeOffset cachedNetworkDateTimeOffset = DateTimeOffset.MinValue;

        public static DateTimeOffset Parse(string iso8601String)
        {
            return Iso8601Parser.Parse(iso8601String);
        }

        public static DateTimeOffset From(long epochMillis)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(epochMillis).ToOffset(TimeSpan.FromHours(9));
        }

        public static DateTimeOffset From(long epochMillis, TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(DateTimeOffset.FromUnixTimeMilliseconds(epochMillis), timeZoneInfo);
        }

        public static string FormatYYYYMMDD(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("yyyy-MM-dd");
        }

        public static string FormatYYMMDD(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("yyMMdd");
        }

        public static string Format(DateTimeOffset dateTimeOffset)
        {
            return Iso8601Parser.Format(dateTimeOffset);
        }

        public static long toEpochmillis(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToUnixTimeMilliseconds();
        }

        public static DateTimeOffset GetNextMidnight(DateTimeOffset from)
        {
            DateTime nextDay = from.Date.AddDays(1);
            return new DateTimeOffset(nextDay, from.Offset);
        }

        public static bool IsDateEqual(DateTimeOffset a, DateTimeOffset b, TimeZoneInfo timeZoneInfo)
        {
            DateTimeOffset convertedA = TimeZoneInfo.ConvertTime(a, timeZoneInfo);
            DateTimeOffset convertedB = TimeZoneInfo.ConvertTime(b, timeZoneInfo);
            return convertedA.Date == convertedB.Date;
        }

        public static bool IsDateBefore(DateTimeOffset a, DateTimeOffset b, TimeZoneInfo timeZoneInfo)
        {
            DateTimeOffset convertedA = TimeZoneInfo.ConvertTime(a, timeZoneInfo);
            DateTimeOffset convertedB = TimeZoneInfo.ConvertTime(b, timeZoneInfo);
            return convertedA.Date < convertedB.Date;
        }

        public static bool IsBeforeOneHour(DateTimeOffset a, DateTimeOffset b)
        {
            long aEpochmillis = a.ToUnixTimeMilliseconds();
            long bEpochmillis = b.ToUnixTimeMilliseconds();
            long oneHour = 1000 * 60 * 60;
            long aDate = aEpochmillis / oneHour;
            long bDate = bEpochmillis / oneHour;
            return aDate < bDate;
        }

        public static long GetLocalTimestamp()
        {
            return toEpochmillis(DateTimeOffset.Now);
        }

        public static long GetNetworkTimestampOrLocal()
        {
            return toEpochmillis(GetNetworkDateTimeOffsetOrLocal());
        }

        public static DateTimeOffset GetNetworkDateTimeOffsetOrLocalAtKorea()
        {
            return GetNetworkDateTimeOffsetOrLocal(5).ToOffset(TimeSpan.FromHours(9)); ;
        }

        public static DateTimeOffset GetNetworkDateTimeOffsetOrLocal()
        {
            return GetNetworkDateTimeOffsetOrLocal(5);
        }

        public static DateTimeOffset GetNetworkDateTimeOffsetOrLocal(int diffMinute)
        {
            // cache에 기록된 네트워크 시간을 확인하고 로컬 시간과 시간을 비교한다
            // diffMinute분 이상 차이나는 것이 아니라면, 로컬 시간을 내려준다
            // diffMinute분 이상 차이나는 것이라면 네트워크 시간을 캐시 시간으로 변경하고 해당 값을 내려준다.
            DateTimeOffset localDateTimeOffset = DateTimeOffset.Now;
            TimeSpan timeDifferenceAbs = (localDateTimeOffset > cachedNetworkDateTimeOffset) ?
                localDateTimeOffset - cachedNetworkDateTimeOffset :
                cachedNetworkDateTimeOffset - localDateTimeOffset;

            if (timeDifferenceAbs.TotalMinutes >= diffMinute)
            {
                // 캐시에 기록된 네트워크 시간과 로컬 핸드폰에 기록된 시간이 큰 차이가 없는 경우

                return DateTimeOffset.Now;
            }
            {
                // 차이가 있다면 네트워크 시간을 리프레시
                cachedNetworkDateTimeOffset = NetworkTimer.GetNetworkDateTimeOffset();
                return cachedNetworkDateTimeOffset;
            }
        }

        public static DateTimeOffset GetNetworkDateTimeOffset()
        {
            return NetworkTimer.GetNetworkDateTimeOffset(); ;
        }

        public static string CreateLeftTimer(DateTimeOffset from, DateTimeOffset to, string format, TimeSpan maxTimeSpan)
        {
            TimeSpan difference = to - from;
            return CreateLeftTimer(difference, format, maxTimeSpan);
        }

        public static string CreateLeftTimer(TimeSpan difference, string format, TimeSpan maxTimeSpan)
        {
            // 최대값이 설정 값을 초과하지 않도록 설정
            if (difference > maxTimeSpan)
                difference = maxTimeSpan;
            // 최소값이 음수가 되지 않도록 설정
            if (difference < TimeSpan.Zero)
                difference = TimeSpan.Zero;
            return difference.ToString(format);
        }

        private class Iso8601Parser
        {
            public static readonly string MIN_FORMAT_VALUE = "1970-01-01 00:00:00+00:00";
            public static readonly long MIN_TIMESTAMP_VALUE = 0;

            public static DateTimeOffset Parse(string iso8601String)
            {
                return DateTimeOffset.ParseExact(
                  iso8601String,
                  "yyyy-MM-dd HH:mm:sszzz",
                  System.Globalization.CultureInfo.InvariantCulture,
                  System.Globalization.DateTimeStyles.AssumeUniversal);
            }

            public static string Format(DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.ToString("yyyy-MM-dd HH:mm:sszzz");
            }
        }
    }
}