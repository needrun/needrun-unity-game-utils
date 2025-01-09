using UnityEngine;

namespace NeedrunGameUtils
{
    public class InactivateWhenDemo : MonoBehaviour
    {
        public void ActivateOrNot()
        {
            if (VersionManager.IsDemo())
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            // 혹시나 다른 스크립트에 의해서 활성화되었을 수 있으므로, Active된 경우 다시 체크한다.
            ActivateOrNot();
        }
    }
}