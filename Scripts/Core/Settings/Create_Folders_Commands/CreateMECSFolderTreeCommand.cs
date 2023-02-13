#if UNITY_EDITOR
using System;
using MECS.Patrons.Commands;
using UnityEditor;
using UnityEngine;

namespace MECS.Core
{
    //* Command used to create the full extension of framework folders tree
    //* T = bool
    public class CreateMECSFolderTreeCommand : ICommandReturn<bool>
    {
        //ICommandReturn<bool>, notify ends of command
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn<bool>, execute command content creating all the folder
        public bool Execute()
        {
            //Return value
            bool areAllFoldersCreated = true;

            //Avoid errors
            try
            {
                //Create root folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PARENT_PATH,
                MECSDefaultSettingsNaming.DEFAULT_SETTINGS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                //Create systems folders
                areAllFoldersCreated = new CreateMECSSystemsFoldersCommand().Execute()
                //Create managers folders
                && new CreateMECSManagersFoldersCommand().Execute()
                //Create input folder
                && new CreateMECSInputFoldersCommand().Execute()
                //Create scriptable object folders
                && new CreateMECSScriptableObjectFoldersCommand().Execute();

                //Debug information
                if (areAllFoldersCreated)
                    Debug.Log("created all folders related to framework");
            }
            catch (Exception exception)
            {
                //Debug information
                areAllFoldersCreated = false;
                Debug.LogError(exception.ToString());
            }

            return areAllFoldersCreated;
        }
    }
}
#endif