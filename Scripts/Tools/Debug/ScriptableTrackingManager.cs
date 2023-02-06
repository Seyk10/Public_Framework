using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.GameEvents;
using MECS.Patrons.Commands;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Manager used to track and store information of activation on each scriptable object type
    [CreateAssetMenu(fileName = "New_Scriptable_Tracking_Manager", menuName = "MECS/Managers/Scriptable_Tracking")]
    public class ScriptableTrackingManager : ScriptableObject, IDisposable
    {
        //Editor variables
        [Header("Information display")]
        [SerializeField] private string[] gameEventsActiveInformation = null;

        //Variables
        private readonly List<string> gameEventsActiveInformationList = new();

        //ScriptableObject, subscribe to tracking events
        private void OnEnable()
        {
            Dispose();
            NotificationCommand<NotifyScriptableDebugOnEnableArgs>.NotificationEvent += OnEnableResponse;
            NotificationCommand<NotifyScriptableDebugOnDisableArgs>.NotificationEvent += OnDisableResponse;
        }

        //ScriptableObject, subscribe to tracking events
        private void OnDisable()
        {
            NotificationCommand<NotifyScriptableDebugOnEnableArgs>.NotificationEvent -= OnEnableResponse;
            NotificationCommand<NotifyScriptableDebugOnDisableArgs>.NotificationEvent -= OnDisableResponse;
            Dispose();
        }

        //Method, response to scriptable enable notification
        private void OnEnableResponse(object sender, NotifyScriptableDebugOnEnableArgs args)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "OnEnableResponse(object sender, NotifyScriptableDebugOnEnableArgs args)");

            string methodName = "OnEnableResponse(object sender, NotifyScriptableDebugOnEnableArgs args)";

            //Check if event values are safe
            if (AreResponseValuesSafe(sender, args, methodName))
                //Check args values
                if (args.AreValuesValid())
                {
                    //Check game event type
                    if (args.scriptableType == typeof(GameEvent))
                    {
                        //Check if list contains value
                        if (!gameEventsActiveInformationList.Contains(args.scriptableName))
                            //Add to list
                            if (CollectionsTools.listTools.AddValue(gameEventsActiveInformationList, args.scriptableName,
                            new ComplexDebugInformation(basicDebugInformation, "couldnt add value to gameEventsActiveInformationList")))
                                gameEventsActiveInformation = gameEventsActiveInformationList.ToArray();

                        return;
                    }

                    //Debug if type wasn't supported
                    DebugNotSupportedType(methodName, args.scriptableType.Name);
                }
        }

        //Method, response to scriptable disable notification
        private void OnDisableResponse(object sender, NotifyScriptableDebugOnDisableArgs args)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "OnDisableResponse(object sender, NotifyScriptableDebugOnDisableArgs args)");

            string methodName = "OnDisableResponse(object sender, NotifyScriptableDebugOnDisableArgs args)";

            //Check if event values are safe
            if (AreResponseValuesSafe(sender, args, methodName))
                //Check args values
                if (args.AreValuesValid())
                {
                    //Check game event type
                    bool canTryGameEventRemove = args.scriptableType == typeof(GameEvent)
                    && gameEventsActiveInformationList.Count > 0;

                    if (canTryGameEventRemove)
                    {
                        //Check if list contains value
                        if (gameEventsActiveInformationList.Contains(args.scriptableName))
                            //Remove from list
                            if (CollectionsTools.listTools.RemoveValue(gameEventsActiveInformationList, args.scriptableName,
                            new ComplexDebugInformation(basicDebugInformation,
                            "couldnt remove value from gameEventsActiveInformationList")))
                                gameEventsActiveInformation = gameEventsActiveInformationList.ToArray();

                        return;
                    }

                    //Debug if type wasn't supported
                    DebugNotSupportedType(methodName, args.scriptableType.Name);
                }
        }

        //Method, debug base response values
        private bool AreResponseValuesSafe(object sender, ANotifyScriptableDebugStateArgs args, string methodName) =>
            //Check parameters
            ReferenceTools.AreValuesSafe(new object[] { sender, args, methodName },
            new ComplexDebugInformation(this.GetType().Name,
            "AreResponseValuesSafe(object sender, ANotifyScriptableDebugStateArgs args, string methodName)",
            "given parameters aren't safe"))

            //Check args
            && args.AreValuesValid();

        //Method, debug not supported type
        private void DebugNotSupportedType(string methodName, string typeName)
            => DebugTools.DebugWarning(new ComplexDebugInformation(this.GetType().Name, methodName,
             "type " + typeName + " isn't supported."));

        //IDisposable, clean values
        public void Dispose()
        {
            gameEventsActiveInformation = null;
            gameEventsActiveInformationList.Clear();
        }
    }
}