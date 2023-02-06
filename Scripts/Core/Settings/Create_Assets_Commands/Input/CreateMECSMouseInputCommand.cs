#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.GameEvents;
using MECS.Patrons.Commands;
using MECS.Tools;
using MECS.Variables;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.InputScriptableObjectsNaming.
PCInputScriptableObjectsNaming.MouseInputScriptableObjectsNaming;

namespace MECS.Core
{
    //* Command used to create scriptable objects related to mouse input
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSMouseInputCommand : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ACreateMECSScriptableObjectCommand, base builder
        public CreateMECSMouseInputCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify when command ends
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, execute command creating mouse input assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming.
            mouseInputScriptableObjectsNaming.DEFAULT_MOUSE_INPUT_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            MouseInputAssetsScriptableObjectsNaming assetsNaming =
            MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming.
            mouseInputScriptableObjectsNaming.mouseInputAssetsScriptableObjectsNaming;

            //Check if could create all assets
            //Create left click button performed variable asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.LEFT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.LEFT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME),
            path, assetsNaming.LEFT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create left click button canceled variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.LEFT_CLICK_BUTTON_CANCELED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.LEFT_CLICK_BUTTON_CANCELED_VARIABLE_NAME),
            path, assetsNaming.LEFT_CLICK_BUTTON_CANCELED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create left click button performed game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.LEFT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.LEFT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME),
            path, assetsNaming.LEFT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create left click button canceled game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.LEFT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.LEFT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME),
            path, assetsNaming.LEFT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create right click button performed variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.RIGHT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.RIGHT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME),
            path, assetsNaming.RIGHT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create right click button canceled variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.RIGHT_CLICK_BUTTON_CANCELED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.RIGHT_CLICK_BUTTON_CANCELED_VARIABLE_NAME),
            path, assetsNaming.RIGHT_CLICK_BUTTON_CANCELED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create right click button performed game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.RIGHT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.RIGHT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME),
            path, assetsNaming.RIGHT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create right click button canceled game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.RIGHT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.RIGHT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME),
            path, assetsNaming.RIGHT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create mouse delta variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.MOUSE_DELTA_POSITION_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<Vector2Variable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.MOUSE_DELTA_POSITION_VARIABLE_NAME),
            path, assetsNaming.MOUSE_DELTA_POSITION_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create mouse screen position variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.MOUSE_SCREEN_POSITION_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<Vector2Variable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.MOUSE_SCREEN_POSITION_VARIABLE_NAME),
            path, assetsNaming.MOUSE_SCREEN_POSITION_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create mouse world position variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.MOUSE_WORLD_POSITION_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<Vector3Variable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.MOUSE_WORLD_POSITION_VARIABLE_NAME),
            path, assetsNaming.MOUSE_WORLD_POSITION_VARIABLE_NAME).Execute(),
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