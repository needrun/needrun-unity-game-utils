using UnityEngine;

namespace NeedrunGameUtils
{
    public class ShowIfEnumAttribute : PropertyAttribute
    {
        public string enumField;
        public int[] enumValues = new int[0];

        public ShowIfEnumAttribute(string enumField, params int[] enumValues)
        {
            this.enumField = enumField;
            this.enumValues = enumValues;
        }
    }
}