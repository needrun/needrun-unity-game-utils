using System;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class LocalStorage
    {
        public static bool Exist(string name)
        {
            return PlayerPrefs.HasKey(name);
        }

        public static T GetByKey<T>(string name)
        {
            if (!PlayerPrefs.HasKey(name))
                throw new Exception("Can not find data(" + name + ") in player prefs");
            string json = PlayerPrefs.GetString(name);
            return JsonUtils.FromJson<T>(json);
        }

        public static T GetOrDefault<T>(string name, T defaultValue)
        {
            if (!PlayerPrefs.HasKey(name))
            {
                return defaultValue;
            }
            try
            {
                string json = PlayerPrefs.GetString(name);
                return JsonUtils.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public static void Save(string name, object data)
        {
            string json = JsonUtils.ToJson(data);
            PlayerPrefs.SetString(name, json);
        }

        public static void Remove(string name)
        {
            PlayerPrefs.DeleteKey(name);
        }
    }
}