
namespace NeedrunGameUtils
{
    public class FunctionCall
    {
        private readonly static string functionCallPrefix = "Function_";

        public static void StaticAt<T>(string methodName, params object[] paramValues)
        {
            ReflectionUtils.CallStaticAt<T>(functionCallPrefix + methodName, paramValues);
        }

        public static void At<T>(T instance, string methodName, params object[] paramValues)
        {
            ReflectionUtils.CallAt<T>(instance, functionCallPrefix + methodName, paramValues);
        }

        public static TReturn StaticWithReturn<TReturn, T>(string methodName, params object[] paramValues)
        {
            return ReflectionUtils.CallStaticWithReturn<TReturn, T>(functionCallPrefix + methodName, paramValues);
        }

        public static TReturn AtWithReturn<TReturn, T>(T instance, string methodName, params object[] paramValues)
        {
            return ReflectionUtils.CallWithReturn<TReturn, T>(instance, functionCallPrefix + methodName, paramValues);
        }
    }
}
