using System;
using System.Collections;
using UnityEngine;

namespace NeedrunGameUtils
{
    // ref.1: https://developer.android.com/guide/playcore/in-app-review/unity?hl=ko
    // ref.2: https://developers.google.com/unity/packages?hl=ko#play_in-app_review
    public class AndroidStoreReview : MonoBehaviour
    {
        public void RequestReview(Action onSuccess, Action<string> onError)
        {
            StartCoroutine(RequestGooglePlayStoreReviewCoroutine(onSuccess, onError));
        }

        public IEnumerator RequestGooglePlayStoreReviewCoroutine(Action onSuccess, Action<string> onError)
        {
#if UNITY_ANDROID
        // 구글 리뷰 가이드
        // '할당량'
        //  - 우수한 사용자 환경을 제공하기 위해 Google Play는 사용자에게 리뷰 대화상자를 표시할 수 있는 빈도에 관한 시간제한 할당량을 적용합니다.
        //  - 이 할당량으로 인해 짧은 기간(예: 1개월 미만) launchReviewFlow 메서드를 두 번 이상 호출할 경우 대화상자가 표시되지 않을 수도 있습니다.
        Google.Play.Review.ReviewManager reviewManager = new Google.Play.Review.ReviewManager();
        var requestFlowOperation = reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            if (onError != null)
            {
                onError("Request review failed. " + requestFlowOperation.Error);
            }
            yield break;
        }
        Google.Play.Review.PlayReviewInfo playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
        yield return launchFlowOperation;
        playReviewInfo = null;
        if (launchFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            if (onError != null)
            {
                onError("Launch request review failed. " + launchFlowOperation.Error);
            }
            yield break;
        }
        Debug.Log("Open Review Popup");
        if (onSuccess != null)
        {
            onSuccess();
        }
#endif
            yield break;
        }
    }
}