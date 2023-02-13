using System;
using System.Diagnostics;
using MECS.Collections;
using MECS.Patrons.Commands;
using MECS.Variables;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Tools
{
    //*Tool class used to generic reference solutions
    public static class ReferenceTools
    {
        #region VALUES_CHECKING  
        //Method, check if given value is safe
        public static bool IsValueSafe(object value, string debugMessage = null, LogType logType = LogType.Error)
        {
            //Return value
            bool isReferenceSafe = true;

            //Check reference
            //Values cant be null
            if (value.Equals(null))
            {
                isReferenceSafe = false;

                //Check if should debug
                if (debugMessage.Equals(null))
                    //Notify exceptions manager
                    new NotificationCommand<DebugArgs>(null, new DebugArgs(debugMessage + " value is null",
                    logType, new StackTrace(true))).Execute();
            }
            //Value cant be default
            else if (value.Equals(default))
            {
                isReferenceSafe = false;

                //Check if should debug
                if (debugMessage.Equals(null))
                    //Notify exceptions manager
                    new NotificationCommand<DebugArgs>(null, new DebugArgs(debugMessage + " value is default",
                    logType, new StackTrace(true))).Execute();
            }

            return isReferenceSafe;
        }
        #endregion
        #region VARIABLES_CHECKING
        //method, check if given variable reference is safe
        public static bool IsVariableReferenceSafe<T, T2>(AVariableReference<T, T2> reference,
        string debugMessage) where T2 : AVariable<T> =>
            CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { reference, reference.Value },
            debugMessage + " given variable reference isn't safe");

        //Method, check if given variable reference is safe
        public static bool IsVariableSafe<T>(AVariable<T> variable, string debugMessage) =>
            CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { variable, variable.Value },
            debugMessage + " given variable isn't safe");
        #endregion

        //Method, check if given event raise parameters are valid
        public static bool AreEventParametersValid<T>(object sender, T args, string debugMessage) where T : EventArgs, IValuesChecking =>
            //Check sender parameter
            ReferenceTools.IsValueSafe(sender,
            debugMessage + " given sender isn't safe")

            //Check args parameter
            && ReferenceTools.IsValueSafe(args,
            debugMessage + " given args isn't safe")

            //Check args values
            && args.AreValuesValid();
    }
}