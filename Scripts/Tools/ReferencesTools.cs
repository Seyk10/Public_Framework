using System;
using MECS.Collections;
using MECS.Core;
using MECS.Variables;
using MECS.Variables.References;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //*Tool class used to generic reference solutions
    public static class ReferenceTools
    {
        //Method, debug non valid reference information
        public static void DebugWarningNoValidReference(string scriptName, string methodName, string referenceName)
            => Debug.LogWarning("Warning: On " + scriptName + ", " + methodName + ", " + referenceName + " is not valid.");

        public static void DebugErrorNoValidReference(string scriptName, string methodName, string referenceName)
            => Debug.LogError("Warning: On " + scriptName + ", " + methodName + ", " + referenceName + " is not valid.");

        //Check data references for its use and return data on as reference
        // T = Data type
        // T2 = Profile type
        public static bool CanUseData<T, T2>(DataReference<T, T2> dataReference, out T[] data) where T : AData where T2 : AProfile<T>
        {
            data = dataReference.GetValue();
            return dataReference != null && data != null;
        }

        //Check if given value is safe
        public static bool IsValueSafe(object value)
        {
            //Return value
            bool isReferenceSafe = true;

            //Check reference
            if (value == null)
                isReferenceSafe = false;

            return isReferenceSafe;
        }

        //Check if given value is safe
        public static bool IsValueSafe(object value, ComplexDebugInformation complexDebugInformation)
        {
            //Return value
            bool isReferenceSafe = true;

            //Check reference
            //Values cant be null
            if (value == null)
            {
                isReferenceSafe = false;
                DebugTools.DebugError(complexDebugInformation.AddCustomText("value is null"));
            }
            //Value cant be default
            else if (value == default)
            {
                isReferenceSafe = false;
                DebugTools.DebugError(complexDebugInformation.AddCustomText("value is default"));
            }


            return isReferenceSafe;
        }

        //Check if given value is safe
        public static bool IsValueSafe(object value, BasicDebugInformation nullValueDebug)
        {
            //Return value
            bool isReferenceSafe = true;

            //Check reference
            if (value == null)
            {
                isReferenceSafe = false;
#if UNITY_EDITOR
                DebugTools.DebugError(new ComplexDebugInformation(nullValueDebug, "values is null"));
#endif
            }

            return isReferenceSafe;
        }

        //Check if given reference is safe
        public static bool IsVariableReferenceSafe<T, T2>(AVariableReference<T, T2> reference,
        ComplexDebugInformation complexDebugInformation) where T2 : AVariable<T>
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new("ReferenceTools",
            "IsReferenceSafe<T, T2>(AReference<T, T2> reference, ComplexDebugInformation complexDebugInformation)");

            //Make checking
            return
            //Check parameters
            AreValuesSafe(new object[] { reference, complexDebugInformation },
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't safe"))

            && IsValueSafe(reference.Value, complexDebugInformation.AddTempCustomText("value on given variable reference isn't safe"));
        }

        //Check if given variable reference is safe
        public static bool IsVariableSafe<T>(AVariable<T> variable, bool checkValue = false)
        => IsValueSafe(variable) && checkValue ? IsValueSafe(variable.Value) : true;

        //Check if given variable reference is safe
        public static bool IsVariableSafe<T>(AVariable<T> variable, BasicDebugInformation nullValueDebug, bool checkValue = false)
        => IsValueSafe(variable, nullValueDebug) && checkValue ? IsValueSafe(variable.Value, nullValueDebug) : true;

        //Check if given variable reference is safe
        public static bool IsVariableSafe<T>(AVariable<T> variable, ComplexDebugInformation nullValueDebug, bool checkValue = false)
        => IsValueSafe(variable, nullValueDebug) && checkValue ? IsValueSafe(variable.Value, nullValueDebug) : true;

        //Convert given object to given type
        // T = target type
        public static T ConvertObjectToType<T>(object value) => value is T ? (T)value : default;

        //Check if given event raise parameters are valid
        public static bool AreEventParametersValid<T>(object sender, T args,
        ComplexDebugInformation complexDebugInformation) where T : EventArgs, IValuesChecking =>
            //Check sender parameter
            ReferenceTools.IsValueSafe(sender,
            complexDebugInformation.AddTempCustomText("given sender isn't safe"))

            //Check args parameter
            && ReferenceTools.IsValueSafe(args,
            complexDebugInformation.AddTempCustomText("given args isn't safe"))

            //Check args values
            && args.AreValuesValid();

        //Check if given values are
        public static bool AreValuesSafe(object[] values, ComplexDebugInformation complexDebugInformation)
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new("ReferenceTools",
            "AreReferencesSafe(object[] values, ComplexDebugInformation complexDebugInformation)");

            //Return value
            bool areParametersSafe =
            //Check values array
            CollectionsTools.arrayTools.IsArrayContentSafe(values, new ComplexDebugInformation(basicDebugInformation,
            "given values array isn't safe"))

            //Check debug information
            && ReferenceTools.IsValueSafe(complexDebugInformation,
            new ComplexDebugInformation(basicDebugInformation, "given debug information isn't safe"));

            //Debug error with given debug information
            if (!areParametersSafe)
                DebugTools.DebugError(complexDebugInformation.AddTempCustomText("references aren't safe"));

            return areParametersSafe;
        }
    }
}