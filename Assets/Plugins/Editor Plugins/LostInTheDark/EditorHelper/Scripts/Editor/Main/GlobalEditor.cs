using LostIntheDark;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LostIntheDark
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class GlobalEditor : Editor
    {

        public bool DrawGlobalEditor()
        {
            serializedObject.Update();

            SerializedProperty prop = serializedObject.GetIterator();
            prop.NextVisible(true);
            do
            {
                if (GlobalEditor.IsVisible(prop))
                {
                    EditorGUILayout.PropertyField(prop);
                }
            }
            while (prop.NextVisible(false));

            return serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            this.DrawGlobalEditor();
        }

        internal static bool IsVisible(SerializedProperty prop)
        {
            var type = prop.serializedObject.targetObject.GetType();
            PropertyInfo propInfo = type.GetProperty(prop.name);
            ConditionalDisplayAttribute[] attr = propInfo != null ?
                (ConditionalDisplayAttribute[])propInfo.GetCustomAttributes(typeof(ConditionalDisplayAttribute), true) :
                (ConditionalDisplayAttribute[])type.GetField(prop.name)?.GetCustomAttributes(typeof(ConditionalDisplayAttribute), true);
            if (attr != null && attr.Length > 0)
            {
                ConditionalDisplayAttribute conditionalDisplay = attr[0];
                SerializedProperty sp = prop.serializedObject.FindProperty(conditionalDisplay.Property);
                
                if (string.IsNullOrEmpty(conditionalDisplay.Condition)
                    && sp?.boxedValue != null)
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(conditionalDisplay.Condition)
                    && GlobalEditor.ParseCondition(prop.serializedObject, conditionalDisplay.Property, conditionalDisplay.Condition, conditionalDisplay.Value))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        private static bool ParseCondition(SerializedObject sObject, string property, string condition, object value)
        {
            Type objType = sObject.targetObject.GetType();
            PropertyInfo propertyInfo = objType.GetProperty(property);
            FieldInfo fieldInfo = objType.GetField(property);

            object propertyValue = null;

            if (propertyInfo != null)
            {
                propertyValue = propertyInfo.GetValue(sObject.targetObject, null);
            }
            else if (fieldInfo != null)
            {
                propertyValue = fieldInfo.GetValue(sObject.targetObject);
            }
            switch (condition)
            {
                case "=":
                    return System.Object.Equals(propertyValue, value);
                case "!=":
                    return !System.Object.Equals(propertyValue, value);
                case "in":
                    return value is Array? Array.IndexOf(value as Array, propertyValue) >= 0: false;
            }

            return false;
        }
    }
}