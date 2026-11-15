using System.Collections.Generic;
using UnityEngine;
namespace LostIntheDark
{
    public class EditorTesterSubLevel2 : MonoBehaviour
    {
        public string Param1;
        [HideInSubview]
        public List<string> Param2 = new List<string>();
        public bool Param3 = false;
    }
}