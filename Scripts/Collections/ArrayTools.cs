using System.Collections.Generic;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Collections
{
    //* Executions related to array operations
    public class ArrayTools
    {
        #region NOT_SAFE_OPERATIONS
        //Check if values inside the array and array are correct or valid
        // T = array type
        public bool IsArraySafe<T>(T[] array, BasicDebugInformation debugInformation)
        {
            //Value to return
            bool areValuesSafe = ReferenceTools.IsValueSafe(array, debugInformation);

            //Avoid null array
            if (areValuesSafe)
            {
                //Avoid 0 size
                if (array.Length != 0)
                {
                    //Itinerate array and check values
                    foreach (var value in array)
                        //Check value
                        if (!ReferenceTools.IsValueSafe(value, debugInformation))
                        {
                            areValuesSafe = false;
                            break;
                        }
                }
                //0 length arrays aren't safe
                else
                {
                    areValuesSafe = false;
#if UNITY_EDITOR
                    DebugTools.DebugError(debugInformation);
#endif
                }
            }

            return areValuesSafe;
        }

        //Check if values inside the array and array are correct or valid
        // T = array type
        public bool IsArraySafe<T>(T[] array)
        {
            //Value to return
            bool areValuesSafe = ReferenceTools.IsValueSafe(array);

            //Avoid null array
            if (areValuesSafe)
            {
                //Avoid 0 size
                if (array.Length > 0)
                {
                    //Itinerate array and check values
                    foreach (var value in array)
                        //Check value
                        if (!ReferenceTools.IsValueSafe(value))
                        {
                            areValuesSafe = false;
                            break;
                        }
                }
                else
                    areValuesSafe = false;
            }

            return areValuesSafe;
        }
        #endregion
        #region SAFE_OPERATIONS
        //Check if values inside the array and array are correct or valid
        // T = array type
        public bool IsArrayContentSafe<T>(T[] array, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "IsArraySafe<T>(T[] array, ComplexDebugInformation complexDebugInformation)");

            //Value to return
            bool areParametersSafe =
                //Check array reference
                ReferenceTools.IsValueSafe(array, new ComplexDebugInformation(basicDebugInformation,
                "array reference isn't safe"))

                //Check complexDebugInformation reference
                && ReferenceTools.IsValueSafe(complexDebugInformation, new ComplexDebugInformation(basicDebugInformation,
                "complexDebugInformation reference isn't safe")),

                //Return value
                isArraySafe = true;

            //Avoid null array
            if (areParametersSafe)
            {
                //Check length
                isArraySafe = array.Length > 0 ? true : false;

                //Avoid 0 size
                if (isArraySafe)
                {
                    //Itinerate array and check values
                    foreach (var value in array)
                        //Check value
                        if (!ReferenceTools.IsValueSafe(value, complexDebugInformation.AddCustomText("value on given array isn't safe")))
                        {
                            isArraySafe = false;
                            break;
                        }
                }
                //0 length arrays aren't safe
                else
                    DebugTools.DebugError(complexDebugInformation.AddCustomText("given array hasn't values"));
            }

            return isArraySafe;
        }

        //Check if a element is inside of given array
        // T = object target type
        public bool HasArrayObject<T>(T[] array, T value, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "HasArrayObject<T>(T[] array, T value, "
            + "ComplexDebugInformation complexDebugInformation");

            //Check parameters before execution
            bool areParametersSafe =
            //Check array reference
            IsArrayContentSafe(array, new ComplexDebugInformation(basicDebugInformation, "given array isn't safe"))

            //Check value and debug values
            && ReferenceTools.AreValuesSafe(new object[] { value, complexDebugInformation },
                new ComplexDebugInformation(basicDebugInformation, "given parameters aren't safe")),

            //Return value
            hasArrayObject = false;

            //Check array inst null
            if (areParametersSafe)
            {
                //Itinerate array and search for value on array
                foreach (T arrayValue in array)
                    if (arrayValue.Equals(arrayValue))
                        hasArrayObject = true;

                //Debug if doesnt find value
                if (!hasArrayObject)
                    DebugTools.DebugError(complexDebugInformation.AddTempCustomText("given array doesnt contains target value"));
            }

            return hasArrayObject;
        }

        //Check if a element is inside of given array
        // T = object target type
        public bool HasArrayObject<T>(T[] array, T value)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "HasArrayObject<T>(T[] array, T value)");

            //Check parameters before execution
            bool areParametersSafe =
            //Check array reference
            IsArrayContentSafe(array, new ComplexDebugInformation(basicDebugInformation, "given array isn't safe"))

            //Check value
            && ReferenceTools.IsValueSafe(value, new ComplexDebugInformation(basicDebugInformation, "given value isn't safe")),

            //Return value
            hasArrayObject = false;

            //Check array inst null
            if (areParametersSafe)
                //Itinerate array and search for value on array
                foreach (T arrayValue in array)
                    if (arrayValue.Equals(value))
                    {
                        hasArrayObject = true;
                        break;
                    }

            return hasArrayObject;
        }

        //Convert given array to given type and return value
        // T = original array type
        // T2 = final array type
        public bool ConvertArrayToType<T, T2>(T[] originalArray, out T2[] finalArray,
        ComplexDebugInformation complexDebugInformation)
        {
            //Set default value on out
            finalArray = default;

            //Debug information
            BasicDebugInformation basicDebugInformation =
            new BasicDebugInformation("CollectionsTools", "ConvertArrayToType<T, T2>(T[] originalArray, out T2[] finalArray," +
            "ComplexDebugInformation complexDebugInformation");

            //Check parameters values
            bool areParametersValid =
            //Check original array
            IsArrayContentSafe(originalArray, new ComplexDebugInformation(basicDebugInformation,
            "given array isn't safe"));

            //Continue execution if can
            if (areParametersValid)
            {
                //List to pass as array
                List<T2> tempFinalArrayList = new();

                //Itinerate original array and convert each value adding it to a list
                foreach (T value in originalArray)
                    //Try to convert
                    if (TypeTools.ConvertToType<T2>(value, out T2 newTypeValue,
                    complexDebugInformation.AddTempCustomText("couldnt convert value from given array")))
                        tempFinalArrayList.Add(newTypeValue);
                    //Break iteration
                    else
                    {
                        areParametersValid = false;
                        break;
                    }

                //Set out value with list 
                finalArray = tempFinalArrayList.ToArray();
            }

            //Return if could make conversion of values
            return areParametersValid;
        }

        //Add value to given array and return it
        //T = array type
        public bool AddValue<T>(T[] array, T value, out T[] outArray, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "AddValue<T>(T[] array, T value, ComplexDebugInformation complexDebugInformation)");

            //Set out value
            outArray = null;

            //Return value
            bool couldAdd = false;

            //Execute if parameters are correct
            if (ReferenceTools.AreValuesSafe(new object[] { array, value, complexDebugInformation },
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't safe")))
            {
                //Create list to store array
                List<T> list = new();

                //Local method, return new array to out
                T[] GetNewArray()
                {
                    //Array value to list
                    couldAdd = CollectionsTools.listTools.AddValue(list, value,
                    complexDebugInformation.AddTempCustomText("couldnt add value to temporal list"));

                    return list.ToArray();
                }

                //Check if array has content
                if (array.Length > 0)
                {
                    //Try to add array to list with current content
                    if (CollectionsTools.listTools.AddRange(list, array,
                        complexDebugInformation.AddTempCustomText("couldnt add array value to temporal list")))
                        outArray = GetNewArray();
                }
                //Add value
                else
                    outArray = GetNewArray();
            }

            return couldAdd;
        }
        #endregion
    }
}