using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class ObjectPooling : MonoBehaviour
    {
        [Serializable]
        public enum PoolMode
        {
            LAZY,
            MANUAL,
            MANUAL_FROM_PARENT,
        }

        // ========================================================================================== PoolMode.LAZY
        [SerializeField]
        private PoolMode poolMode = PoolMode.LAZY;
        [SerializeField]
        [ShowIfEnum("poolMode", (int)PoolMode.LAZY)]
        private Transform objectPoolParent;
        [SerializeField]
        [ShowIfEnum("poolMode", (int)PoolMode.LAZY)]
        private GameObject element;
        [SerializeField]
        [ShowIfEnum("poolMode", (int)PoolMode.LAZY)]
        private int poolSize = 200;
        // ========================================================================================== PoolMode.MANUAL_FROM_PARENT
        [SerializeField]
        [ShowIfEnum("poolMode", (int)PoolMode.MANUAL_FROM_PARENT)]
        private GameObject instancesFromParent;
        // ==========================================================================================

        [SerializeField]
        [Header("[ === 아래는 Manual Mode일 때만 사용 === ]")]
        private List<GameObject> instances = new List<GameObject>();
        private List<GameObject> usableInstance = new List<GameObject>();
        public List<GameObject> usedInstance
        {
            get
            {
                List<GameObject> usedInstance = new List<GameObject>();
                foreach (GameObject instance in instances)
                {
                    if (usableInstance.Contains(instance) == false)
                    {
                        usedInstance.Add(instance);
                    }
                }
                return usedInstance;
            }
        }
        public bool isOutOfInstance => usableInstance.Count == 0;
        private bool isInitialized = false;

        public void Initialize()
        {
            {
                // for safe acess
                if (isInitialized)
                {
                    return;
                }
                isInitialized = true;
            }

            usableInstance.Clear();
            switch (poolMode)
            {
                case PoolMode.LAZY:

                    element.SetActive(false);
                    instances.Clear();
                    for (int i = 0; i < poolSize; i++)
                    {
                        GameObject forked = Instantiate(element, objectPoolParent.transform);
                        forked.SetActive(false);
                        instances.Add(forked);
                        usableInstance.Add(forked);
                    }
                    break;
                case PoolMode.MANUAL_FROM_PARENT:
                    instances.Clear();
                    foreach (Transform child in instancesFromParent.transform)
                    {
                        instances.Add(child.gameObject);
                    }
                    break;
                default:
                case PoolMode.MANUAL:
                    break;
            }
        }

        public void Clean()
        {
            {
                // for safe acess
                if (isInitialized == false)
                {
                    Initialize();
                }
            }

            usableInstance.Clear();
            foreach (GameObject instance in instances)
            {
                instance.SetActive(false);
                usableInstance.Add(instance);
            }
        }

        public GameObject GetInstance()
        {
            {
                // for safe acess
                if (isInitialized == false)
                {
                    Initialize();
                }
            }
            if (usableInstance.Count == 0)
            {
                return null;
            }
            GameObject instance = usableInstance[0];
            usableInstance.RemoveAt(0);
            instance.SetActive(true);
            return instance;
        }

        public GameObject ReturnInstance(GameObject instance)
        {
            {
                // for safe acess
                if (isInitialized == false)
                {
                    Initialize();
                }
            }
            instance.SetActive(false);
            usableInstance.Add(instance);
            return instance;
        }
    }
}