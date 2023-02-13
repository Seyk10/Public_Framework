using System.Collections.Generic;
using System.Diagnostics;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Collections
{
    //* Executions related to array operations
    public class ArrayTools
    {
        #region ARRAY_CONTENT_ITERATION
        //Method, check if values inside the array and array are correct or valid and notify errors
        // T = array type
        public bool IsArrayContentSafe<T>(T[] array, string debugMessage)
        {
            //Value to return
            bool isArraySafe = true;

            //Avoid null array
            if (ReferenceTools.IsValueSafe(array, debugMessage + " given array isn't safe"))
            {
                //Check length
                isArraySafe = array.Length > 0 ? true : false;

                //Avoid 0 size
                if (isArraySafe)
                {
                    //Itinerate array and check values
                    foreach (var value in array)
                        //Check value
                        if (!ReferenceTools.IsValueSafe(value, debugMessage + " value on array isn't safe"))
                        {
                            isArraySafe = false;
                            break;
                        }
                }
                //0 length arrays aren't safe, notify debug manager
                else
                    new NotificationCommand<DebugArgs>(this,
                        new DebugArgs(debugMessage + " given array have 0 values", LogType.Error, new StackTrace(true))).Execute();
            }

            return isArraySafe;
        }

        //Method, check if values inside the array and array are correct or valid
        // T = array type
        public bool IsArrayContentSafe<T>(T[] array)
        {
            //Value to return
            bool isArraySafe = true;

            //Avoid null array
            if (ReferenceTools.IsValueSafe(array, " given array isn't safe"))
            {
                //Check length
                isArraySafe = array.Length > 0 ? true : false;

                //Avoid 0 size
                if (isArraySafe)
                {
                    //Itinerate array and check values
                    foreach (var value in array)
                        //Check value
                        if (!ReferenceTools.IsValueSafe(value))
                        {
                            isArraySafe = false;
                            break;
                        }
                }
            }

            return isArraySafe;
        }

        //Method, check if a element is inside of given array and debug if not
        // T = object target type
        public bool HasArrayObject<T>(T[] array, T value, string debugMessage)
        {
            //Check parameters before execution
            bool areParametersSafe =
            //Check array reference
            IsArrayContentSafe(array, debugMessage + " given array isn't safe")

            //Check value and debug values
            && ReferenceTools.IsValueSafe(value, debugMessage + " given value to search isn't safe"),

            //Return value
            hasArrayObject = false;

            //Check array inst null
            if (areParametersSafe)
            {
                //Itinerate array and search for value on array
                foreach (T arrayValue in array)
                    if (arrayValue.Equals(arrayValue))
                        hasArrayObject = true;

                //Notify debug manager
                if (!hasArrayObject)
                    new NotificationCommand<DebugArgs>(this,
                    new DebugArgs(debugMessage + " given array doesnt contains target value", LogType.Error, new StackTrace(true)))
                    .Execute();
            }

            return hasArrayObject;
        }

        //Method, check if a element is inside of given array
        // T = object target type
        public bool HasArrayObject<T>(T[] array, T value)
        {
            //Check parameters before execution
            bool areParametersSafe =
            //Check array reference
            IsArrayContentSafe(array, " given array content to itinerate isn't safe")

            //Check value
            && ReferenceTools.IsValueSafe(value, " given value to check on array isn't safe"),

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
        #endregion

        //Method, convert given array to given type and return value
        // T = original array type
        // T2 = final array type
        public bool ConvertArrayToType<T, T2>(T[] originalArray, out T2[] finalArray, string debugMessage)
        {
            //Set default value on out
            finalArray = default;

            //Check parameters values
            bool areParametersValid =
            //Check original array
            IsArrayContentSafe(originalArray, debugMessage + " given array to convert isn't safe");

            //Continue execution if can
            if (areParametersValid)
            {
                //List to pass as array
                List<T2> tempFinalArrayList = new();

                //Itinerate original array and convert each value adding it to a list
                foreach (T value in originalArray)
                    //Try to convert
                    if (TypeTools.ConvertToType<T2>(value, out T2 newTypeValue,
                    debugMessage + "couldnt convert value from given array"))
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

        //Method, add value to given array and return it
        //T = array type
        public bool AddValue<T>(T[] array, T value, out T[] outArray, string debugMessage)
        {
            //Set out value
            outArray = null;

            //Return value
            bool couldAdd = false;

            //Execute if parameters are correct
            if (IsArrayContentSafe(new object[] { array, value }, debugMessage + " given parameters aren't safe"))
            {
                //Create list to store array
                List<T> list = new();

                //Local method, return new array to out
                T[] GetNewArray()
                {
                    //Array value to list
                    couldAdd = CollectionsTools.listTools.AddValue(list, value,
                        debugMessage + " couldnt add value to temporal list");

                    return list.ToArray();
                }

                //Check if array has content
                if (array.Length > 0)
                {
                    //Try to add array to list with current content
                    if (CollectionsTools.listTools.AddRange(list, array,
                        debugMessage + " couldnt add array range value to temporal list"))
                        outArray = GetNewArray();
                }
                //Add value
                else
                    outArray = GetNewArray();
            }

            return couldAdd;
        }
    }
}