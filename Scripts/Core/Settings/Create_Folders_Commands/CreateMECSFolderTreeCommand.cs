#if UNITY_EDITOR
using System;
using System.IO;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEditor;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Command used to create the full extension of framework folders tree
    //* T = bool
    public class CreateMECSFolderTreeCommand : ACreateMECSFolderCommand, ICommandReturn<bool>
    {
        //ACreateMECSFolderCommand, base builder
        public CreateMECSFolderTreeCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

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
                areAllFoldersCreated = new CreateMECSSystemsFoldersCommand(
                    complexDebugInformation.AddTempCustomText("couldnt create systems folders")).Execute()
                //Create managers folders
                && new CreateMECSManagersFoldersCommand(
                    complexDebugInformation.AddTempCustomText("couldnt create managers folders")).Execute()
                //Create input folder
                && new CreateMECSInputFoldersCommand(
                    complexDebugInformation.AddTempCustomText("couldnt create input folders")).Execute()
                //Create scriptable object folders
                && new CreateMECSScriptableObjectFoldersCommand(
                    complexDebugInformation.AddTempCustomText("couldnt create scriptable object folders")).Execute();

                //Debug information
                if (areAllFoldersCreated)
                    DebugTools.DebugLog(new ComplexDebugInformation("CreateMECSFolderTreeCommand", "Execute()",
                    "created all folders related to framework"));
            }
            catch (Exception exception)
            {
                //Debug information
                areAllFoldersCreated = false;
                DebugTools.DebugError(complexDebugInformation.AddTempCustomText(exception.ToString()));
            }

            return areAllFoldersCreated;
        }
    }
}
#endif