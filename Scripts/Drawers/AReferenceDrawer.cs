using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MECS.Drawers
{
#if UNITY_EDITOR
    public class AReferenceDrawer<T, T2> : PropertyDrawer
    {
        //* Custom property drawer to set popups on references
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            EditorGUI.BeginProperty(position, label, property);
            //Get local value 
            bool useLocalValues = property.FindPropertyRelative("UseLocalValues").boolValue;

            //Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            //Store rect
            var rect = new Rect(position.position, Vector2.one * 20);

            //Local method, get default texture
            Texture GetTexture()
            {
                //Find textures and filtrate by name
                var textures = Resources.FindObjectsOfTypeAll<Texture>()
                .Where(t => t.name.ToLower().Contains("animationdopesheetkeyframe"))
                .Cast<Texture>().ToList();

                return textures[0];
            }

            //Local method, set property boolean value
            void SetProperty(SerializedProperty property, bool value)
            {
                //Get property
                var propRelative = property.FindPropertyRelative("UseLocalValues");
                //Apply new values
                propRelative.boolValue = value;
                property.serializedObject.ApplyModifiedProperties();
            }

            //Check dropdown selection
            if (EditorGUI.DropdownButton(rect, new GUIContent(GetTexture()), FocusType.Keyboard, new GUIStyle()
            {
                fixedWidth = 50f,
                border = new RectOffset(1, 1, 1, 1)
            }))
            {
                //Add menu items
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Local Value"),
                useLocalValues,
                () => SetProperty(property, true));

                menu.AddItem(new GUIContent("Variable"),
                !useLocalValues,
                () => SetProperty(property, false));

                //Show menu
                menu.ShowAsContext();
            }

            //Set rectangle
            position.position += Vector2.right * 15;

            //Set editor after selection
            var localValue = property.FindPropertyRelative("LocalValue");

            if (useLocalValues)
            {
                //TODO: Update to used array on after popup selection
                //EditorGUI.
            }
        }
    }
#endif
}