using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeedrunGameUtils
{
    public class LocaleImage : MonoBehaviour
    {
        public List<LocaleToSprite> localeToSprites = new List<LocaleToSprite>();
        private Image image;

        private void Awake()
        {
            this.image = GetComponent<Image>();
            if (image != null)
            {
                Locale locale = I18n.GetCurrentLocale();
                ChangeImage(locale);
                I18n.AddListener(() =>
                {
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        Locale locale = I18n.GetCurrentLocale();
                        ChangeImage(locale);
                    });
                });
            }
        }

        private void ChangeImage(Locale locale)
        {
            foreach (LocaleToSprite localeToSprite in localeToSprites)
            {
                if (localeToSprite.locale == locale)
                {
                    this.image.sprite = localeToSprite.sprite;
                    break;
                }
            }
        }
    }
}
