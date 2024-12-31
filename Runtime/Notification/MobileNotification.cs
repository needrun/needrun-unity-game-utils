using UnityEngine;
using System;

namespace NeedrunGameUtils
{
        public class MobileNotification : SingletonMonoBehaviour<MobileNotification>
        {
                [SerializeField]
                private AndroidMobileNotification androidMobileNotification;
                [SerializeField]
                private IosMobileNotification iosMobileNotification;

                public void SendNotification(string id, string title, string description, DateTime fireTime)
                {
#if UNITY_EDITOR
                        // Do nothing
#elif UNITY_ANDROID
        androidMobileNotification.SendNotification(id, title, description, fireTime);
#elif UNITY_IOS
        iosMobileNotification.SendNotification(id, title, description, fireTime);
#endif
                }

                public void CancelAllNotifications()
                {
#if UNITY_EDITOR
                        // Do nothing
#elif UNITY_ANDROID
        androidMobileNotification.CancelAllNotifications();
#elif UNITY_IOS
        iosMobileNotification.CancelAllNotifications();
#endif
                }

                public void CancelNotification(string id)
                {
#if UNITY_EDITOR
                        // Do nothing
#elif UNITY_ANDROID
        androidMobileNotification.CancelNotification(id);
#elif UNITY_IOS
        iosMobileNotification.CancelNotification(id);
#endif
                }
        }
}