using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace LostIntheDark
{
    [CustomPropertyDrawer(typeof(AssetListAttribute))]
    [CustomPropertyDrawer(typeof(ListManagerAttribute))]
    public class ListManagerAttributeDrawer : PropertyDrawer
    {
        protected int rowCount = 0;
        protected bool open = false;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AssetListAttribute attribute = (AssetListAttribute)this.attribute;
            SerializedProperty iterator = property.serializedObject.FindProperty(property.propertyPath);
            SerializedProperty nameProp = property.FindPropertyRelative(attribute.NameField);
            iterator.NextVisible(true);
            property.isExpanded = EditorGUI.Foldout(
                new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                property.isExpanded,
                string.IsNullOrEmpty(iterator.stringValue) ? "" : iterator.stringValue, true);

            this.rowCount = 0;
            do
            {
                this.rowCount++;
                if (property.isExpanded)
                {
                    Rect rowPos = new Rect(position.x, position.y + ((this.rowCount) * EditorGUIUtility.singleLineHeight), position.width, EditorGUIUtility.singleLineHeight);
                    if (attribute != null && attribute.SpecialFieldname == iterator.name)
                    {
                        this.CreateSpecialField(rowPos, iterator, attribute.Label, attribute.Type, nameProp);
                    }
                    else
                    {
                        EditorGUI.PropertyField(rowPos, iterator, new GUIContent(iterator.name), true);
                    }
                }
            } while (iterator.NextVisible(true) && property.depth < iterator.depth);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight * (this.rowCount + 1);
            }
            return base.GetPropertyHeight(property, label);
        }

        protected void CreateSpecialField(Rect rect, SerializedProperty property, string label, System.Type type, SerializedProperty nameProp)
        {
            var oldScene = AssetDatabase.LoadAssetAtPath(property.stringValue, type);

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUI.ObjectField(rect, string.IsNullOrEmpty(label) ? property.name : label, oldScene, type, false);

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                property.stringValue = newPath;
                if (nameProp != null)
                {
                    nameProp.stringValue = Path.GetFileNameWithoutExtension(newPath);
                }
            }
        }

    }
}