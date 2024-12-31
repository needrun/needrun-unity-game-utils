using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public class NetworkChecker : MonoBehaviour
    {
        private const float checkInterval = 5f;
        private const float restoreCheckInterval = 1f;
        public readonly UnityEvent disconnected = new UnityEvent();
        public readonly UnityEvent reconnected = new UnityEvent();

        private void Awake()
        {
            StartCoroutine(MonitoringNetworkStatus());
        }

        private IEnumerator MonitoringNetworkStatus()
        {
            while (true)
            {
                // 연결 상태 출력
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    Debug.LogWarning("Network disconnected.");
                    // 연결이 끊긴 경우
                    disconnected.Invoke();

                    while (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        // 연결이 끊겨있는 동안은 해당 루프를 빠져나가지 못한다.
                        yield return new WaitForSecondsRealtime(restoreCheckInterval);
                    }

                    // 네트워크가 복구된 경우
                    reconnected.Invoke();
                }
                yield return new WaitForSecondsRealtime(checkInterval);
            }
        }
    }
}