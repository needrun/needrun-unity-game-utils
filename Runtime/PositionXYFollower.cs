using UnityEngine;

namespace NeedrunGameUtils
{
    public class PositionXYFollower : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        void FixedUpdate()
        {
            if (target != null)
            {
                transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            }
        }
    }
}
