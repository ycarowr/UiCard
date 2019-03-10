using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Attributes.Editor
{
    /// <summary>
    ///     Overriding the UnityEditor class in order to support the button attribute.
    /// </summary>
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
            DrawButtons();
        }

        private void DrawButtons()
        {
            if (Methods.Length < 1)
                return;

            //get all methods
            foreach (var method in Methods)
            {
                //get the button attribute
                var buttonAttribute = (ButtonAttribute) method
                    .GetCustomAttribute(typeof(ButtonAttribute));

                //if button attribute is present, draw it
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