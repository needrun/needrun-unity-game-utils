using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    class LocaleElement
    {
        public string key;
        public string en;
        public string kr;
        public string jp;
    }

    public class I18n
    {

        private static Dictionary<string, Dictionary<string, string>> locales = new Dictionary<string, Dictionary<string, string>>();

        private static Locale? locale = null;
        private static readonly UnityEvent localeChangedEvent = new UnityEvent();

        public static void Load(string[] filenames)
        {
            foreach (string filename in filenames)
            {
                Load(filename);
            }
        }

        public static void Load(string filename)
        {
            List<LocaleElement> allLocaleElement = JsonLoader.GetByFileName<List<LocaleElement>>(filename);
            foreach (LocaleElement localeElement in allLocaleElement)
            {
                if (!locales.ContainsKey(localeElement.key))
                {
                    locales.Add(localeElement.key, new Dictionary<string, string>());
                }

                // 영문 입력
                if (!locales[localeElement.key].ContainsKey("en"))
                {
                    locales[localeElement.key].Add("en", localeElement.en);
                }
                else
                {
                    // 이미 존재하는 경우 덮어쓰기
                    locales[localeElement.key]["en"] = localeElement.en;
                }

                // 한글 입력
                if (!locales[localeElement.key].ContainsKey("kr"))
                {
                    locales[localeElement.key].Add("kr", localeElement.kr);
                }
                else
                {
                    // 이미 존재하는 경우 덮어쓰기
                    locales[localeElement.key]["kr"] = localeElement.kr;
                }

                // 일본어 입력
                if (!locales[localeElement.key].ContainsKey("jp"))
                {
                    locales[localeElement.key].Add("jp", localeElement.jp);
                }
                else
                {
                    // 이미 존재하는 경우 덮어쓰기
                    locales[localeElement.key]["jp"] = localeElement.jp;
                }
            }
        }

        public static string GetValue(string key)
        {
            return GetValue(key, key);
        }

        public static string Format(string key, params object[] parameters)
        {
            if (!I18n.locales.ContainsKey(key) || !I18n.locales[key].ContainsKey(GetCurrentLocale().GetLocaleCode()))
            {
                Debug.LogWarning("Can not find (" + key + ") at locale data");
                return string.Format(key, parameters);
            }
            string source = I18n.locales[key][GetCurrentLocale().GetLocaleCode()];
            return string.Format(source, parameters);
        }

        public static string GetValue(string key, string defaultValue)
        {
            if (!I18n.locales.ContainsKey(key) || !I18n.locales[key].ContainsKey(GetCurrentLocale().GetLocaleCode()))
            {
                Debug.LogWarning("Can not find (" + key + ") at locale data");
                return defaultValue;
            }
            return I18n.locales[key][GetCurrentLocale().GetLocaleCode()];
        }

        public static void AddListener(UnityAction action)
        {
            I18n.localeChangedEvent.AddListener(action);
        }

        public static void RemoveListener(UnityAction action)
        {
            I18n.localeChangedEvent.RemoveListener(action);
        }

        public static void RemoveAllListeners()
        {
            I18n.localeChangedEvent.RemoveAllListeners();
        }

        public static Locale GetCurrentLocale()
        {
            if (I18n.locale == null)
            {
                I18n.locale = LocalStorage.GetOrDefault<Locale>("locale", Locale.KR);
            }
            return (Locale)I18n.locale;
        }

        public static void ChangeLocale(Locale locale)
        {
            I18n.locale = locale;
            I18n.localeChangedEvent.Invoke();
            LocalStorage.Save("locale", I18n.locale);
        }
    }
}