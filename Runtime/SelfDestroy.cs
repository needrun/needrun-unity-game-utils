using UnityEngine;

namespace NeedrunGameUtils
{
    public class SelfDestroy : MonoBehaviour
    {
        [SerializeField]
        private float lifetime = 1.0f;

        void Start()
        {
            this.Delay(lifetime).OnComplete(() => Destroy(gameObject));
        }
    }
}
