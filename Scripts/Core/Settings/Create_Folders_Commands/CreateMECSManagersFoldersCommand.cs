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
    public class CreateMECSManagersFoldersCommand : ACreateMECSFolderCommand, ICommandReturn<bool>
    {
        //ACreateMECSFolderCommand, base builder
        public CreateMECSManagersFoldersCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify ends of command
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn, execute command content creating all folders related to managers assets
        public bool Execute()
        {
            //Return value
            bool areFoldersCreated = true;

            //Avoid errors
            try
            {
                //Create main managers folder
                AssetDatabase.CreateFolder(MECSDefaultSettingsNaming.DEFAULT_SETTINGS_PATH,
                MECSDefaultSettingsNaming.managersNaming.DEFAULT_MANAGERS_FOLDER_NAME);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                //Debug information
                DebugTools.DebugLog(new ComplexDebugInformation("CreateMECSManagersFoldersCommand", "Execute()",
                "created managers folders related to framework"));
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