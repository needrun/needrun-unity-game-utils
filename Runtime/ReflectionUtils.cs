using System;
using System.Reflection;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class ReflectionUtils
    {
        public static void CallStaticAt<T>(string methodName, params object[] paramValues)
        {
            // 이 클래스 안에서 resourceId와 일치하는 메서드를 찾습니다.
            BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo method = typeof(T).GetMethod(methodName, bindingFlags);
            if (method != null && method.IsStatic)
            {
                // 메서드의 파라미터 정보 가져오기
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    // 파라미터가 있는 경우
                    method.Invoke(null, paramValues);
                }
                else
                {
                    // 파라미터가 없는 경우
                    method.Invoke(null, null);
                }
            }
            else
            {
                string message = $"Cannot find method(name: {methodName}) in type {typeof(T).FullName}";
                Debug.LogError(message);
                throw new Exception(message);
            }
        }

        public static void CallAt<T>(T instance, string methodName, params object[] paramValues)
        {
            // 이 클래스 안에서 resourceId와 일치하는 메서드를 찾습니다.
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo method = typeof(T).GetMethod(methodName, bindingFlags);
            if (method != null)
            {
                // 메서드의 파라미터 정보 가져오기
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    // 파라미터가 있는 경우
                    method.Invoke(instance, paramValues);
                }
                else
                {
                    // 파라미터가 없는 경우
                    method.Invoke(instance, null);
                }
            }
            else
            {
                string message = $"Cannot find method(name: {methodName}) in type {typeof(T).FullName}";
                Debug.LogError(message);
                throw new Exception(message);
            }
        }

        public static TReturn CallStaticWithReturn<TReturn, T>(string methodName, params object[] paramValues)
        {
            // 이 클래스 안에서 methodName과 일치하는 메서드를 찾습니다.
            BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo method = typeof(T).GetMethod(methodName, bindingFlags);
            if (method != null && method.IsStatic)
            {
                // 메서드의 파라미터 정보 가져오기
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    // 파라미터가 있는 경우
                    return (TReturn)method.Invoke(null, paramValues);
                }
                else
                {
                    // 파라미터가 없는 경우
                    return (TReturn)method.Invoke(null, null);
                }
            }
            else
            {
                string message = $"Cannot find method(name: {methodName}) in type {typeof(T).FullName}";
                Debug.LogError(message);
                throw new Exception(message);
            }
        }

        public static TReturn CallWithReturn<TReturn, T>(T instance, string methodName, params object[] paramValues)
        {
            // 이 클래스 안에서 methodName과 일치하는 메서드를 찾습니다.
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo method = typeof(T).GetMethod(methodName, bindingFlags);
            if (method != null)
            {
                // 메서드의 파라미터 정보 가져오기
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    // 파라미터가 있는 경우
                    return (TReturn)method.Invoke(instance, paramValues);
                }
                else
                {
                    // 파라미터가 없는 경우
                    return (TReturn)method.Invoke(instance, null);
                }
            }
            else
            {
                string message = $"Cannot find method(name: {methodName}) in type {typeof(T).FullName}";
                Debug.LogError(message);
                throw new Exception(message);
            }
        }
    }
}
