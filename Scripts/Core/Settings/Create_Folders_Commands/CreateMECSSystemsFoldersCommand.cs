#if UNITY_EDITOR
using System;
using System.IO;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEditor;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Command used to create all folders related to scriptable objects assets
    //* T = bool
    public class CreateMECSSystemsFoldersCommand : ACreateMECSFolderCommand, ICommandReturn<bool>
    {
        //ACreateMECSFolderCommand, base builder
        public CreateMECSSystemsFoldersCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

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
                DebugTools.DebugLog(new ComplexDebugInformation("CreateMECSSystemsFoldersCommand", "Execute()",
                "created systems folders related to framework"));
            }
            catch (Exception exception)
            {
                DebugTools.DebugError(complexDebugInformation.AddTempCustomText(exception.ToString()));
                areFoldersCreated = false;
            }

            return areFoldersCreated;
        }
    }
}

#endif