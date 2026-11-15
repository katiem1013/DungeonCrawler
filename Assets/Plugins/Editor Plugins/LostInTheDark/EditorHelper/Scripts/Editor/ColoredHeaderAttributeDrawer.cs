using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace LostIntheDark
{
    [CustomPropertyDrawer(typeof(ColoredHeaderAttribute))]
    public class ColoredHeaderAttributeDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            string text = ((ColoredHeaderAttribute)this.attribute).Label;
            Color bgColor = ((ColoredHeaderAttribute)this.attribute).Color;

            EditorGUI.DrawRect(new Rect(position.x, position.y + 20, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight + 10), bgColor);
            EditorGUI.indentLevel++;
            EditorGUI.LabelField(new Rect(position.x, position.y + 20, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight + 10), text);
            EditorGUI.indentLevel--;
        }

        public override float GetHeight()
        {
            return base.GetHeight() + 35;
        }
    }
}