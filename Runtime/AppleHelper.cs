using UnityEngine;

namespace NeedrunGameUtils
{
    public class AppleHelper
    {
        public static bool IsApplePlatform()
        {
            // Unity의 Application.platform을 사용하여 현재 플랫폼을 확인
            if (// Application.platform == RuntimePlatform.OSXEditor || // mac에서 개발할 때는 테스트가 필요해서 off처리 해둠
                Application.platform == RuntimePlatform.OSXPlayer ||
                Application.platform == RuntimePlatform.IPhonePlayer) // IPhone, IPad
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}