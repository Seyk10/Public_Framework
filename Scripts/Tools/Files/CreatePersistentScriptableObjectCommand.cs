#if UNITY_EDITOR
using System;
using System.IO;
using MECS.Patrons.Commands;
using UnityEditor;
using UnityEngine;

namespace MECS.Tools
{
    //* Command used to create persistent scriptable objects
    //* T = ScriptableObject type
    public class CreatePersistentScriptableObjectCommand<T> : ICommandReturn<T> where T : ScriptableObject
    {
        //Variables
        private readonly string path = null,
        assetName = null;

        //Default builder
        public CreatePersistentScriptableObjectCommand(string path, string assetName)
        {
            this.path = path;
            this.assetName = assetName;
        }

        //ICommandReturn, notify when its finished
        public event EventHandler<T> CommandFinishedEvent = null;

        //ICommandReturn, create asset based on given parameters and return result
        public T Execute()
        {
            string finalPath = path + "/" + assetName + ".asset";

            //Return value
            T asset = null;

            //Check if file exists
            if (!File.Exists(finalPath))
            {
                //Create asset and save it
                asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, finalPath);

                //Safe assets and refresh data base
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            CommandFinishedEvent?.Invoke(this, asset);

            return asset;
        }
    }
}
#endif