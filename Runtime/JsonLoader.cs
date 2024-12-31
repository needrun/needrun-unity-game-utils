
namespace NeedrunGameUtils
{
    public class JsonLoader
    {
        public static T GetByFileName<T>(string fileName)
        {
            string filePath = PathResolver.ResolveStreamingAssets(fileName);
            string json = FileLoader.Load(filePath);
            return JsonUtils.FromJson<T>(json);
        }
    }
}
