#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Colliders;
using MECS.Conditionals;
using MECS.Entities.Enums;
using MECS.Events;
using MECS.Events.Tracking;
using MECS.Filters;
using MECS.GameEvents;
using MECS.LifeCycle;
using MECS.MemoryManagement.Entity;
using MECS.MemoryManagement.Entity.Pooling;
using MECS.Patrons.Commands;
using MECS.Physics.Casting;
using MECS.Timers;
using MECS.Tools;
using UnityEngine;
using static MECS.Core.MECSDefaultSettingsNaming.ManagersNaming;

namespace MECS.Core
{
    //* Command used to create all the managers on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSManagersCommand : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ACreateMECSScriptableObjectCommand, base builder
        public CreateMECSManagersCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the managers asset and return it as ScriptableObject dictionary
        public Dictionary<string, ScriptableObject> Execute()
        {
            string path = MECSDefaultSettingsNaming.managersNaming.DEFAULT_MANAGERS_PATH;

            //Dictionary of assets created assets
            Dictionary<string, ScriptableObject> tempDictionary = new Dictionary<string, ScriptableObject>();

            //Store all asset names
            ManagersAssetsNaming assetsNaming =
            MECSDefaultSettingsNaming.managersNaming.managersAssetsNaming;

            //Check if could create all assets
            //Create collider manager asset
            bool couldntCreateAssets = CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.COLLIDER_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<ColliderManager>
            (complexDebugInformation.AddTempCustomText("couldnt create " + assetsNaming.COLLIDER_MANAGER_NAME),
            path, assetsNaming.COLLIDER_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create conditional manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.CONDITIONAL_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<ConditionalManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.CONDITIONAL_MANAGER_NAME),
            path, assetsNaming.CONDITIONAL_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create scriptable enum manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.SCRIPTABLE_ENUM_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<ScriptableEnumManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.SCRIPTABLE_ENUM_MANAGER_NAME),
            path, assetsNaming.SCRIPTABLE_ENUM_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create event manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.EVENT_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<EventManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.EVENT_MANAGER_NAME),
            path, assetsNaming.EVENT_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create filter manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.FILTER_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<FilterManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.FILTER_MANAGER_NAME),
            path, assetsNaming.FILTER_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create game event manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.GAME_EVENT_LISTENER_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<GameEventListenerManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.GAME_EVENT_LISTENER_MANAGER_NAME),
            path, assetsNaming.GAME_EVENT_LISTENER_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create life cycle manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.LIFE_CYCLE_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<LifeCycleManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.LIFE_CYCLE_MANAGER_NAME),
            path, assetsNaming.LIFE_CYCLE_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create addresable entity manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.ADDRESABLE_ENTITY_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<AddresableEntityManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.ADDRESABLE_ENTITY_MANAGER_NAME),
            path, assetsNaming.ADDRESABLE_ENTITY_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create addresable pooled manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.ADDRESABLE_POOLED_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<AddresablePooledManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.ADDRESABLE_POOLED_MANAGER_NAME),
            path, assetsNaming.ADDRESABLE_POOLED_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create casting manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.CASTING_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<CastingManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.CASTING_MANAGER_NAME),
            path, assetsNaming.CASTING_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create timer manager asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.TIMER_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<TimerManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.TIMER_MANAGER_NAME),
            path, assetsNaming.TIMER_MANAGER_NAME).Execute(),
            complexDebugInformation)

            //Create event tracking asset
            && CollectionsTools.dictionaryTools.AddValue(tempDictionary, assetsNaming.EVENT_TRACKING_MANAGER_NAME,
            new CreatePersistentScriptableObjectCommand<EventTrackingManager>
            (complexDebugInformation.
            AddTempCustomText("couldnt create " + assetsNaming.EVENT_TRACKING_MANAGER_NAME),
            path, assetsNaming.EVENT_TRACKING_MANAGER_NAME).Execute(),
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