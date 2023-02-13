#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.MemoryManagement.Entity.Pooling;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.ScriptableObjectsNaming.AddresablePooledScriptableObjectsNaming;

namespace MECS.Core
{
    //* Command used to create all the assets around Addresable Pooled system and manager on MECS framework
    //*
    public class CreateMECSAddresablePooledScriptableCommands : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.scriptableObjectsNaming
            .addresablePooledScriptableObjectsNaming.DEFAULT_ADDRESABLE_POOLED_SCRIPTABLE_OBJECTS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            AddresablePooledScriptableObjectsAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.scriptableObjectsNaming.addresablePooledScriptableObjectsNaming.
            addresablePooledScriptableObjectsAssetsNaming;

            //Check if could create all assets
            //Create initialize pool asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.INITIALIZE_POOL_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<InitializePoolCommand>
            (path, assetsNaming.INITIALIZE_POOL_COMMAND_NAME).Execute())

            //Create return pooled asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.RETURN_POOLED_ENTITY_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<ReturnPooledEntityCommand>
            (path, assetsNaming.RETURN_POOLED_ENTITY_COMMAND_NAME).Execute());

            //Set on final dictionary values
            if (couldntCreateAssets)
                assetsDictionary = tempDictionary;

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, assetsDictionary);

            return assetsDictionary;
        }
    }
}
#endif