using System;
using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public class DateChangeMonitoring : SingletonMonoBehaviour<DateChangeMonitoring>
    {
        public readonly UnityEvent dateChangedEvent = new UnityEvent();
        private DateTimeOffset lastMonitoringDateTimeOffset;
        private bool pause = false;

        private void Start()
        {
            // 최초에만 InvokeRepeating을 실행
            if (!IsInvoking("MonitoringDateChanged"))
            {
                lastMonitoringDateTimeOffset = TimeUtils.GetNetworkDateTimeOffsetOrLocalAtKorea();
                // 해당 객체가 Start를 호출할 때 바로 호출되고 그 이후에는 1초에 한번씩 MonitoringDateChanged을 호출
                InvokeRepeating("MonitoringDateChanged", 0f, 1f);
            }
        }

        private void MonitoringDateChanged()
        {
            if (pause)
            {
                return;
            }
            DateTimeOffset currentDateTimeOffset = TimeUtils.GetNetworkDateTimeOffsetOrLocalAtKorea();
            if (TimeUtils.IsDateEqual(currentDateTimeOffset, lastMonitoringDateTimeOffset, TimeUtils.KOREA_TIME_ZONE))
            {
                return;
            }
            else
            {
                Debug.LogFormat("Date change detected. PREV: {0}, CURRENT: {1}",
                   TimeUtils.Format(lastMonitoringDateTimeOffset),
                   TimeUtils.Format(currentDateTimeOffset));
                lastMonitoringDateTimeOffset = currentDateTimeOffset;
                dateChangedEvent.Invoke();
            }
        }

        public void Resume()
        {
            pause = false;
        }

        public void Stop()
        {
            pause = true;
        }
    }
}