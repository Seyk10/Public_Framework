using System;
using UnityEngine;

namespace MECS.Events.Tracking
{
    //* Struct used to store tracking information about unity events
    //* Structure used to display info on editor
    [Serializable]
    public class UnityEventTargetInfo
    {
        //Editor variables
        [SerializeField] private string targetName = null;
        [SerializeField] private string targetState = null;
        [SerializeField] private string methodName = null;

        //Builder to set manual info
        public UnityEventTargetInfo(string targetName, string targetState, string methodName)
        {
            this.targetName = targetName;
            this.targetState = targetState;
            this.methodName = methodName;
        }

        //Attributes
        public string TargetName => targetName;
        public string TargetState => targetState;
        public string MethodName => methodName;
    }
}