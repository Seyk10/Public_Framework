using UnityEditor;
using UnityEngine;

namespace MECS.GameEvents
{
#if UNITY_EDITOR
    //* Custom editor for game events, make a button to raise events from editor
    [CustomEditor(typeof(GameEvent))]
    public class GameEvenCustomEditor : Editor
    {
        //Set default and custom button on editor
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            //get target and assign button
            GameEvent gameEvent = (GameEvent)target;

            if (GUILayout.Button("Raise()")) gameEvent.Observer.RaiseSubjects();
        }
    }
#endif
}