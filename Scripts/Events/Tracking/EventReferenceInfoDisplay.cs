using System;
using UnityEngine;

namespace MECS.Events.Tracking
{
    //* Struct used to display event reference information
    //* Used to display editor information
    [Serializable]
    public struct EventReferenceInfoDisplay
    {
        //Editor variables   
        [SerializeField] private string raisingEntityName;
        [SerializeField] private string raisingComponentName;
        [SerializeField] private UnityEventTargetInfo[] unityEventTargetInfos;
        [SerializeField] private string actionName;

        //Default builder
        public EventReferenceInfoDisplay(string raisingEntityName, string raisingComponentName, UnityEventTargetInfo[] unityEventTargetInfos, string actionName)
        {
            this.raisingEntityName = raisingEntityName;
            this.raisingComponentName = raisingComponentName;
            this.unityEventTargetInfos = unityEventTargetInfos;
            this.actionName = actionName;
        }
    }
}