using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LostIntheDark
{
    public class SubFieldAttribute : PropertyAttribute
    {
        public readonly List<string> SubFieldsName = new List<string>();
        public bool IsPacked = true;

        public SubFieldAttribute(string subfields = "", bool isPacked = true)
        {
            this.SubFieldsName = string.IsNullOrEmpty(subfields) ? new List<string>() : subfields.Split(";",System.StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            this.IsPacked = isPacked;
        }
    }
}