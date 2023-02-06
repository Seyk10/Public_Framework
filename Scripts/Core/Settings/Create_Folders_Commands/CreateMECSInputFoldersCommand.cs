#if UNITY_EDITOR
using System;
using System.IO;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEditor;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Command used to create folders related to input assets
    public class CreateMECSInputFoldersCommand : ACreateMECSFolderCommand, ICommandReturn<bool>
    {
        //ACreateMECSFolderCommand, base builder
        public CreateMECSInputFoldersCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

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
                DebugTools.DebugLog(new ComplexDebugInformation("CreateMECSFolderTreeCommand", "Execute()",
                   "created system folders related to framework"));
            }
            catch (Exception exception)
            {
                DebugTools.DebugError(complexDebugInformation.AddTempCustomText(exception.ToString()));
                areFoldersCreated = false;
            }

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, areFoldersCreated);

            return areFoldersCreated;
        }
    }
}
#endif