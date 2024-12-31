using UnityEngine;
using System;

namespace NeedrunGameUtils
{
    public class AndroidMobileNotification : MonoBehaviour
    {
        private NotificationMemoryData memory = new NotificationMemoryData();
        private string channelId = $"com.{Application.companyName}.{Application.productName}";

        private void Awake()
        {
            this.memory = NotificationMemoryData.Load();
        }

        public void Start()
        {
            try
            {
#if UNITY_ANDROID
            // 디바이스의 안드로이드 api level 얻기
            //
            // SystemInfo.operatingSystem 은 다음과 같은 형식으로 반환됩니다.
            // ref: https://docs.unity3d.com/ScriptReference/SystemInfo-operatingSystem.html
            // Prints "Windows 11 (10.0.22621) 64bit" on 64 bit Windows 11
            // Prints "Mac OS X 13.4" on Mac OS Ventura
            // Prints "iPhone OS" with iOS 15.3.1
            // Prints "iPad OS" on iPad with iOS 16
            // Prints "Android OS 13 / API-33 (TQ2A.230305.008.C1/9619669)"
            if (!SystemInfo.operatingSystem.Contains("Android"))
            {
                return;
            }
            string androidInfo = SystemInfo.operatingSystem;
            int apiLevel = int.Parse(androidInfo.Substring(androidInfo.IndexOf("-") + 1, 2));
            Debug.Log("androidInfo: " + androidInfo);
            Debug.Log("apiLevel: " + apiLevel);

            // 디바이스의 api level이 33 이상이라면 퍼미션 요청
            if (apiLevel >= 33 && !UnityEngine.Android.Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            {
                if (LocalStorage.Exist("NotificationPermissionRequested"))
                {
                    // 알람 권한이 없지만, 이미 요청했던 경우
                    return;
                }
                UnityEngine.Android.Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
                LocalStorage.Save("NotificationPermissionRequested", "true");
            }

            // 디바이스의 api level이 26 이상이라면 알림 채널 설정
            if (apiLevel >= 26)
            {
                var channel = new Unity.Notifications.Android.AndroidNotificationChannel()
                {
                    Id = channelId,
                    Name = "Default Channel",
                    Importance = Unity.Notifications.Android.Importance.Default,
                    Description = "Generic notifications",
                };
                Unity.Notifications.Android.AndroidNotificationCenter.RegisterNotificationChannel(channel);
            }
#endif
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public void SendNotification(string id, string title, string description, DateTime fireTime)
        {
            try
            {
#if UNITY_ANDROID
            var notification = new Unity.Notifications.Android.AndroidNotification();
            notification.Title = title;
            notification.Text = description;
            notification.FireTime = fireTime;
            notification.SmallIcon = "icon_1";
            notification.LargeIcon = "icon_0";
            notification.ShowInForeground = false; // 앱이 실행중일 때도 시간이 되면 알림을 보여줄지 말지 결정합니다.
            int notificationId = Unity.Notifications.Android.AndroidNotificationCenter.SendNotification(notification, channelId);
            memory.android.TryAdd(id, notificationId);
            memory.Save();
#endif
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public void CancelAllNotifications()
        {
#if UNITY_ANDROID
        Unity.Notifications.Android.AndroidNotificationCenter.CancelAllNotifications();
        memory.android.Clear();
#endif
        }

        public void CancelNotification(string id)
        {
#if UNITY_ANDROID
        if (memory.android.TryGetValue(id, out int notificationId))
        {
            Unity.Notifications.Android.AndroidNotificationCenter.CancelNotification(notificationId);
            memory.android.Remove(id);
        }
#endif
        }
    }
}