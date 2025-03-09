using System;
using UnityEngine;
using UnityEditor;

namespace NeedrunGameUtils
{
    [CustomPropertyDrawer(typeof(SerializableInterfaceAttribute), true)]
    public class SerializableInterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty objectProp = property.FindPropertyRelative("_object");
            EditorGUI.ObjectField(position, objectProp, typeof(MonoBehaviour), label);

            EditorGUI.EndProperty();
        }
    }
}