using System;
using System.IO;
using MECS.Patrons.Commands;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Command delete folder with files and other directories inside
    public class RecursiveFolderDeleteCommand : ICommandReturn<bool>
    {
        //Variables
        private readonly ComplexDebugInformation complexDebugInformation = null;
        private readonly string path = null,
        folderName = null;

        //Default builder
        public RecursiveFolderDeleteCommand(ComplexDebugInformation complexDebugInformation, string path, string folderName)
        {
            this.complexDebugInformation = complexDebugInformation;
            this.path = path;
            this.folderName = folderName;
        }

        //ICommandReturn, notify when command has end
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn, execute recursive delete of folders
        public bool Execute()
        {
            //Set full path
            string fullPath = folderName + "/" + path;
            bool couldDeleteFolder = false;

            //Local method, used to delete recursive directories
            void RecursiveDelete(DirectoryInfo directory)
            {
                //Check if directory exists
                if (!directory.Exists)
                {
                    couldDeleteFolder = false;
#if UNITY_EDITOR
                    DebugTools.DebugError(complexDebugInformation.AddCustomText("Directory with name " + directory.Name + "doesnt exists"));
#endif
                }
                else
                {
                    //Itinerate sub-directories and delete these
                    foreach (var subDirectory in directory.EnumerateDirectories())
                        RecursiveDelete(subDirectory);

                    //Delete previous directory
                    directory.Delete(true);
                }
            }

            //Delete given folder
            RecursiveDelete(new DirectoryInfo(fullPath));

            return couldDeleteFolder;
        }
    }
}