using System.Diagnostics;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Tools
{
    //* Executions related to type operations
    public static class TypeTools
    {
        //Method, check if can convert to type object
        public static bool IsType<T>(object value) => ReferenceTools.IsValueSafe(value)
        && value is T;

        //Method, check if can convert to type object
        //T = target type
        public static bool IsType<T>(object value, string debugMessage)
        {
            //Return value
            bool isCorrect = ReferenceTools.IsValueSafe(value, debugMessage + "given value to check type isn't safe");

            //Check type
            if (isCorrect)
            {
                isCorrect = value is T;

                //Notify debug managers
                if (!isCorrect)
                    new NotificationCommand<DebugArgs>(null, new DebugArgs(debugMessage
                    + " given values isn't target type, value type is "
                    + value.GetType().Name + " and target type is " + typeof(T).Name, LogType.Error, new StackTrace(true))).Execute();
            }

            return isCorrect;
        }

        //Method, convert value to target type and out it
        public static bool ConvertToType<T>(object value, out T convertedObject, string debugMessage)
        {
            //Check parameters
            bool areParametersValid = ReferenceTools.IsValueSafe(value, debugMessage + " given value to convert isn't valid");

            //Check type
            bool isCorrectType = false;
            convertedObject = default;

            //Make executions
            if (areParametersValid)
            {
                isCorrectType = IsType<T>(value, debugMessage + " given value to convert isn't target type");

                convertedObject = isCorrectType ? (T)value : default;
            }

            //Return if value is given type
            return isCorrectType;
        }
    }
}