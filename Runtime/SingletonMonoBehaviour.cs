using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T instance;
        private static UnityEvent<T> instanceAssigned = new UnityEvent<T>();
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
                instanceAssigned.Invoke(instance);
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public static void AfterInstanceAssigned(UnityAction<T> action)
        {
            if (instance != null)
            {
                action(instance);
            }
            instanceAssigned.AddListener(action);
        }
    }
}