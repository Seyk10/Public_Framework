using UnityEngine;
using MECS.Core;

namespace MECS.Tools
{
    //* Static class used to make a standard of debugging
    public static class DebugTools
    {
        //Notify information using a standard
        public static void DebugLog(ComplexDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                Debug.Log("Log: On " + debugInformation.scriptName + ", " +
                debugInformation.methodName + ", " +
                debugInformation.CustomText);
        }

        //Notify warning information using a standard
        public static void DebugWarning(ComplexDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                Debug.LogWarning("Warning: On " + debugInformation.scriptName + ", " +
                debugInformation.methodName + ", " +
                debugInformation.CustomText);
        }

        //Notify error information using a standard
        public static void DebugError(BasicDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                Debug.LogError("Error: On " + debugInformation.scriptName + ", " + debugInformation.methodName);
        }

        //Notify error information using a standard
        public static void DebugError(ComplexDebugInformation debugInformation)
        {
            if (MECSSettings.CanDebug)
                Debug.LogError("Error: On " + debugInformation.scriptName + ", " +
                debugInformation.methodName + ", " +
                debugInformation.CustomText);
        }

        //Nested class, used to store and share basic debug information
        public class BasicDebugInformation
        {
            //Variables
            public readonly string scriptName = null,
            methodName = null;

            //Base builder
            public BasicDebugInformation(string scriptName, string methodName)
            {
                this.scriptName = scriptName;
                this.methodName = methodName;
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