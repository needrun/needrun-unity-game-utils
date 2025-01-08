using System;

namespace NeedrunGameUtils
{
    public class MathUtils
    {
        public static bool IsInsideOfRange(float min, float x, float max)
        {
            return min <= x && x <= max;
        }

        public static bool IsOutsideOfRange(float min, float x, float max)
        {
            return !IsInsideOfRange(min, x, max);
        }

        public static int Clamp(int x, int min, int max)
        {
            return Math.Min(Math.Max(x, min), max);
        }

        public static long Clamp(long x, long min, long max)
        {
            return Math.Min(Math.Max(x, min), max);
        }

        public static float Clamp(float x, float min, float max)
        {
            return Math.Min(Math.Max(x, min), max);
        }

        public static float SafeDivide(float x, float y, float defaultValue = 0)
        {
            return y == 0 ? defaultValue : x / y;
        }
    }
}
