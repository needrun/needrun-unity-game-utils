using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfEnumAttribute))]
public class ShowIfEnumDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfEnumAttribute showIf = (ShowIfEnumAttribute)attribute;
        SerializedProperty enumProperty = GetEnumProperty(property, showIf.enumField);

        if (enumProperty != null && enumProperty.enumValueIndex == showIf.enumValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfEnumAttribute showIf = (ShowIfEnumAttribute)attribute;
        SerializedProperty enumProperty = GetEnumProperty(property, showIf.enumField);

        if (enumProperty != null && enumProperty.enumValueIndex == showIf.enumValue)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        return 0; // 높이를 0으로 설정하여 숨김
    }

    private SerializedProperty GetEnumProperty(SerializedProperty property, string enumField)
    {
        // 리스트 요소라면 상위 객체로 접근
        if (property.propertyPath.Contains(".Array.data["))
        {
            string path = property.propertyPath.Substring(0, property.propertyPath.IndexOf(".Array.data["));
            SerializedObject serializedObject = property.serializedObject;
            SerializedProperty parentProperty = serializedObject.FindProperty(path);

            return parentProperty?.FindPropertyRelative(enumField);
        }
        else
        {
            return property.serializedObject.FindProperty(enumField);
        }
    }
}
