#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Patrons.Commands;
using MECS.Physics.Casting;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.ScriptableObjectsNaming.CastingScriptableObjectsNaming;

namespace MECS.Core
{
    //* Command used to create all the assets around Casting system and manager on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSCastingScriptableCommands : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ACreateMECSScriptableObjectCommand, base builder
        public CreateMECSCastingScriptableCommands(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.scriptableObjectsNaming.
            castingScriptableObjectsNaming.DEFAULT_CASTING_SCRIPTABLE_OBJECTS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            CastingScriptableObjectsAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.scriptableObjectsNaming.castingScriptableObjectsNaming.
            castingScriptableObjectsAssetsNaming;

            //Check if could create all assets
            //Create start casting asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.START_CASTING_STATE_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<StartCastingStateCommand>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.START_CASTING_STATE_COMMAND_NAME),
            path, assetsNaming.START_CASTING_STATE_COMMAND_NAME).Execute(),
            complexDebugInformation)

            //Create stop casting asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.STOP_CASTING_STATE_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<StopCastingStateCommand>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.STOP_CASTING_STATE_COMMAND_NAME),
            path, assetsNaming.STOP_CASTING_STATE_COMMAND_NAME).Execute(),
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