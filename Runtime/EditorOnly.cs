using UnityEngine;

namespace NeedrunGameUtils
{
    public class EditorOnly : MonoBehaviour
    {
        private void Awake()
        {
            if (Application.isEditor == false)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            if (Application.isEditor == false)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
