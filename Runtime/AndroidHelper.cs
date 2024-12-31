using UnityEngine;

namespace NeedrunGameUtils
{
    public class AndroidHelper
    {
        public static bool IsAndroidPlatform()
        {
            // Unity의 Application.platform을 사용하여 현재 플랫폼을 확인
            if (Application.platform == RuntimePlatform.Android)
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