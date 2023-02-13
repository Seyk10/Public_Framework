using System.Diagnostics;
using MECS.Core;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Tools
{
    //* Manager used to custom tracking of exceptions and errors on executions
    [CreateAssetMenu(fileName = "New_Exception_Manager", menuName = "MECS/Managers/Exception")]
    public class DebugManager : ScriptableObject
    {
        //Variables
        private bool isLogEventSubscribed = false;
        private const string DEBUG_SEPARATION = "*---------------------------------------------------------------------------* \n";

        //ScriptableObject method, calls when asset is enable
        private void OnEnable()
        {
            //Subscribe if debug mode is active
            if (!isLogEventSubscribed)
            {
                //Store if manager is subscribe in case if MECSSettings.CanDebug value change
                isLogEventSubscribed = true;
                Application.logMessageReceived += ApplicationLogExceptionResponse;
                NotificationCommand<DebugArgs>.NotificationEvent += NotificationDebugResponse;
            }
        }

        //ScriptableObject method, calls when asset is disable
        private void OnDisable()
        {
            //Unsubscribe if response is subscribed
            if (isLogEventSubscribed)
            {
                //Store if manager is subscribe in case if MECSSettings.CanDebug value change
                isLogEventSubscribed = false;
                Application.logMessageReceived -= ApplicationLogExceptionResponse;
                NotificationCommand<DebugArgs>.NotificationEvent -= NotificationDebugResponse;
            }
        }

        //Method, response to Application.logMessageReceived used to debug on console error values
        private void ApplicationLogExceptionResponse(string logText, string stackTrace, LogType logType)
        {
            //Check if exception is correct type and debug as error
            if (logType.Equals(LogType.Exception))
                DebugInformation(logText + " " + stackTrace, logType, new StackTrace(true));
        }

        //Method, response to NotificationCommand<DebugArgs> used to receive all debug information
        private void NotificationDebugResponse(object sender, DebugArgs args)
        {
            //Check if given args are valid and debug information
            if (args.AreValuesValid())
                DebugInformation(args.debugMessage, args.logType, args.stackTrace);
        }

        //Method, debug information based on debug type
        private void DebugInformation(string exceptionMessage, LogType logType, StackTrace stackTrace)
        {
            //Store trace values
            //Create a StackTrace that captures filename
            //StackTrace stackTrace = new StackTrace();
            string debugStack = null,
            stackTree = "";

            //Line number and column information.
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                //Note that at this level, there are four
                //Stack frames, one for each method invocation.
                StackFrame sf = stackTrace.GetFrame(i);

                //Add method information
                debugStack += stackTree + "Method: " + sf.GetMethod().ToString() + "\n";
                //Add file information
                debugStack += stackTree + "File: " + sf.GetFileName() + "\n";
                //Add life number information
                debugStack += stackTree + "Line Number: " + sf.GetFileLineNumber() + "\n";
                //Add new line
                debugStack += DEBUG_SEPARATION;

                //Add space to stack tree
                stackTree += "        ";
            }

            //Add exception message to stack
            debugStack += exceptionMessage + "\n";

            //Avoid if cant debug on console
            if (MECSSettings.CanDebug)
                //Check debug type and debug target type
                switch (logType)
                {
                    case LogType.Log:
                        UnityEngine.Debug.Log(debugStack);
                        break;

                    case LogType.Warning:
                        UnityEngine.Debug.LogWarning(debugStack);
                        break;

                    case LogType.Error:
                        UnityEngine.Debug.LogError(debugStack);
                        break;

                    case LogType.Exception:
                        debugStack = "Error: Given log got from an unexpected exception. \n"
                        + DEBUG_SEPARATION
                        + debugStack;

                        UnityEngine.Debug.LogError(debugStack);
                        break;

                    default:
                        debugStack = "Error: Given log type isn't implemented. \n"
                        + DEBUG_SEPARATION
                        + debugStack;

                        UnityEngine.Debug.LogError(debugStack);
                        break;
                }

            //TODO: Create log file with given information 
        }
    }
}