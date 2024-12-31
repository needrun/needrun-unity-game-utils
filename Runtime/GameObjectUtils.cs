using System;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class GameObjectUtils
    {
        public static GameObject[] GetChildrenGameObject(GameObject parent)
        {
            int childCount = parent.transform.childCount;
            GameObject[] childObjects = new GameObject[childCount];
            for (int i = 0; i < childCount; i++)
            {
                childObjects[i] = parent.transform.GetChild(i).gameObject;
            }
            return childObjects;
        }

        public static void RemoveChildren(GameObject parent)
        {
            foreach (Transform child in parent.transform)
                GameObject.Destroy(child.gameObject);
        }

        public static int CountActiveChildren(GameObject parent)
        {
            return CountActiveChildren(parent, null);
        }

        public static int CountActiveChildren(GameObject parent, Action<GameObject> activeChildCallback)
        {
            int visibleCount = 0;
            foreach (Transform child in parent.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    visibleCount++;
                    if (activeChildCallback != null)
                    {
                        activeChildCallback.Invoke(child.gameObject);
                    }
                }
            }
            return visibleCount;
        }

    }
}