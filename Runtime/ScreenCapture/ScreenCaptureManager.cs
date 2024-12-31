using System;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class ScreenCaptureManager : MonoBehaviour
    {
        public bool capturing { get; private set; }
        public ScreenCaptureAtEditor screenCaptureAtEditor;
        public ScreenCaptureAtAndroid screenCaptureAtAndroid;
        public ScreenCaptureAtIos screenCaptureAtIos;

        public void Capture(Action onSuccess = null, Action<string> onFail = null)
        {
            if (capturing == false)
            {
                capturing = true;
                if (Application.isEditor)
                {
                    screenCaptureAtEditor.Capture(onSuccess, onFail, CaptureScreen, CaptureDone);
                }
                else if (AndroidHelper.IsAndroidPlatform())
                {
                    screenCaptureAtAndroid.Capture(onSuccess, onFail, CaptureScreen, CaptureDone);
                }
                else if (
                    // Unity의 Application.platform을 사용하여 현재 플랫폼을 확인
                    // Application.platform == RuntimePlatform.OSXEditor || // mac에서 개발할 때는 테스트가 필요해서 off처리 해둠
                    Application.platform == RuntimePlatform.OSXPlayer ||
                    Application.platform == RuntimePlatform.IPhonePlayer) // IPhone, IPad
                {
                    screenCaptureAtIos.Capture(onSuccess, onFail, CaptureScreen, CaptureDone);
                }
            }
        }

        private byte[] CaptureScreen()
        {
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();
            byte[] imageByte = texture.EncodeToPNG();
            DestroyImmediate(texture);
            return imageByte;
        }

        private void CaptureDone()
        {
            capturing = false;
        }
    }
}