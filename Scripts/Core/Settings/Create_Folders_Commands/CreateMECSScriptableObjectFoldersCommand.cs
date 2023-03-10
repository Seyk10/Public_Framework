#if UNITY_EDITOR
using System;
using MECS.Patrons.Commands;
using UnityEditor;
using UnityEngine;

namespace MECS.Core
{
    //* Command used to create all folders related to scriptable objects assets
    //* T = bool
    public class CreateMECSScriptableObjectFoldersCommand : ICommandReturn<bool>
    {
        //ICommandReturn, notify ends of command
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn, execute command content creating all folders related to scriptable objects assets
        public bool Execute()
        {
            //Return value
            bool areFoldersCreated = true;

            //Avoid errors
            try
            {
                //Create main scriptable object folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH,
                MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Create sub folders
                //Functionalities
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH,
                MECSDefaultSettingsNaming.scriptableObjectsNaming.functionalitiesScriptableObjectsNaming
                .DEFAULT_FUNCTIONALITIES_SCRIPTABLE_OBJECTS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Game events listener
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH,
                MECSDefaultSettingsNaming.scriptableObjectsNaming.gameEventListenerScriptableObjectsNaming
                .DEFAULT_GAME_EVENT_LISTENER_SCRIPTABLE_OBJECTS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Timer
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH,
                MECSDefaultSettingsNaming.scriptableObjectsNaming.timerScriptableObjectsNaming
                .DEFAULT_TIMER_SCRIPTABLE_OBJECTS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Conditionals
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH,
                MECSDefaultSettingsNaming.scriptableObjectsNaming.conditionalsScriptableObjectsNaming
                .DEFAULT_CONDITIONALS_SCRIPTABLE_OBJECTS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Casting
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH,
                MECSDefaultSettingsNaming.scriptableObjectsNaming.castingScriptableObjectsNaming
                .DEFAULT_CASTING_SCRIPTABLE_OBJECTS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Addresable pooled
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.scriptableObjectsNaming.DEFAULT_SCRIPTABLE_OBJECTS_PATH,
                MECSDefaultSettingsNaming.scriptableObjectsNaming.addresablePooledScriptableObjectsNaming
                .DEFAULT_ADDRESABLE_POOLED_SCRIPTABLE_OBJECTS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Debug information
                Debug.Log("created scriptable object folders related to framework");
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