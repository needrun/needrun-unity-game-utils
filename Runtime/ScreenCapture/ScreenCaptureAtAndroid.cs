using System;
using System.Collections;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class ScreenCaptureAtAndroid : MonoBehaviour
    {
        public void Capture(Action onSuccess = null, Action<string> onFail = null, Func<byte[]> onCaptureScreen = null, Action onCaptureDone = null)
        {
            StartCoroutine(CaptureCoroutine(onSuccess, onFail, onCaptureScreen, onCaptureDone));
        }

        IEnumerator CaptureCoroutine(Action onSuccess = null, Action<string> onFail = null, Func<byte[]> onCaptureScreen = null, Action onCaptureDone = null)
        {
#if UNITY_ANDROID
        yield return new WaitForEndOfFrame();

        // ------------------------------------------------------------------------------------------------------------------------------------------------
        // Phase 1
        // 스크린샷이 저장될 디렉토리 생성 및 권한 체크
        // ------------------------------------------------------------------------------------------------------------------------------------------------
        // Do nothing

        // ------------------------------------------------------------------------------------------------------------------------------------------------
        // Phase 2
        // 스크린샷 저장
        // ------------------------------------------------------------------------------------------------------------------------------------------------
        string filename = Application.productName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        string savedFilePath = filename;
        try
        {
            savedFilePath = SaveImageToMediaStore(onCaptureScreen, filename); // MediaStore API를 통해 저장
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

        // ------------------------------------------------------------------------------------------------------------------------------------------------
        // Phase 3
        // 스크린샷에 갤러리 정보 업데이트
        // ------------------------------------------------------------------------------------------------------------------------------------------------
        try
        {
            ScanMediaFile(savedFilePath);
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

#if UNITY_ANDROID
    private string SaveImageToMediaStore(Func<byte[]> onCaptureScreen, string filename)
    {
        // MediaStore API를 통해 이미지 저장
        // MediaStore.Images.Media 클래스 사용
        AndroidJavaClass mediaStoreClass = new AndroidJavaClass("android.provider.MediaStore$Images$Media");
        AndroidJavaObject contentResolver = GetContentResolver();

        // ContentValues를 통해 파일 메타데이터 설정
        // 참조: https://developer.android.com/reference/android/provider/MediaStore.MediaColumns?_gl=1*1kuhrdf*_up*MQ..*_ga*MTA3MDU3MjM4OC4xNzMxMTY5NjUz*_ga_6HH9YJMN9M*MTczMTE2OTY1My4xLjAuMTczMTE2OTY1My4wLjAuMTE0ODk3NDEx#DISPLAY_NAME
        AndroidJavaObject values = new AndroidJavaObject("android.content.ContentValues");
        values.Call("put", "_display_name", filename); // 파일 이름 (_display_name 임)
        values.Call("put", "mime_type", "image/png");  // MIME 타입
        values.Call("put", "relative_path", "Pictures/Screenshots"); // 저장될 폴더

        // MediaStore Uri 가져오기
        AndroidJavaObject uri = mediaStoreClass.CallStatic<AndroidJavaObject>("getContentUri", "external");
        AndroidJavaObject uriResult = contentResolver.Call<AndroidJavaObject>("insert", uri, values); // https://developer.android.com/reference/android/net/Uri?_gl=1*1rkvmxf*_up*MQ..*_ga*OTEzMDQ0MTI5LjE3MzExNzA5ODA.*_ga_6HH9YJMN9M*MTczMTE3MDk3OS4xLjAuMTczMTE3MDk3OS4wLjAuOTg3NDU4MzMy#insert(android.net.Uri,%20android.content.ContentValues,%20android.os.Bundle)

        // 저장된 파일 경로 얻기
        string filePath = uriResult.Call<string>("toString");

        // 이미지 저장 (이 과정은 일반적으로 ContentResolver를 통해 처리)
        byte[] imageBytes = onCaptureScreen();
        AndroidJavaObject outputStream = GetContentResolver().Call<AndroidJavaObject>("openOutputStream", uriResult);
        outputStream.Call("write", imageBytes);
        outputStream.Call("close");
        return filePath;
    }

    private AndroidJavaObject GetContentResolver()
    {
        using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            return currentActivity.Call<AndroidJavaObject>("getContentResolver");
        }
    }

    private void ScanMediaFile(string filePath)
    {
        // MediaScannerConnection을 통해 새 파일을 스캔하여 갤러리에서 바로 볼 수 있도록 함
        AndroidJavaClass mediaScannerConnectionClass = new AndroidJavaClass("android.media.MediaScannerConnection");
        AndroidJavaObject context = GetContext(); // Unity에서 현재 Context를 가져옵니다.

        // 스캔을 호출하여 새로 저장된 파일을 갤러리와 같은 앱에서 확인할 수 있도록 함
        mediaScannerConnectionClass.CallStatic("scanFile", context, new string[] { filePath }, null, null);
    }

    private AndroidJavaObject GetContext()
    {
        using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
#endif
    }
}
