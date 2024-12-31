using System;
using System.Collections;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class ScreenCaptureAtIos : MonoBehaviour
    {
        public void Capture(Action onSuccess = null, Action<string> onFail = null, Func<byte[]> onCaptureScreen = null, Action onCaptureDone = null)
        {
            StartCoroutine(CaptureCoroutine(onSuccess, onFail, onCaptureScreen, onCaptureDone));
        }

        IEnumerator CaptureCoroutine(Action onSuccess = null, Action<string> onFail = null, Func<byte[]> onCaptureScreen = null, Action onCaptureDone = null)
        {
#if UNITY_IOS
        yield return new WaitForEndOfFrame();

        // ------------------------------------------------------------------------------------------------------------------------------------------------
        // Phase 1
        // 스크린샷이 저장될 디렉토리 생성 및 권한 체크
        // ------------------------------------------------------------------------------------------------------------------------------------------------
        string filePath = Application.persistentDataPath + "/";

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
        try
        {
            UpdateIosGallery(finalLocation);
        }
        catch (Exception e)
        {
            onCaptureDone();
            if (onFail != null)
            {
                onFail(I18n.Format("Fail to save screenshot: {0}", e.Message));
            }
            yield break;
        }

        if (onSuccess != null)
        {
            onSuccess();
        }
        onCaptureDone();
#endif
            yield break;
        }

#if UNITY_IOS
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void _SaveImageToGallery(string path);
    private void UpdateIosGallery(string filePath)
    {
        _SaveImageToGallery(filePath);
    }
#endif
    }
}
