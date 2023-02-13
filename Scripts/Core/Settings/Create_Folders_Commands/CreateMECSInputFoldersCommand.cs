#if UNITY_EDITOR
using System;
using MECS.Patrons.Commands;
using UnityEditor;
using UnityEngine;

namespace MECS.Core
{
    //* Command used to create folders related to input assets
    public class CreateMECSInputFoldersCommand : ICommandReturn<bool>
    {
        //ICommandReturn, notify end of command
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn, execute command content creating all folders related to input assets
        public bool Execute()
        {
            //Return value
            bool areFoldersCreated = true;

            //Avoid errors
            try
            {
                //Main input folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH,
                MECSDefaultSettingsNaming.inputScriptableObjectsNaming.DEFAULT_INPUT_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //PC folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming.DEFAULT_INPUT_PATH,
                MECSDefaultSettingsNaming.inputScriptableObjectsNaming
                .pcInputScriptableObjectsNaming.DEFAULT_PC_INPUT_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Mouse folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming
                .pcInputScriptableObjectsNaming.DEFAULT_PC_INPUT_PATH,
                MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming
                .mouseInputScriptableObjectsNaming.DEFAULT_MOUSE_INPUT_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Keyboard folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.inputScriptableObjectsNaming
                .pcInputScriptableObjectsNaming.DEFAULT_PC_INPUT_PATH,
                MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming
                .keyboardInputScriptableObjectsNaming.DEFAULT_KEYBOARD_INPUT_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Debug information
                Debug.Log("created system folders related to framework");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                areFoldersCreated = false;
            }

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, areFoldersCreated);

            return areFoldersCreated;
        }
    }
}
#endif