using UnityEngine;
using UnityEngine.UI;

namespace NeedrunGameUtils
{
    public class ImageUtils
    {

        public static void SetAlpha(Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        public static void SetAlpha(CanvasGroup canvasGroup, float alpha)
        {
            canvasGroup.alpha = alpha;
        }

    }
}
