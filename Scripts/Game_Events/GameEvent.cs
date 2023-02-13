using UnityEngine;
using MECS.Patrons.Observers;
using System.Collections.Generic;
using MECS.Tools;
using MECS.Collections;
using System;
using MECS.Patrons.Commands;
using static MECS.Tools.DebugTools;

namespace MECS.GameEvents
{
    //* Scriptable object used to raise events around a group of listeners
    //* Store observer object to register listeners
    [CreateAssetMenu(fileName = "New_Game_Event", menuName = "MECS/Patrons/Game_Event")]
    public class GameEvent : ScriptableObject, IDisposable
    {
        //Editor variables
        [Header("Display")]
        [SerializeField] private string[] subjectsNames = null;
        [SerializeField] private string[] callsInformation = null;

        //Variables
        private readonly List<string> subjectsNamesList = new(),
        callsList = new();
        public List<string> SubjectsNamesList => subjectsNamesList;

        //ScriptableObject, make subscriptions and dispose for fresh start
        private void OnEnable()
        {
            //Clean data
            Dispose();

            //Subscribe to observer events
            observer.ListenerAdditionEvent += AddEditorDisplayInformation;
            observer.ListenerRemoveEvent += RemoveEditorDisplayInformation;

            //Notify when scriptable is enabled
            new NotificationCommand<NotifyScriptableDebugOnEnableArgs>(this,
                new NotifyScriptableDebugOnEnableArgs(this.GetType(), this.name, " couldnt notify game event OnEnable")).Execute();
        }

        //ScriptableObject, remove subscriptions
        private void OnDisable()
        {
            //Unsubscribe to observer events
            observer.ListenerAdditionEvent -= AddEditorDisplayInformation;
            observer.ListenerRemoveEvent -= RemoveEditorDisplayInformation;

            //Notify when scriptable is disabled
            new NotificationCommand<NotifyScriptableDebugOnDisableArgs>(this,
                new NotifyScriptableDebugOnDisableArgs(this.GetType(), this.name, " couldnt notify game event OnEnable")).Execute();
        }

        //Method, add editor information
        private void AddEditorDisplayInformation(object sender, string args)
        {
            //Make check of each value
            bool isSenderSafe = ReferenceTools.IsValueSafe(sender, " given sender isn't valid"),
            areArgsSafe = ReferenceTools.IsValueSafe(args, " given args isn't valid"),
            areReferencesSafe = isSenderSafe && areArgsSafe;

            //Make updates of display
            if (areReferencesSafe)
            {
                //Make add to subjectsNamesList
                if (CollectionsTools.listTools.AddValue(subjectsNamesList, args,
                " couldnt add to subjectsNamesList on game event " + this.name))
                {
                    //Notify debug information, adding subscribers
                    new NotificationCommand<NotifyScriptableDebugOnEnableArgs>(this,
                    new NotifyScriptableDebugOnEnableArgs(this.GetType(), this.name, " couldnt notify game event OnEnable")).Execute();

                    //Set new names
                    subjectsNames = subjectsNamesList.ToArray();
                }
            }
        }

        //Method, add editor information
        private void RemoveEditorDisplayInformation(object sender, string args)
        {
            //Make check of each value
            bool isSenderSafe = ReferenceTools.IsValueSafe(sender, " given sender isn't valid"),
            areArgsSafe = ReferenceTools.IsValueSafe(args, " given args isn't valid"),
            areReferencesSafe = isSenderSafe && areArgsSafe;

            //Make updates of display
            if (areReferencesSafe)
                //Make remove to subjectsNamesList
                if (CollectionsTools.listTools.RemoveValue(subjectsNamesList, args, " couldnt remove args from subjectsNamesList"))
                    subjectsNames = subjectsNamesList.ToArray();
        }

        //IDisposable, clean all collections
        public void Dispose()
        {
            observer.Dispose();
            callsList.Clear();
            subjectsNamesList.Clear();
        }

        //Variables
        private readonly Observer observer = new Observer();
        public Observer Observer => observer;

        //Method, raise game event
        public void RaiseEvent() => observer.RaiseSubjects();
    }
}