using System.Security.Cryptography;
using System.Text;

namespace NeedrunGameUtils
{
    public class HashUtils
    {
        private static HashAlgorithm md5 = MD5.Create();

        public static string Md5(string str)
        {
            byte[] data = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string GenerateDirtyHash<T>(T target)
        {
            string json = JsonUtils.ToJson(target);
            string result = Md5(json);
            return result;
        }
    }
}