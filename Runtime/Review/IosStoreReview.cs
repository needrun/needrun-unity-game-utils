using System;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class IosStoreReview : MonoBehaviour
    {
        public void RequestReview(Action onSuccess, Action<string> onError)
        {
            try
            {
#if UNITY_IOS
            // 앱스토어 가이드
            // 1년에 최대 3번까지 평가를 요청할 수 있습니다.
            UnityEngine.iOS.Device.RequestStoreReview();
            if (onSuccess != null)
            {
                onSuccess();
            }
#endif
            }
            catch (Exception e)
            {
                if (onError != null)
                {
                    onError("Request review failed. " + e.Message);
                }
            }
        }
    }
}