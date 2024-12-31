using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NeedrunGameUtils
{
    public class JsonUtils
    {
        private static JsonConverter[] jsonConverters = {
    };

        static JsonUtils()
        {
            // IL2CPP를 이용해 빌드시 HashSet이나 Dictinary같은 타입이 Deserialize가 안될 수 있음 (안드로이드 환경에선 발생 x, Xcode에서만 발생)
            // Aot 환경 (IL2CPP)에서도 컴파일이 가능하도록 아래와 같이 설정 
            Newtonsoft.Json.Utilities.AotHelper.EnsureList<string>(); // https://github.com/applejag/Newtonsoft.Json-for-Unity/wiki/Reference-Newtonsoft.Json.Utilities.AotHelper#aothelperensurelist
            Newtonsoft.Json.Utilities.AotHelper.EnsureDictionary<string, long>(); // https://github.com/applejag/Newtonsoft.Json-for-Unity/wiki/Reference-Newtonsoft.Json.Utilities.AotHelper#aothelperensuredictionary
        }

        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, jsonConverters);
        }

        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, jsonConverters);
        }

        public static Dictionary<string, object> ToDictionary(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            JObject root = JObject.Parse(json);
            return Parse(root);
        }

        public static Dictionary<string, object> Parse(JObject obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (var pair in obj)
            {
                string jObjectKey = pair.Key;
                JToken jObjectValue = pair.Value;
                result.Add(jObjectKey, ParseValue(jObjectValue));
            }
            return result;
        }

        public static object ParseValue(JToken jObjectValue)
        {
            object value = null;
            switch (jObjectValue.Type)
            {
                case JTokenType.Array:
                    JArray jarray = (JArray)jObjectValue;
                    object[] temp = new object[jarray.Count];
                    for (int i = 0; i < jarray.Count; i++)
                    {
                        if (jarray[i].Type == JTokenType.Object)
                        {
                            temp[i] = Parse((JObject)(jarray[i]));
                        }
                        else
                        {
                            temp[i] = ParseValue(jarray[i]);
                        }
                    }
                    value = temp;
                    break;
                case JTokenType.Null:
                    value = null;
                    break;
                case JTokenType.Boolean:
                    value = ((bool)jObjectValue);
                    break;
                case JTokenType.Float:
                    value = ((float)jObjectValue);
                    break;
                case JTokenType.Integer:
                    value = ((long)jObjectValue);
                    break;
                case JTokenType.Object:
                    value = Parse((JObject)jObjectValue);
                    break;
                default:
                    value = jObjectValue.ToString();
                    break;
            }
            return value;
        }
    }
}
