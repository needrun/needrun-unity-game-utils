using System;

namespace NeedrunGameUtils
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class EditorButtonAttribute : Attribute
    {
        public string ButtonName { get; }

        public EditorButtonAttribute(string buttonName = null)
        {
            ButtonName = buttonName;
        }
    }
}