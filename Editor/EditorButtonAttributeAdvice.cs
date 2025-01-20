using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace NeedrunGameUtils
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class EditorButtonAttributeAdvice : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // 대상 객체 가져오기
            MonoBehaviour targetObject = (MonoBehaviour)target;

            // 대상 객체의 모든 메서드 탐색
            MethodInfo[] methods = targetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (MethodInfo method in methods)
            {
                // ButtonAttribute가 정의된 메서드만 처리
                EditorButtonAttribute editorButtonAttribute = method.GetCustomAttribute<EditorButtonAttribute>();
                if (editorButtonAttribute != null)
                {
                    string buttonName = string.IsNullOrEmpty(editorButtonAttribute.ButtonName) ? method.Name : editorButtonAttribute.ButtonName;
                    if (GUILayout.Button(buttonName))
                    {
                        method.Invoke(targetObject, null);
                    }
                }
            }
        }
    }
}