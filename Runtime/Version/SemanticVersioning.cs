
namespace NeedrunGameUtils
{
    public class SemanticVersioning
    {
        public int majorVersion { get; }
        public int minorVersion { get; }
        public int patchVersion { get; }

        public string readableString
        {
            get
            {
                return majorVersion.ToString("0") + "." +
                    minorVersion.ToString("0") + "." +
                    patchVersion.ToString("0");
            }
        }

        public int bundleVersionCode
        {
            get
            {
                return majorVersion * 10000 + minorVersion * 1000 + patchVersion;
            }
        }

        public SemanticVersioning(string versionString)
        {
            // 데모인 경우 demo-0.0.0 또는 999.0.0.0 형식으로 버전이 표기됩니다. (IOS는 999.0.0.0, 안드로이드는 demo-0.0.0 -> IOS는 숫자 아니면 . 외에 다른 문자가 들어가면 빌드가 안되서)
            // versionString(ex. 0.0.0, demo-0.0.0, 999.1.3.0 ...)에서 \d+.\d+.\d+ 형식의 문자열을 파싱합니다.
            string regexForParse = @"(\d+\.\d+\.\d+)$";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(versionString, regexForParse);
            if (!match.Success)
            {
                this.majorVersion = 0;
                this.minorVersion = 0;
                this.patchVersion = 0;
                return;
            }
            string stringForParse = match.Groups[1].Value; // 0.0.0
            string[] parsedVersionString = stringForParse.Split('.');
            this.majorVersion = parsedVersionString.Length > 0 ? int.Parse(parsedVersionString[0]) : 0;
            this.minorVersion = parsedVersionString.Length > 1 ? int.Parse(parsedVersionString[1]) : 0;
            this.patchVersion = parsedVersionString.Length > 2 ? int.Parse(parsedVersionString[2]) : 0;
        }

        public SemanticVersioning(int major, int minor, int patch)
        {
            this.majorVersion = major;
            this.minorVersion = minor;
            this.patchVersion = patch;
        }

        public bool IsGreaterThanEqual(SemanticVersioning other)
        {
            return bundleVersionCode >= other.bundleVersionCode;
        }
    }
}