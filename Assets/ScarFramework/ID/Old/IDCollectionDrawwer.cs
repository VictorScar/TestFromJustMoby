using System;
using UnityEditor;
using UnityEngine;

namespace ScarFramework
{
    [CustomEditor(typeof(DropdownIDCollection))]
    public class IDCollectionDrawwer : Editor
    {
        private string _newIdName = string.Empty;
        private DropdownIDCollection _idCollection;

        public override void OnInspectorGUI()
        {
            _idCollection = (DropdownIDCollection)target;
          
            serializedObject.Update();

            SerializedProperty groupIDProperty = serializedObject.FindProperty("groupID");
            
            var groupID = EditorGUILayout.IntField("GroupID", groupIDProperty.intValue);

            groupIDProperty.intValue = groupID;
            
            SerializedProperty listProperty = serializedObject.FindProperty("ids");
           
            DrawList();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            _newIdName = EditorGUILayout.TextField("New ID Name", _newIdName);

            if (GUILayout.Button("Add New Element"))
            {
                AddNewElement(listProperty);
            }
            
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private void AddNewElement(SerializedProperty listProperty)
        {
            if (!IsIdNameUsed(_idCollection, _newIdName))
            {
                int lastIndex = listProperty.arraySize;
                var newElement = new DropdownID { IDName = _newIdName, ID = _idCollection.CombineID(lastIndex) };

                _idCollection.IDs.Add(newElement);

                serializedObject.ApplyModifiedProperties();
                _newIdName = string.Empty;
            }
            else
            {
                EditorUtility.DisplayDialog("Ошибка", "Имя ID уже используется.", "ОК");
            }
        }

        private void RemoveElement(SerializedProperty listProperty, int index)
        {
            listProperty.DeleteArrayElementAtIndex(index);

            for (int i = index; i < listProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(i);
                var idProperty = elementProperty.FindPropertyRelative("ID");
                idProperty.intValue = i;
            }
        }

        private bool IsIdNameUsed(DropdownIDCollection idCollection, string idName)
        {
            if (idCollection.IDs != null)
            {
                foreach (var id in idCollection.IDs)
                {
                    if (id.IDName == idName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void DrawList()
        {
            SerializedProperty listProperty = serializedObject.FindProperty("ids");
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(i);
                elementProperty.isExpanded = true;
                EditorGUILayout.PropertyField(elementProperty, new GUIContent($"Element {i}"));
                
                if (GUILayout.Button("Remove"))
                {
                    RemoveElement(listProperty, i);
                    
                    serializedObject.ApplyModifiedProperties();
                    return; // Чтобы перерисовать список после удаления
                }
                
                /*if (GUILayout.Button("Rename"))
                {
                    var element = listProperty.GetArrayElementAtIndex(i);
                    var nameProperty = element.FindPropertyRelative("IDName");

                    _idCollection.IDs[i] = new DropdownID {IDName = nameProperty.stringValue, ID = _idCollection.CombineID(_idCollection.IDs[i].ID)};
                    
                    serializedObject.ApplyModifiedProperties();
                    return; // Чтобы перерисовать список после удаления
                }*/
            }
        }
    }
}