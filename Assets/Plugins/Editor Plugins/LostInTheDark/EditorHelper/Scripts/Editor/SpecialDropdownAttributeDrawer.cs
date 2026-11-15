using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LostIntheDark
{
    [CustomPropertyDrawer(typeof(LinkedDropdownAttribute))]
    public class SpecialDropdownAttributeDrawer : PropertyDrawer
    {
        protected int dropdownSelected = -1;
        public List<string> dropdownOptions = new List<string>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LinkedDropdownAttribute attributes = ((LinkedDropdownAttribute)this.attribute);

            SerializedProperty field = property.serializedObject.FindProperty(attributes.SrcFieldname);

            if (field != null && field.isArray)
            {
                this.dropdownOptions.Clear();
                this.dropdownOptions.Add("None");
                for (int i = 0; i < field.arraySize; i++)
                {
                    SerializedProperty element = field.GetArrayElementAtIndex(i);
                    string value;
                    if (!string.IsNullOrEmpty(attributes.SrcSubFieldname))
                    {
                        value = element.FindPropertyRelative(attributes.SrcSubFieldname)?.stringValue;
                    }
                    else
                    {
                        value = element.stringValue;
                    }
                    this.dropdownOptions.Add(value);
                    if (value == property.stringValue)
                    {
                        this.dropdownSelected = i + 1;
                    }
                }
            }

            this.dropdownSelected = EditorGUI.Popup(position, !string.IsNullOrEmpty(attributes.Label) ? attributes.Label : label.text, this.dropdownSelected, this.dropdownOptions.ToArray());
            property.stringValue = this.dropdownOptions.Count > 1 && this.dropdownSelected > 0 ? this.dropdownOptions[this.dropdownSelected].ToString() : "None";
        }
    }
}