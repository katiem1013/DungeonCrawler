using UnityEngine;
using UnityEditor;

namespace LostIntheDark
{
    [CustomEditor(typeof(Updatable), true)]
    public class UpdatableEditor : GlobalEditor
    {
   
        public override void OnInspectorGUI()
        {
            if (this.DrawGlobalEditor())
            {
                Updatable updatable = (this.serializedObject.targetObject as Updatable);
                if (updatable != null)
                { 
                    updatable.OnUpdate();
                }
            }
        }
    }
}
