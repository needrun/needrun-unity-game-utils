using UnityEngine;

namespace NeedrunGameUtils
{
    public class WaitFor
    {
        public static WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        public static WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        public static WaitForSecondsRealtime realOneSecond = new WaitForSecondsRealtime(1);
        public static WaitForSecondsRealtime realHalfSecond = new WaitForSecondsRealtime(0.5f);
    }
}
