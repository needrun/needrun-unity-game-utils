using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class ScreenCaptureAtEditor : MonoBehaviour
    {
        public void Capture(Action onSuccess = null, Action<string> onFail = null, Func<byte[]> onCaptureScreen = null, Action onCaptureDone = null)
        {
            StartCoroutine(CaptureCoroutine(onSuccess, onFail, onCaptureScreen, onCaptureDone));
        }

        IEnumerator CaptureCoroutine(Action onSuccess = null, Action<string> onFail = null, Func<byte[]> onCaptureScreen = null, Action onCaptureDone = null)
        {
            yield return new WaitForEndOfFrame();

            // ------------------------------------------------------------------------------------------------------------------------------------------------
            // Phase 1
            // 스크린샷이 저장될 디렉토리 생성 및 권한 체크
            // ------------------------------------------------------------------------------------------------------------------------------------------------
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/Screenshots/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            // ------------------------------------------------------------------------------------------------------------------------------------------------
            // Phase 2
            // 스크린샷 저장
            // ------------------------------------------------------------------------------------------------------------------------------------------------
            byte[] screenImage = onCaptureScreen();
            string filename = Application.productName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            string finalLocation = filePath + filename;
            File.WriteAllBytes(finalLocation, screenImage);
            Debug.Log("Screenshot saved to: " + finalLocation);

            // ------------------------------------------------------------------------------------------------------------------------------------------------
            // Phase 3
            // 스크린샷에 갤러리 정보 업데이트
            // ------------------------------------------------------------------------------------------------------------------------------------------------
            // Do nothing

            if (onSuccess != null)
            {
                onSuccess();
            }
            onCaptureDone();
        }
    }
}
