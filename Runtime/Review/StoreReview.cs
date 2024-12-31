using System;
using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public class StoreReview : SingletonMonoBehaviour<StoreReview>
    {
        public UnityEvent<string> editorRequestReviewEvent = new UnityEvent<string>();
        [SerializeField]
        private IosStoreReview iosStoreReview;
        [SerializeField]
        private AndroidStoreReview androidStoreReview;

        public void RequestReview(Action onSuccess, Action<string> onError)
        {
#if UNITY_EDITOR
            InsteadToastAlarm();
#elif UNITY_ANDROID
        if (Configuration.IS_ONESTORE_BUILD == false)
        {
            androidStoreReview.RequestReview(onSuccess, onError);
        }
#elif UNITY_IOS
        iosStoreReview.RequestReview(onSuccess, onError);
#endif
        }

        public void InsteadToastAlarm()
        {
            editorRequestReviewEvent.Invoke("[EDITOR] Request store review!");
        }
    }
}