#if UNITY_EDITOR
using System;
using MECS.Collections;
using MECS.GameEvents;
using MECS.Input;
using MECS.Patrons.Commands;
using MECS.Tools;
using MECS.Variables;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.InputScriptableObjectsNaming.PCInputScriptableObjectsNaming.KeyboardInputScriptableObjectsNaming;
using static MECS.Core.MECSDefaultSettingsNaming.InputScriptableObjectsNaming.PCInputScriptableObjectsNaming.MouseInputScriptableObjectsNaming;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Set all the configuration of input assets on input system asset
    //* T = bool
    public class ConfigureMECSInputCommand : ICommandReturn<bool>
    {
        //Variables
        private readonly ComplexDebugInformation complexDebugInformation = null;

        //Base builder
        public ConfigureMECSInputCommand(ComplexDebugInformation complexDebugInformation) =>
            this.complexDebugInformation = complexDebugInformation;

        //ICommandReturn, notify when command has finished
        public event EventHandler<bool> CommandFinishedEvent = null;

        //ICommandReturn, execute command content and set input assets on correct input system values
        public bool Execute()
        {
            //Return value
            bool isInputSystemConfigured = true;

            //Get input asset values
            if (CollectionsTools.dictionaryTools.
            GetValue(MECSSettings.SystemsDictionary, MECSDefaultSettingsNaming.systemsNaming.systemAssetsNaming.INPUT_SYSTEM_NAME,
            out ScriptableObject scriptableInputSystem, " couldnt get input system"))
            {
                //Store input system
                InputSystem inputSystem = (InputSystem)scriptableInputSystem;

                //Local method, set mouse input assets
                void SetMouseInputAssets()
                {
                    //Store mouse naming
                    MouseInputAssetsScriptableObjectsNaming mouseInputAssetsScriptableObjectsNaming =
                    MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming
                    .mouseInputScriptableObjectsNaming.mouseInputAssetsScriptableObjectsNaming;

                    #region LEFT_CLICK
                    //Get left click performed variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .LEFT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME,
                        out ScriptableObject scriptableLeftClickButtonPerformedVariable,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.LEFT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME))
                        //Set left click performed variable asset
                        inputSystem.PcMapReferences.MouseInput.LeftClickInput.ButtonPerformed.VariableValue =
                        (BoolVariable)scriptableLeftClickButtonPerformedVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get left click canceled variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .LEFT_CLICK_BUTTON_CANCELED_VARIABLE_NAME,
                        out ScriptableObject scriptableLeftClickButtonCanceledVariable,
                        "couldnt get " + mouseInputAssetsScriptableObjectsNaming.LEFT_CLICK_BUTTON_CANCELED_VARIABLE_NAME))
                        //Set left click canceled variable asset
                        inputSystem.PcMapReferences.MouseInput.LeftClickInput.ButtonCanceled.VariableValue =
                        (BoolVariable)scriptableLeftClickButtonCanceledVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get left click performed game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .LEFT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableLeftClickButtonPerformedGameEvent,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.LEFT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME))
                        //Set left click performed game event asset
                        inputSystem.PcMapReferences.MouseInput.LeftClickInput.ButtonPerformed.GameEvent =
                        (GameEvent)scriptableLeftClickButtonPerformedGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get left click canceled game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .LEFT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableLeftClickButtonCanceledGameEvent,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.LEFT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME))
                        //Set left click canceled game event asset
                        inputSystem.PcMapReferences.MouseInput.LeftClickInput.ButtonCanceled.GameEvent =
                        (GameEvent)scriptableLeftClickButtonCanceledGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                    #region RIGHT_CLICK
                    //Get right click performed variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .RIGHT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME,
                        out ScriptableObject scriptableRightClickButtonPerformedVariable,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.RIGHT_CLICK_BUTTON_PERFORMED_VARIABLE_NAME))
                        //Set right click performed variable asset
                        inputSystem.PcMapReferences.MouseInput.RightClickInput.ButtonPerformed.VariableValue =
                        (BoolVariable)scriptableRightClickButtonPerformedVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get right click canceled variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .RIGHT_CLICK_BUTTON_CANCELED_VARIABLE_NAME,
                        out ScriptableObject scriptableRightClickButtonCanceledVariable,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.RIGHT_CLICK_BUTTON_CANCELED_VARIABLE_NAME))
                        //Set right click canceled variable asset
                        inputSystem.PcMapReferences.MouseInput.RightClickInput.ButtonCanceled.VariableValue =
                        (BoolVariable)scriptableRightClickButtonCanceledVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get right click performed game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .RIGHT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableRightClickButtonPerformedGameEvent,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.RIGHT_CLICK_BUTTON_PERFORMED_GAME_EVENT_NAME))
                        //Set right click performed game event asset
                        inputSystem.PcMapReferences.MouseInput.RightClickInput.ButtonPerformed.GameEvent =
                        (GameEvent)scriptableRightClickButtonPerformedGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get right click canceled game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .RIGHT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableRightClickButtonCanceledGameEvent,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.RIGHT_CLICK_BUTTON_CANCELED_GAME_EVENT_NAME))
                        //Set right click canceled game event asset
                        inputSystem.PcMapReferences.MouseInput.RightClickInput.ButtonCanceled.GameEvent =
                        (GameEvent)scriptableRightClickButtonCanceledGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                    #region MOUSE_POSITIONS
                    //Get mouse delta position variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .MOUSE_DELTA_POSITION_VARIABLE_NAME,
                        out ScriptableObject scriptableMouseDeltaPositionVariable,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.MOUSE_DELTA_POSITION_VARIABLE_NAME))
                        //Set mouse delta position variable asset
                        inputSystem.PcMapReferences.MouseInput.MouseDeltaPosition =
                        (Vector2Variable)scriptableMouseDeltaPositionVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get mouse screen position variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .MOUSE_SCREEN_POSITION_VARIABLE_NAME,
                        out ScriptableObject scriptableMouseScreenPositionVariable,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.MOUSE_SCREEN_POSITION_VARIABLE_NAME))
                        //Set mouse screen position variable asset
                        inputSystem.PcMapReferences.MouseInput.MouseScreenPosition =
                        (Vector2Variable)scriptableMouseScreenPositionVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get mouse world position variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, mouseInputAssetsScriptableObjectsNaming
                        .MOUSE_WORLD_POSITION_VARIABLE_NAME,
                        out ScriptableObject scriptableMouseWorldPositionVariable,
                        " couldnt get " + mouseInputAssetsScriptableObjectsNaming.MOUSE_WORLD_POSITION_VARIABLE_NAME))
                        //Set mouse world position variable asset
                        inputSystem.PcMapReferences.MouseInput.MouseWorldPosition =
                        (Vector3Variable)scriptableMouseWorldPositionVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                }

                //Local method, set keyboard input assets
                void SetKeyboardInputAssets()
                {
                    //Store keyboard naming
                    KeyboardInputAssetsScriptableObjectsNaming keyboardInputAssetsScriptableObjectsNaming =
                    MECSDefaultSettingsNaming.inputScriptableObjectsNaming.pcInputScriptableObjectsNaming
                    .keyboardInputScriptableObjectsNaming.keyboardInputAssetsScriptableObjectsNaming;

                    #region A_BUTTON
                    //Get A button performed variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .A_BUTTON_PERFORMED_VARIABLE_NAME,
                        out ScriptableObject scriptableAButtonPerformedVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.A_BUTTON_PERFORMED_VARIABLE_NAME))
                        //Set A button performed variable asset
                        inputSystem.PcMapReferences.KeyboardInput.AButtonInput.ButtonPerformed.VariableValue =
                        (BoolVariable)scriptableAButtonPerformedVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get A button canceled variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .A_BUTTON_CANCELED_VARIABLE_NAME,
                        out ScriptableObject scriptableAButtonCanceledVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.A_BUTTON_CANCELED_VARIABLE_NAME))
                        //Set A button canceled variable asset
                        inputSystem.PcMapReferences.KeyboardInput.AButtonInput.ButtonCanceled.VariableValue =
                        (BoolVariable)scriptableAButtonCanceledVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get A button performed game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .A_BUTTON_PERFORMED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableAButtonPerformedGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.A_BUTTON_PERFORMED_GAME_EVENT_NAME))
                        //Set A button performed game event asset
                        inputSystem.PcMapReferences.KeyboardInput.AButtonInput.ButtonPerformed.GameEvent =
                        (GameEvent)scriptableAButtonPerformedGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get A button canceled game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .A_BUTTON_CANCELED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableAButtonCanceledGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.A_BUTTON_CANCELED_GAME_EVENT_NAME))
                        //Set A button canceled game event asset
                        inputSystem.PcMapReferences.KeyboardInput.AButtonInput.ButtonCanceled.GameEvent =
                        (GameEvent)scriptableAButtonCanceledGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                    #region S_BUTTON
                    //Get S button performed variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .S_BUTTON_PERFORMED_VARIABLE_NAME,
                        out ScriptableObject scriptableSButtonPerformedVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.S_BUTTON_PERFORMED_VARIABLE_NAME))
                        //Set S button performed variable asset
                        inputSystem.PcMapReferences.KeyboardInput.SButtonInput.ButtonPerformed.VariableValue =
                        (BoolVariable)scriptableSButtonPerformedVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get S button canceled variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .S_BUTTON_CANCELED_VARIABLE_NAME,
                        out ScriptableObject scriptableSButtonCanceledVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.S_BUTTON_CANCELED_VARIABLE_NAME))
                        //Set S button canceled variable asset
                        inputSystem.PcMapReferences.KeyboardInput.SButtonInput.ButtonCanceled.VariableValue =
                        (BoolVariable)scriptableSButtonCanceledVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get S button performed game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .S_BUTTON_PERFORMED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableSButtonPerformedGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.S_BUTTON_PERFORMED_GAME_EVENT_NAME))
                        //Set S button performed game event asset
                        inputSystem.PcMapReferences.KeyboardInput.SButtonInput.ButtonPerformed.GameEvent =
                        (GameEvent)scriptableSButtonPerformedGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get S button canceled game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .S_BUTTON_CANCELED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableSButtonCanceledGameEvent,
                         "couldnt get " + keyboardInputAssetsScriptableObjectsNaming.S_BUTTON_CANCELED_GAME_EVENT_NAME))
                        //Set S button canceled game event asset
                        inputSystem.PcMapReferences.KeyboardInput.SButtonInput.ButtonCanceled.GameEvent =
                        (GameEvent)scriptableSButtonCanceledGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                    #region D_BUTTON
                    //Get D button performed variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .D_BUTTON_PERFORMED_VARIABLE_NAME,
                        out ScriptableObject scriptableDButtonPerformedVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.D_BUTTON_PERFORMED_VARIABLE_NAME))
                        //Set D button performed variable asset
                        inputSystem.PcMapReferences.KeyboardInput.DButtonInput.ButtonPerformed.VariableValue =
                        (BoolVariable)scriptableDButtonPerformedVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get D button canceled variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .D_BUTTON_CANCELED_VARIABLE_NAME,
                        out ScriptableObject scriptableDButtonCanceledVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.D_BUTTON_CANCELED_VARIABLE_NAME))
                        //Set D button canceled variable asset
                        inputSystem.PcMapReferences.KeyboardInput.DButtonInput.ButtonCanceled.VariableValue =
                        (BoolVariable)scriptableDButtonCanceledVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get D button performed game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .D_BUTTON_PERFORMED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableDButtonPerformedGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.D_BUTTON_PERFORMED_GAME_EVENT_NAME))
                        //Set D button performed game event asset
                        inputSystem.PcMapReferences.KeyboardInput.DButtonInput.ButtonPerformed.GameEvent =
                        (GameEvent)scriptableDButtonPerformedGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get D button canceled game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .D_BUTTON_CANCELED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableDButtonCanceledGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.D_BUTTON_CANCELED_GAME_EVENT_NAME))
                        //Set D button canceled game event asset
                        inputSystem.PcMapReferences.KeyboardInput.DButtonInput.ButtonCanceled.GameEvent =
                        (GameEvent)scriptableDButtonCanceledGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                    #region W_BUTTON
                    //Get W button performed variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .W_BUTTON_PERFORMED_VARIABLE_NAME,
                        out ScriptableObject scriptableWButtonPerformedVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.W_BUTTON_PERFORMED_VARIABLE_NAME))
                        //Set W button performed variable asset
                        inputSystem.PcMapReferences.KeyboardInput.WButtonInput.ButtonPerformed.VariableValue =
                        (BoolVariable)scriptableWButtonPerformedVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get W button canceled variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .W_BUTTON_CANCELED_VARIABLE_NAME,
                        out ScriptableObject scriptableWButtonCanceledVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.W_BUTTON_CANCELED_VARIABLE_NAME))
                        //Set W button canceled variable asset
                        inputSystem.PcMapReferences.KeyboardInput.WButtonInput.ButtonCanceled.VariableValue =
                        (BoolVariable)scriptableWButtonCanceledVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get W button performed game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .W_BUTTON_PERFORMED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableWButtonPerformedGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.W_BUTTON_PERFORMED_GAME_EVENT_NAME))
                        //Set W button performed game event asset
                        inputSystem.PcMapReferences.KeyboardInput.WButtonInput.ButtonPerformed.GameEvent =
                        (GameEvent)scriptableWButtonPerformedGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get W button canceled game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .W_BUTTON_CANCELED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableWButtonCanceledGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.W_BUTTON_CANCELED_GAME_EVENT_NAME))
                        //Set W button canceled game event asset
                        inputSystem.PcMapReferences.KeyboardInput.WButtonInput.ButtonCanceled.GameEvent =
                        (GameEvent)scriptableWButtonCanceledGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                    #region SPACE_BUTTON
                    //Get space button performed variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .SPACE_BUTTON_PERFORMED_VARIABLE_NAME,
                        out ScriptableObject scriptableSpaceButtonPerformedVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.SPACE_BUTTON_PERFORMED_VARIABLE_NAME))
                        //Set space button performed variable asset
                        inputSystem.PcMapReferences.KeyboardInput.SpaceButtonInput.ButtonPerformed.VariableValue =
                        (BoolVariable)scriptableSpaceButtonPerformedVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get space button canceled variable asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .SPACE_BUTTON_CANCELED_VARIABLE_NAME,
                        out ScriptableObject scriptableSpaceButtonCanceledVariable,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.SPACE_BUTTON_CANCELED_VARIABLE_NAME))
                        //Set space button canceled variable asset
                        inputSystem.PcMapReferences.KeyboardInput.SpaceButtonInput.ButtonCanceled.VariableValue =
                        (BoolVariable)scriptableSpaceButtonCanceledVariable;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get space button performed game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .SPACE_BUTTON_PERFORMED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableSpaceButtonPerformedGameEvent,
                         "couldnt get " + keyboardInputAssetsScriptableObjectsNaming.SPACE_BUTTON_PERFORMED_GAME_EVENT_NAME))
                        //Set space button performed game event asset
                        inputSystem.PcMapReferences.KeyboardInput.SpaceButtonInput.ButtonPerformed.GameEvent =
                        (GameEvent)scriptableSpaceButtonPerformedGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;

                    //Get space button canceled game event asset
                    if (CollectionsTools.dictionaryTools.
                        GetValue(MECSSettings.InputScriptableDictionary, keyboardInputAssetsScriptableObjectsNaming
                        .SPACE_BUTTON_CANCELED_GAME_EVENT_NAME,
                        out ScriptableObject scriptableSpaceButtonCanceledGameEvent,
                        " couldnt get " + keyboardInputAssetsScriptableObjectsNaming.SPACE_BUTTON_CANCELED_GAME_EVENT_NAME))
                        //Set space button canceled game event asset
                        inputSystem.PcMapReferences.KeyboardInput.SpaceButtonInput.ButtonCanceled.GameEvent =
                        (GameEvent)scriptableSpaceButtonCanceledGameEvent;
                    else
                        //Set if input system isn't configured
                        isInputSystemConfigured = false;
                    #endregion
                }

                //Set inputs on values
                SetMouseInputAssets();
                SetKeyboardInputAssets();

                //Debug if input is configured
                if (isInputSystemConfigured)
                    new NotificationCommand<DebugArgs>(this, new DebugArgs(" input system configured", LogType.Error,
                    new System.Diagnostics.StackTrace(true))).Execute();
            }

            return isInputSystemConfigured;
        }
    }
}
#endif