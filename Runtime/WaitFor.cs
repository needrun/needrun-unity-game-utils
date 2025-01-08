using UnityEngine;

namespace NeedrunGameUtils
{
    public class WaitFor
    {
        public static WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        public static WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        public static WaitForSecondsRealtime real_1_Second = new WaitForSecondsRealtime(1);
        public static WaitForSecondsRealtime real_half_Second = new WaitForSecondsRealtime(0.5f);
    }
}
