#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.GameEvents;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.ScriptableObjectsNaming.GameEventListenerScriptableObjectsNaming;

namespace MECS.Core
{
    //* Command used to create all the assets around Conditional system and manager on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSGameEventListenerScriptableCommands : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //CreateMECSGameEventListenerScriptableCommands, base builder
        public CreateMECSGameEventListenerScriptableCommands(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.scriptableObjectsNaming.gameEventListenerScriptableObjectsNaming.
            DEFAULT_GAME_EVENT_LISTENER_SCRIPTABLE_OBJECTS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            GameEventListenerScriptableObjectsAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.scriptableObjectsNaming.gameEventListenerScriptableObjectsNaming.
            gameEventListenerScriptableObjectsAssetsNaming;

            //Check if could create all assets
            //Create check game event registration asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.GAME_EVENT_REGISTRATION_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<ScriptableRegistrationCommand>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.GAME_EVENT_REGISTRATION_COMMAND_NAME),
            path, assetsNaming.GAME_EVENT_REGISTRATION_COMMAND_NAME).Execute(),
            complexDebugInformation)

            //Create check game event unregister asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.GAME_EVENT_UNREGISTER_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<ScriptableUnregisterCommand>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.GAME_EVENT_UNREGISTER_COMMAND_NAME),
            path, assetsNaming.GAME_EVENT_UNREGISTER_COMMAND_NAME).Execute(),
            complexDebugInformation);

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