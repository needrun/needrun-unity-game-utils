using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NeedrunGameUtils
{
    public class LocaleText : MonoBehaviour
    {
        [SerializeField]
        private string key;
        private Text text;
        private TMP_Text tmpText;

        private void Awake()
        {
            this.text = GetComponent<Text>();
            if (text != null)
            {
                this.text.text = I18n.GetValue(this.key.Trim());
                I18n.AddListener(ChangeText);
            }
            else
            {
                this.tmpText = GetComponent<TMP_Text>();
                if (this.tmpText != null)
                {
                    this.tmpText.text = I18n.GetValue(this.key.Trim());
                    I18n.AddListener(ChangeText);
                }
            }
        }

        private void OnDestroy()
        {
            I18n.RemoveListener(ChangeText);
        }

        private void ChangeText()
        {
            if (this.text != null)
            {
                this.text.text = I18n.GetValue(key.Trim());
            }
            else if (this.tmpText != null)
            {
                this.tmpText.text = I18n.GetValue(key.Trim());
            }
        }
    }
}
