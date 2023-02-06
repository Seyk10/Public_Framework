#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.GameEvents;
using MECS.Patrons.Commands;
using MECS.Tools;
using MECS.Variables;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.InputScriptableObjectsNaming.PCInputScriptableObjectsNaming.KeyboardInputScriptableObjectsNaming;

namespace MECS.Core
{
    //* Command used to create scriptable objects related to keyboard input
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSKeyboardInputCommand : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ACreateMECSScriptableObjectCommand, base builder
        public CreateMECSKeyboardInputCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify when command ends
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent;

        //ICommandReturn, execute command creating keyboard input assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming.
            keyboardInputScriptableObjectsNaming.DEFAULT_KEYBOARD_INPUT_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            KeyboardInputAssetsScriptableObjectsNaming assetsNaming =
            MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming.
            keyboardInputScriptableObjectsNaming.keyboardInputAssetsScriptableObjectsNaming;

            //Check if could create all assets
            #region A_BUTTON
            //Create A button performed variable asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.A_BUTTON_PERFORMED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.A_BUTTON_PERFORMED_VARIABLE_NAME),
            path, assetsNaming.A_BUTTON_PERFORMED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create A click button canceled variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.A_BUTTON_CANCELED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.A_BUTTON_CANCELED_VARIABLE_NAME),
            path, assetsNaming.A_BUTTON_CANCELED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create A button performed game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.A_BUTTON_PERFORMED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.A_BUTTON_PERFORMED_GAME_EVENT_NAME),
            path, assetsNaming.A_BUTTON_PERFORMED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create A button canceled game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.A_BUTTON_CANCELED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.A_BUTTON_CANCELED_GAME_EVENT_NAME),
            path, assetsNaming.A_BUTTON_CANCELED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)
            #endregion
            #region D_BUTTON
            //Create D button performed variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.D_BUTTON_PERFORMED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.D_BUTTON_PERFORMED_VARIABLE_NAME),
            path, assetsNaming.D_BUTTON_PERFORMED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create D button canceled variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.D_BUTTON_CANCELED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.D_BUTTON_CANCELED_VARIABLE_NAME),
            path, assetsNaming.D_BUTTON_CANCELED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create D button performed game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.D_BUTTON_PERFORMED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.D_BUTTON_PERFORMED_GAME_EVENT_NAME),
            path, assetsNaming.D_BUTTON_PERFORMED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create D button canceled game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.D_BUTTON_CANCELED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.D_BUTTON_CANCELED_GAME_EVENT_NAME),
            path, assetsNaming.D_BUTTON_CANCELED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)
            #endregion
            #region S_BUTTON
            //Create S button performed variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.S_BUTTON_PERFORMED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.S_BUTTON_PERFORMED_VARIABLE_NAME),
            path, assetsNaming.S_BUTTON_PERFORMED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create S button canceled variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.S_BUTTON_CANCELED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.S_BUTTON_CANCELED_VARIABLE_NAME),
            path, assetsNaming.S_BUTTON_CANCELED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create S button performed game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.S_BUTTON_PERFORMED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.S_BUTTON_PERFORMED_GAME_EVENT_NAME),
            path, assetsNaming.S_BUTTON_PERFORMED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create S button canceled game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.S_BUTTON_CANCELED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.S_BUTTON_CANCELED_GAME_EVENT_NAME),
            path, assetsNaming.S_BUTTON_CANCELED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)
            #endregion
            #region W_BUTTON
            //Create W button performed variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.W_BUTTON_PERFORMED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.W_BUTTON_PERFORMED_VARIABLE_NAME),
            path, assetsNaming.W_BUTTON_PERFORMED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create W button canceled variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.W_BUTTON_CANCELED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.W_BUTTON_CANCELED_VARIABLE_NAME),
            path, assetsNaming.W_BUTTON_CANCELED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create W button performed game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.W_BUTTON_PERFORMED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.W_BUTTON_PERFORMED_GAME_EVENT_NAME),
            path, assetsNaming.W_BUTTON_PERFORMED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create W button canceled game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.W_BUTTON_CANCELED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.W_BUTTON_CANCELED_GAME_EVENT_NAME),
            path, assetsNaming.W_BUTTON_CANCELED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)
            #endregion
            #region SPACE_BUTTON
            //Create space button performed variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SPACE_BUTTON_PERFORMED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.SPACE_BUTTON_PERFORMED_VARIABLE_NAME),
            path, assetsNaming.SPACE_BUTTON_PERFORMED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create space button canceled variable asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SPACE_BUTTON_CANCELED_VARIABLE_NAME,
            new CreatePersistentScriptableObjectCommand<BoolVariable>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.SPACE_BUTTON_CANCELED_VARIABLE_NAME),
            path, assetsNaming.SPACE_BUTTON_CANCELED_VARIABLE_NAME).Execute(),
            complexDebugInformation)

            //Create space button performed game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SPACE_BUTTON_PERFORMED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.SPACE_BUTTON_PERFORMED_GAME_EVENT_NAME),
            path, assetsNaming.SPACE_BUTTON_PERFORMED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation)

            //Create space button canceled game event asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SPACE_BUTTON_CANCELED_GAME_EVENT_NAME,
            new CreatePersistentScriptableObjectCommand<GameEvent>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.SPACE_BUTTON_CANCELED_GAME_EVENT_NAME),
            path, assetsNaming.SPACE_BUTTON_CANCELED_GAME_EVENT_NAME).Execute(),
            complexDebugInformation);
            #endregion

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