using UnityEngine;

namespace NeedrunGameUtils
{
    public class MobileOnly : MonoBehaviour
    {
        [SerializeField]
        private bool enableDevMode = true;

        private void Awake()
        {
            if (Application.isMobilePlatform || (enableDevMode ? Application.isEditor : false))
            {
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}