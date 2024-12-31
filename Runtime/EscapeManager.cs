using UnityEngine;
using UnityEngine.Events;

namespace NeedrunGameUtils
{
    public class EscapeManager : SingletonMonoBehaviour<EscapeManager>
    {
        private int clickCount = 0;
        private UnityEvent clickEscape = new UnityEvent();
        private UnityEvent clickOnce = new UnityEvent();
        private UnityEvent clickTwice = new UnityEvent();
        private bool isClickOnceCalled = false;
        private bool isClickTwiceCalled = false;

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                clickCount++;
                clickEscape.Invoke();
                if (!IsInvoking("ResetClickCount"))
                {
                    Invoke("ResetClickCount", 1.0f);
                }
            }

            if (clickCount == 1)
            {
                if (!isClickOnceCalled)
                {
                    clickOnce.Invoke();
                }
                isClickOnceCalled = true;
            }
            else if (clickCount == 2)
            {
                if (!isClickTwiceCalled)
                {
                    CancelInvoke("ResetClickCount");
                    clickTwice.Invoke();
                    clickCount = 0;
                    isClickTwiceCalled = true;
                }
            }

        }

        private void ResetClickCount()
        {
            clickCount = 0;
            isClickOnceCalled = false;
            isClickTwiceCalled = false;
        }
    }
}
