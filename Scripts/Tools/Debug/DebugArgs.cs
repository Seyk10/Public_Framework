using System;
using System.Diagnostics;
using UnityEngine;

namespace MECS.Tools
{
    //* Debug args used to share values to exception manager
    public class DebugArgs : EventArgs, IValuesChecking
    {
        //Variables
        public string debugMessage = null;
        public LogType logType = LogType.Log;
        public StackTrace stackTrace = null;

        //Basic builder
        public DebugArgs(string debugMessage, LogType logType, StackTrace stackTrace)
        {
            this.debugMessage = debugMessage;
            this.logType = logType;
            this.stackTrace = stackTrace;
        }

        //IValuesChecking method, check if given values are valid
        //Used normal debug to avoid stack over flow
        public bool AreValuesValid()
        {
            //Check if values are valid
            bool areValidValues = true;

            //Check message 
            if (debugMessage == null)
            {
                areValidValues = false;
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Error: debugMessage on DebugArgs is null.");
#endif
            }
            //Check stack
            else if (stackTrace == null)
            {
                areValidValues = false;
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Error: stackTrace on DebugArgs is null.");
#endif
            }

            return areValidValues;
        }
    }
}
