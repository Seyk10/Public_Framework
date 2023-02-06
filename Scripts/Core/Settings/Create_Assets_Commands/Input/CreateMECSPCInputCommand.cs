#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Core
{
    //* Command used to create scriptable objects related to PC input
    //* T = Dictionary<string, ScriptableObject>
    public class CreateMECSPCInputCommand : ACreateMECSScriptableObjectCommand,
    ICommandReturn<Dictionary<string, ScriptableObject>>
    {
        //ACreateMECSScriptableObjectCommand, base builder
        public CreateMECSPCInputCommand(DebugTools.ComplexDebugInformation complexDebugInformation) :
        base(complexDebugInformation)
        { }

        //ACreateMECSScriptableObjectCommand, notify end of command
        public event EventHandler<Dictionary<string, ScriptableObject>> CommandFinishedEvent = null;

        //ICommandReturn, execute command creating pc input assets
        public Dictionary<string, ScriptableObject> Execute()
        {
            //Store mouse and keyboard assets 
            Dictionary<string, ScriptableObject> mouseAssets = new CreateMECSMouseInputCommand(complexDebugInformation).Execute(),
            keyboardAssets = new CreateMECSKeyboardInputCommand(complexDebugInformation).Execute();

            //Check if dictionaries are valid
            bool validDictionaries = ReferenceTools.IsValueSafe(mouseAssets, complexDebugInformation.
            AddTempCustomText("mouse input assets isn't safe"))
            && ReferenceTools.IsValueSafe(keyboardAssets, complexDebugInformation.
            AddTempCustomText("keyboard input assets isn't safe"));

            //Set assets
            if (validDictionaries)
            {
                //Merge mouse and keyboard dictionaries
                bool canMerge = CollectionsTools.dictionaryTools.MergeDictionaries(
                    mouseAssets, keyboardAssets,
                    out Dictionary<string, ScriptableObject> mergedDictionary, complexDebugInformation
                    .AddTempCustomText("couldnt merge mouse and keyboard asset dictionaries"));

                //Set merged value
                if (canMerge)
                    assetsDictionary = mergedDictionary;
            }

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, assetsDictionary);

            return assetsDictionary;
        }
    }
}
#endif