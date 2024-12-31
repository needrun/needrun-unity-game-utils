using UnityEngine;
using System.Collections.Concurrent;

namespace NeedrunGameUtils
{
    public class SpriteLoader
    {
        private static readonly ConcurrentDictionary<string, Sprite> spritesCache = new ConcurrentDictionary<string, Sprite>();

        public static Sprite Load(string pathname)
        {
            if (pathname == null)
            {
                Debug.LogWarning("pathname null requested.");
                return null;
            }
            if (spritesCache.ContainsKey(pathname))
            {
                // 이미 한번 불러온 기록이 있는 경우
                return spritesCache[pathname];
            }
            // 한번도 불러온적이 없다면 pathname을 key로 갖는 맵에 캐시로 기록해둡니다.
            Sprite sprite = Resources.Load<Sprite>(pathname);
            spritesCache.TryAdd(pathname, sprite);
            return sprite;
        }
    }
}