#if UNITY_EDITOR
using System;
using MECS.Patrons.Commands;
using UnityEditor;
using UnityEngine;

namespace MECS.Core
{
    //* Command used to create all folders related to scriptable objects assets
    //* T = bool
    public class CreateMECSSystemsFoldersCommand : ICommandReturn<bool>
    {
        //ICommandReturn, notify ends of command
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn, execute command content creating all folders related to systems assets
        public bool Execute()
        {
            //Return value
            bool areFoldersCreated = true;

            //Avoid errors
            try
            {
                //Create main systems folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH,
                MECSDefaultSettingsNaming.systemsNaming.DEFAULT_SYSTEMS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                //Debug information                
                Debug.Log("created systems folders related to framework");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                areFoldersCreated = false;
            }

            return areFoldersCreated;
        }
    }
}
#endif