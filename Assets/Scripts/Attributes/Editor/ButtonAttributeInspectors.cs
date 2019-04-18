using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Attributes.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class ButtonAttributeInspectors : UnityEditor.Editor
    {
        private MethodInfo[] Methods => target.GetType()
            .GetMethods(BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.NonPublic |
                        BindingFlags.Public);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawMethods();
        }

        private void DrawMethods()
        {
            if (Methods.Length < 1)
                return;

            foreach (var method in Methods)
            {
                var buttonAttribute = (ButtonAttribute) method
                    .GetCustomAttribute(typeof(ButtonAttribute));

                if (buttonAttribute != null)
                    DrawButton(buttonAttribute, method);
            }
        }

        public void DrawButton(ButtonAttribute buttonAttribute, MethodInfo method)
        {
            var label = buttonAttribute.Label ?? method.Name;

            if (GUILayout.Button(label))
                method.Invoke(target, null);
        }
    }
}