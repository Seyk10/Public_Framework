using System;
using System.IO;
using MECS.Patrons.Commands;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Command used to create folder with file system, return boolean to indicate folder creation
    //* T = bool
    public class CreateFolderCommand : ICommandReturn<bool>
    {
        //Variables
        private readonly ComplexDebugInformation complexDebugInformation = null;
        private readonly string path = null,
        folderName = null;
        private readonly bool deletePreviousDirectory = false;

        //Default builder
        public CreateFolderCommand(ComplexDebugInformation complexDebugInformation,
        bool deletePreviousDirectory, string path, string folderName)
        {
            this.complexDebugInformation = complexDebugInformation;
            this.path = path;
            this.folderName = folderName;
            this.deletePreviousDirectory = deletePreviousDirectory;
        }

        //ICommandReturn, notify ends of event
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn, execute command content creating a folder on given path
        public bool Execute()
        {
            //Final path to use
            string finalPath = path + "/" + folderName;

            //Return value
            bool canCreateFolder = true,
            shouldDeletePreviousDirectory = deletePreviousDirectory && File.Exists(finalPath);

            //Check if should delete previous directory
            if (shouldDeletePreviousDirectory)
                //Delete folder and content
                canCreateFolder = new RecursiveFolderDeleteCommand(complexDebugInformation, path, folderName).Execute();

            //Check if can create folder
            if (canCreateFolder)
                //Create folder with given path
                File.Create(finalPath);
#if UNITY_EDITOR
            else
                DebugTools.DebugError(complexDebugInformation.AddTempCustomText("couldnt create folder with given path " + finalPath));
#endif

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, canCreateFolder);

            return canCreateFolder;
        }
    }
}