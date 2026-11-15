using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LostIntheDark
{
    public class AssetListAttribute : PropertyAttribute
    {
        public readonly string SpecialFieldname;
        public readonly System.Type Type;
        public readonly string Label;
        public readonly string NameField;
        public AssetListAttribute(string specialFieldname, System.Type type, string nameField = "", string label = "")
        {
            this.SpecialFieldname = specialFieldname;
            this.Type = type;
            this.Label = label;
            this.NameField = nameField;
        }
        public AssetListAttribute(System.Type type)
        {
            this.SpecialFieldname = "";
            this.Type = type;
            this.Label = "";
            this.NameField = "";
        }
    }

    public class ListManagerAttribute: AssetListAttribute
    {
        public ListManagerAttribute(string specialFieldname, System.Type type, string nameField = "", string label = ""):
            base(specialFieldname, type, nameField, label)
        {
        }
        public ListManagerAttribute(System.Type type): base(type)
        {
            
        }
    }
}

