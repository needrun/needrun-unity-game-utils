using UnityEngine;

namespace NeedrunGameUtils
{
    public class MobileKeyboardSimulator : MonoBehaviour
    {
        [SerializeField]
        public bool simulateKeyboardPopup;
        [SerializeField]
        private GameObject canvas;

        void Update()
        {
            if (simulateKeyboardPopup)
            {
                ShowKeyboardSampleImage();
            }
            else
            {
                HideKeyboardSampleImage();
            }
        }

        public void ShowKeyboardSampleImage()
        {
            this.canvas.SetActive(true);
        }

        public void HideKeyboardSampleImage()
        {
            this.canvas.SetActive(false);
        }

    }
}