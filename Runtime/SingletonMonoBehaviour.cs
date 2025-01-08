using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T instance;
        public static UnityEvent<T> instanceAssigned = new UnityEvent<T>();
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
    }
}