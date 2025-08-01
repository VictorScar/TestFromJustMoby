using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScarFramework.ID
{
    [CustomPropertyDrawer(typeof(IDDropdownView))]
    public class IDDropdownDrawer : PropertyDrawer
    {
        private string _path = "Assets//ScarFramework//ID//Resources//IDCollection.asset";
        private List<string> _names = new List<string>();
        private DropdownIDCollection _idList;
        private int _selectedIndex = -1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (_idList == null)
            {
                _idList = GetOrCreateIDConfig();
            }

            if (GetIDNames())
            {
                SerializedProperty idProp = property.FindPropertyRelative("ID");
                SerializedProperty nameProp = property.FindPropertyRelative("IDName");
                _selectedIndex = _idList.GetIndex(idProp.intValue);


                //  int newIndex = property.intValue;
                int newIndex = EditorGUI.Popup(position, _selectedIndex, _names.ToArray());
                int newID = _idList.IDs[newIndex].ID;

                if (newIndex != _selectedIndex)
                {
                    _selectedIndex = newIndex;
                }

                idProp.intValue = _idList.IDs[_selectedIndex].ID;
                nameProp.stringValue = _idList.IDs[_selectedIndex].IDName;
         
            }
            else
            {
                _selectedIndex = -1;
            }


            EditorGUI.EndProperty();
       
        }

        private DropdownIDCollection GetOrCreateIDConfig()
        {
            var idConfig = AssetDatabase.LoadAssetAtPath<DropdownIDCollection>(_path);

            if (idConfig == null)
            {
                var newIDCollection = ScriptableObject.CreateInstance<DropdownIDCollection>();

                AssetDatabase.CreateAsset(newIDCollection, _path);
                AssetDatabase.SaveAssets();
                EditorUtility.FocusProjectWindow();
                idConfig = newIDCollection;
            }

            return idConfig;
        }

        private bool GetIDNames()
        {
            _names.Clear();

            if (_idList.IDs != null)
            {
                foreach (var id in _idList.IDs)
                {
                    _names.Add(id.IDName);
                }

                return true;
            }

            return false;
        }
    }
}