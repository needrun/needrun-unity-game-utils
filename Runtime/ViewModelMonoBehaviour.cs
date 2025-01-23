using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public class ViewModelMonoBehaviour : MonoBehaviour
    {
        private readonly UnityEvent onChanged = new UnityEvent();
        private readonly Dictionary<string, UnityEvent> onPropertyChanged = new Dictionary<string, UnityEvent>();
        private bool changed = false;
        private HashSet<string> changedPropertyNames = new HashSet<string>();

        public void OnChanged(string propertyName = null)
        {
            changed = true;
            if (propertyName != null)
            {
                changedPropertyNames.Add(propertyName);
            }
        }

        public void Subscribe(UnityAction action)
        {
            onChanged.AddListener(action);
        }

        public void Subscribe(string propertyName, UnityAction action)
        {
            if (!onPropertyChanged.ContainsKey(propertyName))
            {
                onPropertyChanged[propertyName] = new UnityEvent();
            }
            onPropertyChanged[propertyName].AddListener(action);
        }

        public void Unsubscribe(UnityAction action)
        {
            onChanged.RemoveListener(action);
        }

        public void Unsubscribe(string propertyName, UnityAction action)
        {
            if (!onPropertyChanged.ContainsKey(propertyName))
            {
                return;
            }
            onPropertyChanged[propertyName].RemoveListener(action);
        }

        private void Update()
        {
            // 변경된 사실은 Update 프레임에 1번만 나간다.
            // Invoke된 리스너 안에서 다시 변경이 발생하면 changed와 changedPropertyNames가 변경이 일어날 수 있다.
            // 따라서 불변성을 유지하기 위해 Update안에서는 changed와 changedPropertyNames의 복사본을 사용한다.
            bool copiedChanged = this.changed;
            HashSet<string> copiedChangedPropertyNames = new HashSet<string>(this.changedPropertyNames);

            // 기존에 변경된 사실은 초기화한다.
            this.changed = false;
            this.changedPropertyNames.Clear();

            // 리스너에 변경된 사실을 알린다.
            if (copiedChanged)
            {
                onChanged.Invoke();
            }

            if (copiedChangedPropertyNames.Count != 0)
            {
                foreach (string propertyName in copiedChangedPropertyNames)
                {
                    if (onPropertyChanged.ContainsKey(propertyName))
                    {
                        onPropertyChanged[propertyName].Invoke();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            onChanged.RemoveAllListeners();
            onPropertyChanged.Values.ToList().ForEach(unityEvent => unityEvent.RemoveAllListeners());
        }
    }
}