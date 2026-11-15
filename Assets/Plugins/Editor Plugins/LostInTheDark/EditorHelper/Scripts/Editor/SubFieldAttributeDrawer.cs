using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using System.Reflection;
using System.Linq;
using Unity.VisualScripting;
namespace LostIntheDark
{
    [CustomPropertyDrawer(typeof(SubFieldAttribute))]
    public class SubFieldAttributeDrawer : PropertyDrawer
    {
        protected Dictionary<string, bool> foldout = new Dictionary<string, bool>();
        public static GUIStyle GetStyle(Color color)
        {
            GUIStyle style = new GUIStyle();
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            style.normal.background = texture;
            return style;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property);
            List<string> fieldsName = ((SubFieldAttribute)this.attribute).SubFieldsName;
            bool isPacked = ((SubFieldAttribute)this.attribute).IsPacked;
            if (property != null)
            {
                if (!this.foldout.ContainsKey(property.name))
                {
                    this.foldout.Add(property.name, false);
                }
                EditorGUI.indentLevel++;
                foldout[property.name] = EditorGUILayout.Foldout(foldout[property.name], "Details");
                if (this.foldout[property.name])
                {
                    Color color = isPacked ? new Color(0.3f, 0.3f, 0.3f, 1f) : Color.clear;

                    EditorGUILayout.BeginVertical(SubFieldAttributeDrawer.GetStyle(color));
                    if (property.objectReferenceValue != null)
                    {
                        SerializedObject tmp = new SerializedObject(property.objectReferenceValue);
                        if (fieldsName != null && fieldsName.Count > 0)
                        {
                            for (int i = 0; i < fieldsName.Count; i++)
                            {
                                SerializedProperty sub = tmp.FindProperty(fieldsName[i].Trim());

                                if (sub != null)
                                {
                                    EditorGUILayout.PropertyField(sub);
                                }
                            }
                        }
                        else
                        {
                            var objectType = tmp.targetObject.GetType();
                            SerializedProperty prop = tmp.GetIterator();

                            prop.Next(true);
                            do
                            {
                                if (!prop.name.StartsWith("m_"))
                                {
                                    FieldInfo fi = objectType.GetField(prop.name);
                                    HideInSubviewAttribute attr = (HideInSubviewAttribute)fi.GetAttribute(typeof(HideInSubviewAttribute));
                                    if (attr == null && GlobalEditor.IsVisible(prop))
                                    {
                                        EditorGUILayout.PropertyField(prop);
                                    }
                                }
                            }
                            while (prop.NextVisible(false));
                        }
                        tmp.ApplyModifiedProperties();
                    }

                    EditorGUILayout.EndVertical();
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}