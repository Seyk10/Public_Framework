using UnityEngine;
using MECS.Core;
using System.Diagnostics;

namespace MECS.Tools
{
    //* Static class used to make a standard of debugging
    public static class DebugTools
    {
        //Notify information using a standard
        public static void DebugLog(ComplexDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                UnityEngine.Debug.Log("Log: On " + debugInformation.scriptName + ", " +
                debugInformation.methodName + ", " +
                debugInformation.CustomText);
        }

        //Notify warning information using a standard
        public static void DebugWarning(ComplexDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                UnityEngine.Debug.LogWarning("Warning: On " + debugInformation.scriptName + ", " +
                debugInformation.methodName + ", " +
                debugInformation.CustomText);
        }

        //Notify error information using a standard
        public static void DebugError(BasicDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                UnityEngine.Debug.LogError("Error: On " + debugInformation.scriptName + ", " + debugInformation.methodName);
        }

        //Notify error information using a standard
        public static void DebugError(ComplexDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                UnityEngine.Debug.LogError("Error: On " + debugInformation.scriptName + ", " +
                debugInformation.methodName + ", " +
                debugInformation.CustomText);
        }

        //Nested class, used to store and share basic debug information
        public class BasicDebugInformation
        {
            //Variables
            public string scriptName = null,
            methodName = null;

            //Base builder
            public BasicDebugInformation(string scriptName, string methodName)
            {
                this.scriptName = scriptName;
                this.methodName = methodName;
            }

            //Base builder
            public BasicDebugInformation(object entity, StackTrace stackTrace)
            {
                //Get type from entity
                this.scriptName = entity.GetType().Name;
                //Get trace by reflection
                this.methodName = stackTrace.GetFrame(0).GetMethod().Name;
            }
        }

        //Nested class, used to store and share complex debug information
        public class ComplexDebugInformation : BasicDebugInformation
        {
            //Variables            
            private string customText = null;
            public string CustomText => customText;

            //Default builder
            public ComplexDebugInformation(string scriptName, string methodName, string customText)
            : base(scriptName, methodName) => this.customText = customText;

            //Builder based on basic debugInformation
            public ComplexDebugInformation(BasicDebugInformation basicDebugInformation, string customText)
            : base(basicDebugInformation.scriptName, basicDebugInformation.methodName) => this.customText = customText;

            //Method, add new text and return debugInformation
            public ComplexDebugInformation AddCustomText(string text)
            {
                customText += ", " + text;

                return this;
            }

            //Method, add temporal custom text and return new debugInformation
            public ComplexDebugInformation AddTempCustomText(string text)
            => new ComplexDebugInformation(scriptName, methodName, customText + ", " + text);
        }
    }
}