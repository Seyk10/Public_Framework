#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Conditionals;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.ScriptableObjectsNaming.ConditionalsScriptableObjectsNaming;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Command used to create all the assets around Conditional system and manager on MECS framework
    public class CreateMECSConditionalsScriptableCommands : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ICommandReturn, base builder
        public CreateMECSConditionalsScriptableCommands(ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.scriptableObjectsNaming.
            conditionalsScriptableObjectsNaming.DEFAULT_CONDITIONALS_SCRIPTABLE_OBJECTS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            ConditionalsScriptableObjectsAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.scriptableObjectsNaming.conditionalsScriptableObjectsNaming.
            conditionalsScriptableObjectsAssetsNaming;

            //Check if could create all assets
            //Create check conditional asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.CHECK_CONDITIONALS_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<CheckConditionalsCommand>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.CHECK_CONDITIONALS_COMMAND_NAME),
            path, assetsNaming.CHECK_CONDITIONALS_COMMAND_NAME).Execute(),
            complexDebugInformation)

            //Create set false boolean asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SET_FALSE_BOOLEAN_CONDITIONAL_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<ScriptableSetBooleanConditionalCommand>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.SET_FALSE_BOOLEAN_CONDITIONAL_COMMAND_NAME),
            path, assetsNaming.SET_FALSE_BOOLEAN_CONDITIONAL_COMMAND_NAME).Execute(),
            complexDebugInformation)

            //Create set true boolean asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SET_TRUE_BOOLEAN_CONDITIONAL_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<ScriptableSetBooleanConditionalCommand>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.SET_TRUE_BOOLEAN_CONDITIONAL_COMMAND_NAME),
            path, assetsNaming.SET_TRUE_BOOLEAN_CONDITIONAL_COMMAND_NAME).Execute(),
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