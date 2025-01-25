using System;
using System.Collections;
using UnityEngine;

namespace NeedrunGameUtils
{
    public static class MonoBehaviourTimeExtension
    {
        public static DelayTask Delay(this MonoBehaviour monoBehaviour, float delaySeconds)
        {
            DelayTask delayTask = new DelayTask(delaySeconds);
            monoBehaviour.StartCoroutine(delayTask.Execute());
            return delayTask;
        }

        public class DelayTask
        {
            private readonly float _delaySeconds;
            private Action _onComplete;

            public DelayTask(float delaySeconds)
            {
                _delaySeconds = delaySeconds;
            }

            public DelayTask OnComplete(Action onComplete)
            {
                _onComplete += onComplete;
                return this;
            }

            public IEnumerator Execute()
            {
                yield return new WaitForSeconds(_delaySeconds);
                _onComplete?.Invoke();
            }
        }
    }
}