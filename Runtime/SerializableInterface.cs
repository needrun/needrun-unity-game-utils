using System;
using UnityEngine;

namespace NeedrunGameUtils
{
    [Serializable]
    public class SerializableInterface<T> where T : class
    {
        [SerializeField] private MonoBehaviour _object;
        public T Value => _object as T;
    }
}