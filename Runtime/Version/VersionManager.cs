using UnityEngine;

namespace NeedrunGameUtils
{
    public class VersionManager
    {
        public static string GetAppVersion()
        {
            if (IsDemo())
            {
                return Application.version;
            }
            return "v" + GetCurrentSemanticVersioning().readableString;
        }

        private static SemanticVersioning GetCurrentSemanticVersioning()
        {
            return new SemanticVersioning(Application.version);
        }

        public static bool IsSupportVersion(string supportVersionString)
        {
            SemanticVersioning currentVersion = GetCurrentSemanticVersioning();
            SemanticVersioning supportVersion = new SemanticVersioning(supportVersionString);
            return currentVersion.IsGreaterThanEqual(supportVersion);
        }

        public static bool IsDemo()
        {
            // 데모인 경우 demo-0.0.0 또는 999.0.0.0 형식으로 버전이 표기됩니다. (IOS는 999.0.0.0, 안드로이드는 demo-0.0.0 -> IOS는 숫자 아니면 . 외에 다른 문자가 들어가면 빌드가 안되서)
            return Application.version.Contains("demo") || Application.version.StartsWith("999.");
        }
    }
}