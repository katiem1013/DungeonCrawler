using System;
using UnityEngine;
namespace LostIntheDark
{
    public class ColoredHeaderAttribute : PropertyAttribute
    {
        public readonly string Label;
        public readonly Color Color;
        public ColoredHeaderAttribute(string label, string color = "#00000000")
        {
            if (!ColorUtility.TryParseHtmlString(color, out this.Color))
            {
                this.Color = new Color(0, 0, 0, 0);
            }
            this.Label = label;
        }
    }
}