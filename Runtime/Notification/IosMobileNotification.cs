using UnityEngine;
using System;

namespace NeedrunGameUtils
{
    public class IosMobileNotification : MonoBehaviour
    {
        private NotificationMemoryData memory = new NotificationMemoryData();

        private void Awake()
        {
            this.memory = NotificationMemoryData.Load();
        }

        public void SendNotification(string id, string title, string description, DateTime fireTime)
        {
#if UNITY_IOS
        if (memory.ios.Contains(id))
        {
            return;
        }

        try
        {
            var timeTrigger = new Unity.Notifications.iOS.iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = fireTime - DateTime.Now,
                Repeats = false
            };

            var notification = new Unity.Notifications.iOS.iOSNotification()
            {
                Identifier = id,
                Title = title,
                Body = description,
                ShowInForeground = false,
                ForegroundPresentationOption = Unity.Notifications.iOS.PresentationOption.Alert | Unity.Notifications.iOS.PresentationOption.Sound,
                CategoryIdentifier = "default",
                ThreadIdentifier = "thread1",
                Trigger = timeTrigger,
            };

            Unity.Notifications.iOS.iOSNotificationCenter.ScheduleNotification(notification);
            memory.ios.Add(id);
            memory.Save();
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
#endif
        }

        public void CancelAllNotifications()
        {
#if UNITY_IOS
        Unity.Notifications.iOS.iOSNotificationCenter.RemoveAllScheduledNotifications();
        memory.ios.Clear();
#endif
        }

        public void CancelNotification(string id)
        {
#if UNITY_IOS
        if (memory.ios.Contains(id))
        {
            Unity.Notifications.iOS.iOSNotificationCenter.RemoveScheduledNotification(id);
            memory.ios.Remove(id);
        }
#endif
        }
    }
}