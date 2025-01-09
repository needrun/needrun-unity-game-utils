using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    // 싱글톤
    public class NetworkChecker : MonoBehaviour
    {
        public static NetworkChecker instance;
        private static UnityEvent<NetworkChecker> instanceAssigned = new UnityEvent<NetworkChecker>();
        private const float checkInterval = 5f;
        private const float restoreCheckInterval = 1f;
        public readonly UnityEvent disconnected = new UnityEvent();
        public readonly UnityEvent reconnected = new UnityEvent();

        private void Awake()
        {
            // 코루틴 실행 동작이 Awake에 있어야 하는 동작이라 부득이하게 SingletonMonoBehaviour를 사용하지 않고 직접 싱글톤을 구현
            if (instance == null)
            {
                instance = this;
                instanceAssigned.Invoke(instance);
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            StartCoroutine(MonitoringNetworkStatus());
        }

        public static void AddInstanceAssignedListener(UnityAction<NetworkChecker> action)
        {
            if (instance != null)
            {
                action(instance);
            }
            instanceAssigned.AddListener(action);
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