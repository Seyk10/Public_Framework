using System;
using MECS.Tools;
using MECS.Variables.References;
using UnityEngine;
using static MECS.Tools.DebugTools;
using Steamworks;
using MECS.Core;

namespace MECS.Integrations
{
    //TODO: IMPLEMENT NEW DEBUG SYSTEM
    //* Scriptable object used to get current state of given steam achievement
    [CreateAssetMenu(fileName = "New_Achievement", menuName = "MECS/Integrations/Steam/Achievement")]
    public class SteamAchievementStateScriptable : ScriptableObject, IDisposable
    {
        //Editor variables
        [Header("Configuration")]
        [SerializeField] private StringReference achievementName = null;
        [SerializeField] private EAchievementState achievementState = EAchievementState.Null;

        //ScriptableObject method, calls when asset isn't loaded
        private void OnDisable() => Dispose();

        //Method, return current achievement state
        public EAchievementState GetState()
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "GetState()");

            //Avoid errors
            if (ReferenceTools.IsVariableReferenceSafe(achievementName, " name of achievement isn't safe on " + this.name))

                //Avoid if settings aren't loaded
                if (MECSSettings.AreSettingsLoaded)

                    //Check if steam is initialized
                    if (SteamLoaderComponent.Initialized)
                        //Current state
                        if (SteamUserStats.GetAchievement(achievementName.Value, out bool isAchievementAccomplish))
                        {
                            //Set state    
                            if (isAchievementAccomplish)
                                achievementState = EAchievementState.Complete;
                            else
                                achievementState = EAchievementState.Uncompleted;
                        }
                        //Debug if couldnt get achievement
                        else
                            DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                            "couldnt get achievement on " + this.name));
                    //Debug error if steam manager isn't initialized
                    else
                        DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                            "steam manager isn't initialized"));

            return achievementState;
        }

        //Method, accomplish achievement 
        public void AccomplishAchievement()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AccomplishAchievement()");

            //Avoid if settings aren't loaded
            if (MECSSettings.AreSettingsLoaded)

                //Check if steam is initialized
                if (SteamLoaderComponent.Initialized)
                    //Check current state of achievement
                    switch (GetState())
                    {
                        //Complete achievement
                        case EAchievementState.Uncompleted:
                            //Set complete achievement
                            if (SteamUserStats.SetAchievement(achievementName.Value))
                            {
                                achievementState = EAchievementState.Complete;
                                //Save progress
                                SteamUserStats.StoreStats();
                            }

                            //Debug if couldnt set achievement
                            else
                                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                                "couldnt set achievement complete state"));
                            break;

                        //Debug error
                        case EAchievementState.Null:
                            DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                            "couldnt load achievement on " + this.name));
                            break;
                    }
                //Debug error if steam manager isn't initialized
                else
                    DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                        "steam manager isn't initialized"));
        }

        //IDisposable method, clean achievement state
        public void Dispose() => achievementState = EAchievementState.Null;
    }
}