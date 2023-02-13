#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Patrons.Commands;
using MECS.Timers;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.ScriptableObjectsNaming.TimerScriptableObjectsNaming;

namespace MECS.Core
{
    //* Command used to create all the assets around Timer system and manager on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSTimerScriptableCommands : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            //Path for assets
            string path = MECSDefaultSettingsNaming.scriptableObjectsNaming
            .timerScriptableObjectsNaming.DEFAULT_TIMER_SCRIPTABLE_OBJECTS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            TimerScriptableObjectsAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.scriptableObjectsNaming.timerScriptableObjectsNaming.timerScriptableObjectsAssetsNaming;

            //Check if could create all assets
            //Create initialize timer asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.INITIALIZE_TIMER_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<InitializeTimerCommand>
            (path, assetsNaming.INITIALIZE_TIMER_COMMAND_NAME).Execute())

            //Create pause timer asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.PAUSE_TIMER_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<PauseTimerCommand>
            (path, assetsNaming.PAUSE_TIMER_COMMAND_NAME).Execute())

            //Create run timer asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.RUN_TIMER_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<RunTimerCommand>
            (path, assetsNaming.RUN_TIMER_COMMAND_NAME).Execute())

            //Create stop timer asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.STOP_TIMER_COMMAND_NAME,
            new CreatePersistentScriptableObjectCommand<StopTimerCommand>
            (path, assetsNaming.STOP_TIMER_COMMAND_NAME).Execute());

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