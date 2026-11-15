using System;
using System.Collections.Generic;
using UnityEngine;


namespace LostIntheDark
{
    [Serializable]
    public class SpriteTester
    {
        public string Name;
        public string Path;
        public string Description;
        public bool IsUI = false;
    }

    public enum TestEnum
    {
        Value1,
        Value2,
        Value3
    }

    public class EditorTester : Updatable
    {
        public string test;
        [ColoredHeader("SubFields", "#00FF0055")]
        [SubField("Param1;Param2")]
        public EditorTesterSubLevel EditorTesterSubLevel;

        [SubField]
        public EditorTesterSubLevel2 EditorTesterSubLevel2;

        [ColoredHeader("SpecialList", "#FF000055")]
        [AssetList("Path", typeof(Sprite), "Name", "Sprite")]
        public List<SpriteTester> Testers = new List<SpriteTester>();

        [ColoredHeader("Dropdown", "#0000FF55")]
        [LinkedDropdown("Testers", "Name", "Selected Sprite")]
        public string DropDown = "";

        [ColoredHeader("Conditional Display", "#00FFFF55")]
        public TestEnum TestEnum = TestEnum.Value1;
        [ConditionalDisplay("TestEnum", "=", TestEnum.Value1)]
        public string ShowWhenValue1;
        [ConditionalDisplay("TestEnum", "!=", TestEnum.Value1)]
        public string ShowWhenNotValue1;


        public override void OnUpdate()
        {
            Debug.Log(DateTime.Now.ToString("G") + " UPDATED");
        }
    }
}