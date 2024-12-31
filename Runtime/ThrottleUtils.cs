using System;
using System.Collections.Generic;
using System.Timers;

namespace NeedrunGameUtils
{
    public class ThrottleUtils
    {
        private static ThrottleUtils _instance;
        private readonly Dictionary<string, (Action action, Timer timer)> throttleMap;
        private readonly object lockObject = new object(); // 스레드 세이프를 검증하기 위해

        private ThrottleUtils()
        {
            throttleMap = new Dictionary<string, (Action, Timer)>();
        }

        public static ThrottleUtils instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThrottleUtils();
                }
                return _instance;
            }
        }

        // 지정한 기간안에 함수 요청이 연달아 들어오면 마지막에 들어오는 함수만 처리하도록 한다. (스로틀링 처리)
        // 시간(초)  00, 02, 06, 07, 10, 13, 20, 23, 30, 31
        // 이벤트     a,  b,  c,  d,  e,  f,  g,  h,  i,  j
        // 실제호출 -> b, d, f, h, j
        public void ThrottleLast(string key, Action action, double throttleTimeSeconds)
        {
            lock (lockObject)
            {
                if (throttleMap.ContainsKey(key))
                {
                    // 기존 타이머가 있다면 재설정
                    var entry = throttleMap[key];
                    entry.action = action; // 액션을 최신 액션으로 변경
                    throttleMap[key] = entry;
                    return;
                }
                else
                {
                    // 요청받은 함수가 있다면 n초 뒤 실행이 되도록 설정한다.
                    Timer timer = new Timer(throttleTimeSeconds * 1000);
                    timer.Elapsed += (sender, e) => ExecuteAction(key);
                    timer.AutoReset = false;
                    timer.Start();
                    throttleMap[key] = (action, timer);
                }
            }
        }

        private void ExecuteAction(string key)
        {
            lock (lockObject)
            {
                if (throttleMap.ContainsKey(key))
                {
                    var entry = throttleMap[key];
                    entry.action.Invoke();
                    entry.timer.Stop();
                    entry.timer.Dispose();
                    throttleMap.Remove(key);
                }
            }
        }

        public void DisposeThrottle(string key)
        {
            lock (lockObject)
            {
                if (throttleMap.ContainsKey(key))
                {
                    var entry = throttleMap[key];
                    entry.timer.Stop();
                    entry.timer.Dispose();
                    throttleMap.Remove(key);
                }
            }
        }
    }
}