#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Colliders;
using MECS.Conditionals;
using MECS.Entities.Enums;
using MECS.Events;
using MECS.Filters;
using MECS.GameEvents;
using MECS.Input;
using MECS.LifeCycle;
using MECS.MemoryManagement.Entity;
using MECS.MemoryManagement.Entity.Pooling;
using MECS.Patrons.Commands;
using MECS.Physics.Casting;
using MECS.Timers;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.SystemsNaming;

namespace MECS.Core
{
    //* Command used to create all the systems on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSSystemsCommand : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the system asset and return it as ScriptableObject dictionary
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.systemsNaming.DEFAULT_SYSTEMS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            SystemAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.systemsNaming.systemAssetsNaming;

            //Check if could create all assets
            //Create collider system asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.COLLIDER_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<ColliderSystem>
            (path, assetsNaming.COLLIDER_SYSTEM_NAME).Execute())

            //Create conditional system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.CONDITIONAL_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<ConditionalSystem>
            (path, assetsNaming.CONDITIONAL_SYSTEM_NAME).Execute())

            //Create scriptable enum manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SCRIPTABLE_ENUM_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<ScriptableEnumSystem>
            (path, assetsNaming.SCRIPTABLE_ENUM_SYSTEM_NAME).Execute())

            //Create event system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.EVENT_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<EventSystem>
            (path, assetsNaming.EVENT_SYSTEM_NAME).Execute())

            //Create filter system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.FILTER_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<FilterSystem>
            (path, assetsNaming.FILTER_SYSTEM_NAME).Execute())

            //Create game event system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.GAME_EVENT_LISTENER_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<GameEventListenerSystem>
            (path, assetsNaming.GAME_EVENT_LISTENER_SYSTEM_NAME).Execute())

            //Create life cycle system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.LIFE_CYCLE_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<LifeCycleSystem>
            (path, assetsNaming.LIFE_CYCLE_SYSTEM_NAME).Execute())

            //Create addresable entity system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.ADDRESABLE_ENTITY_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<AddresableEntitySystem>
            (path, assetsNaming.ADDRESABLE_ENTITY_SYSTEM_NAME).Execute())

            //Create addresable pooled system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.ADDRESABLE_POOLED_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<AddresablePooledSystem>
            (path, assetsNaming.ADDRESABLE_POOLED_SYSTEM_NAME).Execute())

            //Create casting system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.CASTING_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<CastingSystem>
            (path, assetsNaming.CASTING_SYSTEM_NAME).Execute())

            //Create timer system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.TIMER_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<TimerSystem>
            (path, assetsNaming.TIMER_SYSTEM_NAME).Execute())

            //Create input system asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.INPUT_SYSTEM_NAME,
            new CreatePersistentScriptableObjectCommand<InputSystem>
            (path, assetsNaming.INPUT_SYSTEM_NAME).Execute());

            //Debug about input system
            Debug.LogWarning("new input system isn't configure, set it manually or use MECS/Settings/Reset inputs");

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