using UnityEngine;
using System;
using System.Collections.Generic;

namespace NeedrunGameUtils
{
    [Serializable]
    public class NotificationMemoryData
    {
        private static string ROOT_KEY = "MOBILE_NOTIFICATION";
        public Dictionary<string, int> android = new Dictionary<string, int>();
        public HashSet<string> ios = new HashSet<string>();

        public static NotificationMemoryData Load()
        {
            string json = LocalStorage.GetOrDefault(ROOT_KEY, "{}");
            Debug.Log("NotificationMemoryData.Load: " + json);
            NotificationMemoryData data = JsonUtils.FromJson<NotificationMemoryData>(json);
            if (data.android == null)
            {
                data.android = new Dictionary<string, int>();
            }
            if (data.ios == null)
            {
                data.ios = new HashSet<string>();
            }
            return data;
        }

        public void Save()
        {
            string json = JsonUtils.ToJson(this);
            Debug.Log("@@@@ NotificationMemoryData.Save: " + json);
            LocalStorage.Save(ROOT_KEY, json);
        }

        public string ToJson()
        {
            return JsonUtils.ToJson(this);
        }
    }
}