using UnityEngine;

public class ShowIfEnumAttribute : PropertyAttribute
{
    public string enumField;
    public int enumValue;

    public ShowIfEnumAttribute(string enumField, int enumValue)
    {
        this.enumField = enumField;
        this.enumValue = enumValue;
    }
}
