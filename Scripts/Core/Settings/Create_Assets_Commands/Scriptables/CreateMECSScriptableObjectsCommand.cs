#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Core
{
    //* Command used to create all the assets around systems and managers on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSScriptableObjectsCommand : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ACreateMECSScriptableObjectCommand, base builder
        public CreateMECSScriptableObjectsCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            //Store dictionaries of assets
            //Casting related assets
            Dictionary<string, ScriptableObject> castingDictionary =
            new CreateMECSCastingScriptableCommands(
                complexDebugInformation.AddTempCustomText("couldnt create casting assets")).Execute(),
            //Conditionals related assets   
            conditionalsDictionary = new CreateMECSConditionalsScriptableCommands(
                complexDebugInformation.AddTempCustomText("couldnt create conditionals assets")).Execute(),
            //Timer related assets
            timerDictionary = new CreateMECSTimerScriptableCommands(
                complexDebugInformation.AddTempCustomText("couldnt create timer assets")).Execute(),
            //AddresablePooled related assets
            addresablePooledDictionary = new CreateMECSAddresablePooledScriptableCommands(
                complexDebugInformation.AddTempCustomText("couldnt create addresable pooled assets")).Execute(),
            //Functionalities related assets
            functionalitiesDictionary = new CreateMECSFunctionalitiesScriptableCommands(
                complexDebugInformation.AddTempCustomText("couldnt create functionalities assets")).Execute(),
            //GameEventListener related assets
            gameEventListenerDictionary = new CreateMECSGameEventListenerScriptableCommands(
                complexDebugInformation.AddTempCustomText("couldnt create game event assets")).Execute();

            //Check all dictionaries
            bool areDictionariesValid = CollectionsTools.dictionaryTools.AreValuesSafe(castingDictionary, complexDebugInformation)
            && CollectionsTools.dictionaryTools.AreValuesSafe(conditionalsDictionary, complexDebugInformation)
            && CollectionsTools.dictionaryTools.AreValuesSafe(timerDictionary, complexDebugInformation)
            && CollectionsTools.dictionaryTools.AreValuesSafe(addresablePooledDictionary, complexDebugInformation)
            && CollectionsTools.dictionaryTools.AreValuesSafe(functionalitiesDictionary, complexDebugInformation)
            && CollectionsTools.dictionaryTools.AreValuesSafe(gameEventListenerDictionary, complexDebugInformation);

            //Merge dictionaries
            if (areDictionariesValid)
                //Merge casting and conditionals
                if (CollectionsTools.dictionaryTools.MergeDictionaries(conditionalsDictionary, castingDictionary,
                out Dictionary<string, ScriptableObject> mergedDictionary01,
                complexDebugInformation.AddTempCustomText("couldnt merge casting and conditionals dictionaries")))
                    //Merge mergedDictionary01 with timer dictionary
                    if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary01, timerDictionary,
                    out Dictionary<string, ScriptableObject> mergedDictionary02,
                    complexDebugInformation.AddTempCustomText("couldnt merge mergedDictionary01 and timer dictionaries")))
                        //Merge mergedDictionary02 with addresable pooled dictionary
                        if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary02, addresablePooledDictionary,
                        out Dictionary<string, ScriptableObject> mergedDictionary03,
                        complexDebugInformation.AddTempCustomText("couldnt merge mergedDictionary02 and addresable pooled dictionaries")))
                            //Merge mergedDictionary03 with functionalities dictionary
                            if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary03, functionalitiesDictionary,
                            out Dictionary<string, ScriptableObject> mergedDictionary04,
                            complexDebugInformation.AddTempCustomText("couldnt merge mergedDictionary03 and functionalities dictionaries")))
                                //Merge mergedDictionary04 with game event listener dictionary
                                if (CollectionsTools.dictionaryTools.MergeDictionaries(mergedDictionary04, gameEventListenerDictionary,
                                out Dictionary<string, ScriptableObject> mergedDictionary05,
                                complexDebugInformation.AddTempCustomText("couldnt merge mergedDictionary04 and functionalities dictionaries")))
                                    //Set on final asset dictionary merged dictionaries
                                    assetsDictionary = mergedDictionary05;

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, null);

            return assetsDictionary;
        }
    }
}
#endif