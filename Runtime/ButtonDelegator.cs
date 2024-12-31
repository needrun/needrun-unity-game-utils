
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NeedrunGameUtils
{
    public class ButtonDelegator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField]
        public Button realButton;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (realButton != null)
            {
                ExecuteEvents.Execute(realButton.gameObject, eventData, ExecuteEvents.pointerDownHandler);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (realButton != null)
            {
                ExecuteEvents.Execute(realButton.gameObject, eventData, ExecuteEvents.pointerUpHandler);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (realButton != null)
            {
                ExecuteEvents.Execute(realButton.gameObject, eventData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
