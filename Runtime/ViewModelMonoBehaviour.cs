using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public class ViewModelMonoBehaviour<T> : MonoBehaviour
    {
        public readonly UnityEvent<T> onChanged = new UnityEvent<T>();
        public readonly Dictionary<string, UnityEvent<T>> onPropertyChanged = new Dictionary<string, UnityEvent<T>>();

        public void HandleChanged(T value, string propertyName = null)
        {
            onChanged.Invoke(value);
            if (propertyName != null && onPropertyChanged.ContainsKey(propertyName))
            {
                onPropertyChanged[propertyName].Invoke(value);
            }
            else if (propertyName == null)
            {
                foreach (UnityEvent<T> unityEvent in onPropertyChanged.Values)
                {
                    unityEvent.Invoke(value);
                }
            }
        }

        public void Subscribe(UnityAction<T> action)
        {
            onChanged.AddListener(action);
        }

        public void Subscribe(string propertyName, UnityAction<T> action)
        {
            if (!onPropertyChanged.ContainsKey(propertyName))
            {
                onPropertyChanged[propertyName] = new UnityEvent<T>();
            }
            onPropertyChanged[propertyName].AddListener(action);
        }

        public void Unsubscribe(UnityAction<T> action)
        {
            onChanged.RemoveListener(action);
        }

        public void Unsubscribe(string propertyName, UnityAction<T> action)
        {
            if (!onPropertyChanged.ContainsKey(propertyName))
            {
                return;
            }
            onPropertyChanged[propertyName].RemoveListener(action);
        }

        private void OnDestroy()
        {
            onChanged.RemoveAllListeners();
            onPropertyChanged.Values.ToList().ForEach(unityEvent => unityEvent.RemoveAllListeners());
        }
    }
}