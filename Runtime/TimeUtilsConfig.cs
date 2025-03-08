
using System;

namespace NeedrunGameUtils
{
    public class TimeUtilsConfig
    {
        public TimeSpan ntpConfidenceLevel = TimeSpan.FromDays(3);
        public Func<DateTimeOffset> ntpFailCallback = null;

    }
}