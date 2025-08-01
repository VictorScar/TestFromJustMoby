using System.Reflection;
using ScarFramework.Button;
using UnityEditor;
using UnityEngine;

namespace ScarFramework.Editor.Button
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class CustomEditorAtributeProcessor : UnityEditor.Editor
    {
       
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
          
            ProcessEditorButtons();
        }

        private void ProcessEditorButtons()
        {
            var targetType = target.GetType();
            var methods = targetType.GetMethods();

            foreach (var method in methods)
            {
                var btnAttr = method.GetCustomAttribute<ButtonAttribute>();

                if (btnAttr != null)
                {
                    if (GUILayout.Button(btnAttr.ButtonName))
                    {
                        method.Invoke(target, null);
                    }
                }

            }
        }
      
    }
}