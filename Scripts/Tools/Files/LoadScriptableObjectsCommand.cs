using System;
using System.Collections.Generic;
using System.IO;
using MECS.Collections;
using MECS.Patrons.Commands;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Command used to load assets and return it as dictionary
    //* T = Dictionary<string, ScriptableObject>
    public class LoadScriptableObjectsCommand : ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //Variables
        private readonly string loadingPath = null;
        private readonly ComplexDebugInformation complexDebugInformation = null;

        //Base builder
        public LoadScriptableObjectsCommand(string loadingPath, ComplexDebugInformation complexDebugInformation)
        {
            this.loadingPath = loadingPath;
            this.complexDebugInformation = complexDebugInformation;
        }

        //ICommandReturn, notify ends of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, execute command content loading all assets and return it
        public Dictionary<string, ScriptableObject> Execute()
        {
            //Return value
            Dictionary<string, ScriptableObject> loadedScriptable = null;

            //Basic debug information
            BasicDebugInformation basicDebugInformation = new BasicDebugInformation("LoadScriptableObjectsCommand", "Execute()");

            //Load assets and store
            ScriptableObject[] loadedAssets =
            Resources.LoadAll<ScriptableObject>(loadingPath);

            //Avoid errors checking array
            if (CollectionsTools.arrayTools.IsArrayContentSafe(loadedAssets,
            new ComplexDebugInformation(basicDebugInformation, "couldnt load assets")))
            {
                //Itinerate loaded systems
                foreach (ScriptableObject asset in loadedAssets)
                    CollectionsTools.dictionaryTools
                    .AddValue(loadedScriptable, asset.name, asset,
                    new ComplexDebugInformation(basicDebugInformation, "couldnt add assets to loaded assets dictionary"));

#if UNITY_EDITOR
                Debug.LogError("Not working properly");
#endif
            }

            return loadedScriptable;
        }
    }
}