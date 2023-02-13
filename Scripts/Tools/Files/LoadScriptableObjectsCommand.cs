using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Tools
{
    //* Command used to load assets and return it as dictionary
    //* T = Dictionary<string, ScriptableObject>
    public class LoadScriptableObjectsCommand : ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //Variables
        private readonly string loadingPath = null;

        //Base builder
        public LoadScriptableObjectsCommand(string loadingPath) =>
            this.loadingPath = loadingPath;

        //ICommandReturn, notify ends of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, execute command content loading all assets and return it
        public Dictionary<string, ScriptableObject> Execute()
        {
            //Return value
            Dictionary<string, ScriptableObject> loadedScriptable = null;

            //Load assets and store
            ScriptableObject[] loadedAssets =
            Resources.LoadAll<ScriptableObject>(loadingPath);

            //Avoid errors checking array
            if (CollectionsTools.arrayTools.IsArrayContentSafe(loadedAssets, " loaded assets aren't safe"))
            {
                //Itinerate loaded systems
                foreach (ScriptableObject asset in loadedAssets)
                    CollectionsTools.dictionaryTools
                    .AddValue(loadedScriptable, asset.name, asset, " couldnt add assets to loaded assets dictionary");
            }

            return loadedScriptable;
        }
    }
}