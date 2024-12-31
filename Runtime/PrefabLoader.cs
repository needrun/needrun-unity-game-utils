using System.Collections.Generic;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class PrefabLoader
    {
        private static readonly Dictionary<string, GameObject> prefabsCache = new Dictionary<string, GameObject>();

        public static GameObject Load(string pathname)
        {
            if (pathname == null)
            {
                Debug.LogWarning("pathname null requested.");
                return null;
            }
            if (prefabsCache.ContainsKey(pathname))
            {
                // 이미 한번 불러온 기록이 있는 경우
                return prefabsCache[pathname];
            }
            // 한번도 불러온적이 없다면 pathname을 key로 갖는 맵에 캐시로 기록해둡니다.
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + pathname);
            prefabsCache.Add(pathname, prefab);
            return prefab;
        }
    }
}