using System;
using UnityEngine;
namespace LostIntheDark
{
    public class ConditionalDisplayAttribute : PropertyAttribute
    {
        public readonly string Property;
        public readonly string Condition;
        public readonly object Value;
        public ConditionalDisplayAttribute(string property, string condition = null, object value = null)
        {
            this.Property = property;
            this.Condition = condition;
            this.Value = value;

        }
    }
}