#if UNITY_EDITOR
using System;
using MECS.Patrons.Commands;
using MECS.Collections;
using static MECS.Tools.DebugTools;
using UnityEngine;
using System.Collections.Generic;

namespace MECS.Core
{
    //* Command used to create all the assets around Input system and manager on MECS framework
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSInputScriptableCommands : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ACreateMECSScriptableObjectCommand, default builder
        public CreateMECSInputScriptableCommands(ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ICommandReturn, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, instantiate all the assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            //Store PC assets
            Dictionary<string, ScriptableObject> pcAssets =
            new CreateMECSPCInputCommand(complexDebugInformation).Execute();

            //Check dictionary values
            if (CollectionsTools.dictionaryTools.AreValuesSafe(pcAssets,
            complexDebugInformation.AddTempCustomText("pc assets aren't safe")))
                assetsDictionary = pcAssets;

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, assetsDictionary);

            return assetsDictionary;
        }
    }
}
#endif