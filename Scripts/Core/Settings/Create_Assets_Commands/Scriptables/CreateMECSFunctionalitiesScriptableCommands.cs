#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Functionalities;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.ScriptableObjectsNaming.FunctionalitiesScriptableObjectsNaming;

namespace MECS.Core
{
    //* Command used to create all the assets around Conditional system and manager on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSFunctionalitiesScriptableCommands : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.scriptableObjectsNaming.
            functionalitiesScriptableObjectsNaming.DEFAULT_FUNCTIONALITIES_SCRIPTABLE_OBJECTS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            FunctionalitiesScriptableObjectsAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.scriptableObjectsNaming.functionalitiesScriptableObjectsNaming.
            functionalitiesScriptableObjectsAssetsNaming;

            //Check if could create all assets
            //Create destroy entity asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.DESTROY_ENTITY_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<ScriptableDestroyEntityCommand>
            (path, assetsNaming.DESTROY_ENTITY_COMMAND_NAME).Execute());

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