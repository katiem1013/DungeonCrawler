using System;
using UnityEngine;
namespace LostIntheDark
{
    public class HideInSubviewAttribute : PropertyAttribute
    {
        public readonly bool Hide;
        public HideInSubviewAttribute()
        {
            this.Hide = true;
        }
    }
}