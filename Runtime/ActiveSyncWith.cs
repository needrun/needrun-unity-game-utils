using UnityEngine;

namespace NeedrunGameUtils
{
    public class ActiveSyncWith : MonoBehaviour
    {
        [SerializeField]
        private GameObject from;
        [SerializeField]
        private GameObject[] to;

        void Update()
        {
            foreach (GameObject element in to)
            {
                element.SetActive(from.activeInHierarchy);
            }
        }
    }
}
