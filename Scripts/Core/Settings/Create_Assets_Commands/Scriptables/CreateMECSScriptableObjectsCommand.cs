#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Core
{
    //* Command used to create all the assets around systems and managers on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSScriptableObjectsCommand : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            //Store dictionaries of assets
            //Casting related assets
            Dictionary<string, ScriptableObject> castingDictionary =
            new CreateMECSCastingScriptableCommands().Execute(),
            //Conditionals related assets   
            conditionalsDictionary = new CreateMECSConditionalsScriptableCommands().Execute(),
            //Timer related assets
            timerDictionary = new CreateMECSTimerScriptableCommands().Execute(),
            //AddresablePooled related assets
            addresablePooledDictionary = new CreateMECSAddresablePooledScriptableCommands().Execute(),
            //Functionalities related assets
            functionalitiesDictionary = new CreateMECSFunctionalitiesScriptableCommands().Execute(),
            //GameEventListener related assets
            gameEventListenerDictionary = new CreateMECSGameEventListenerScriptableCommands().Execute();

            //Check all dictionaries
            bool areDictionariesValid = CollectionsTools.dictionaryTools
            .IsDictionaryContentSafe(castingDictionary, " given castingDictionary isn't safe")
            && CollectionsTools.dictionaryTools
            .IsDictionaryContentSafe(conditionalsDictionary, " given conditionalsDictionary isn't safe")
            && CollectionsTools.dictionaryTools
            .IsDictionaryContentSafe(timerDictionary, " given timerDictionary isn't safe")
            && CollectionsTools.dictionaryTools
            .IsDictionaryContentSafe(addresablePooledDictionary, " given addresablePooledDictionary isn't safe")
            && CollectionsTools.dictionaryTools
            .IsDictionaryContentSafe(functionalitiesDictionary, " given functionalitiesDictionary isn't safe")
            && CollectionsTools.dictionaryTools
            .IsDictionaryContentSafe(gameEventListenerDictionary, " given gameEventListenerDictionary isn't safe");

            //Merge dictionaries
            if (areDictionariesValid)
                //Merge casting and conditionals
                if (CollectionsTools.dictionaryTools.MergeDictionaries(conditionalsDictionary, castingDictionary,
                out Dictionary<string, ScriptableObject> mergedDictionary01, " couldnt merge casting and conditionals dictionaries"))
                    //Merge mergedDictionary01 with timer dictionary
                    if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary01, timerDictionary,
                    out Dictionary<string, ScriptableObject> mergedDictionary02, " couldnt merge mergedDictionary01 and timer dictionaries"))
                        //Merge mergedDictionary02 with addresable pooled dictionary
                        if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary02, addresablePooledDictionary,
                        out Dictionary<string, ScriptableObject> mergedDictionary03,
                        " couldnt merge mergedDictionary02 and addresable pooled dictionaries"))
                            //Merge mergedDictionary03 with functionalities dictionary
                            if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary03, functionalitiesDictionary,
                            out Dictionary<string, ScriptableObject> mergedDictionary04,
                            " couldnt merge mergedDictionary03 and functionalities dictionaries"))
                                //Merge mergedDictionary04 with game event listener dictionary
                                if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary04, gameEventListenerDictionary,
                                out Dictionary<string, ScriptableObject> mergedDictionary05,
                                " couldnt merge mergedDictionary04 and functionalities dictionaries"))
                                    //Set on final asset dictionary merged dictionaries
                                    assetsDictionary = mergedDictionary05;

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, null);

            return assetsDictionary;
        }
    }
}
#endif