using UnityEngine;

namespace LostIntheDark
{
    public class LinkedDropdownAttribute : PropertyAttribute
    {
        public readonly string SrcFieldname;
        public readonly string SrcSubFieldname;
        public readonly string Label;

        public LinkedDropdownAttribute(string srcFieldname, string srcSubFieldname = "", string label = "")
        {
            this.SrcFieldname = srcFieldname;
            this.SrcSubFieldname = srcSubFieldname;
            this.Label = label;
        }
    }
}