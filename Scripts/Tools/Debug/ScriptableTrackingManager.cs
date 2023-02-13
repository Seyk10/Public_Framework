using System;
using System.Collections.Generic;
using MECS.Collections;
using MECS.GameEvents;
using MECS.Patrons.Commands;
using UnityEngine;

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
            //Check if event values are safe
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't safe"))
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
                            " couldnt add value to gameEventsActiveInformationList"))
                                gameEventsActiveInformation = gameEventsActiveInformationList.ToArray();

                        return;
                    }
                }
        }

        //Method, response to scriptable disable notification
        private void OnDisableResponse(object sender, NotifyScriptableDebugOnDisableArgs args)
        {
            //Check if event values are safe
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't safe"))
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
                        " couldnt remove value from gameEventsActiveInformationList"))
                            gameEventsActiveInformation = gameEventsActiveInformationList.ToArray();

                    return;
                }
            }
        }

        //IDisposable, clean values
        public void Dispose()
        {
            gameEventsActiveInformation = null;
            gameEventsActiveInformationList.Clear();
        }
    }
}