using UnityEngine;

namespace NeedrunGameUtils
{
    public class MobileKeyboardRepositionPanel : MonoBehaviour
    {
        [SerializeField]
        private MobileKeyboardSimulator keyboardSample;
        [SerializeField]
        private Vector2 offset;
        private RectTransform _rectTransform;
        private RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    this._rectTransform = GetComponent<RectTransform>();
                }
                return _rectTransform;
            }
        }
        private Vector2 originAnchoredPosition;

        private Vector2 popupAnchoredPosition;

        void Awake()
        {
            this.originAnchoredPosition = rectTransform.anchoredPosition;
            this.popupAnchoredPosition = rectTransform.anchoredPosition + offset;
        }

        void Update()
        {
            if (TouchScreenKeyboard.visible || (keyboardSample != null && keyboardSample.simulateKeyboardPopup))
            {
                // 키보드가 올라와있는 경우
                rectTransform.anchoredPosition = this.popupAnchoredPosition;
            }
            else
            {
                // 키보드가 안보이는 경우
                rectTransform.anchoredPosition = this.originAnchoredPosition;
            }
        }
    }
}