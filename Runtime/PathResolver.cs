using UnityEngine;
using System.IO;

namespace NeedrunGameUtils
{
    public class PathResolver
    {
        public static string ResolveStreamingAssets(string path)
        {
            if (!path.StartsWith("/"))
                path = "/" + path;
            return Path.Combine(Application.streamingAssetsPath + path);
        }
    }
}