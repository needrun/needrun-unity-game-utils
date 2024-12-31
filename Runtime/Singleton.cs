using System;

namespace NeedrunGameUtils
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() =>
        {
            Type t = typeof(T);
            var flags = System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic;
            var constructor = t.GetConstructor(flags, null, Type.EmptyTypes, null);
            return constructor.Invoke(null) as T;
        });

        public static T Instance()
        {
            return _instance.Value;
        }

        public static T instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
