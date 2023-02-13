using System.Diagnostics;
using System.Collections.Generic;
using MECS.Patrons.Commands;
using MECS.Tools;
using System;
using UnityEngine;

namespace MECS.Collections
{
    //* Executions related to list operations
    public class ListTools
    {
        //Methods, add object to list and debug if couldnt add
        // T = List type and object
        public bool AddValue<T>(List<T> list, T value, string debugMessage = null, LogType logType = LogType.Error)
        {
            //Return if could add
            bool added = false;

            //Avoid parameters error
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { list, value }, debugMessage
            + "given parameters aren't safe"))
                //Check if collection doesn't contain value
                if (!list.Contains(value))
                {
                    //Add value
                    list.Add(value);
                    added = true;
                }
                //Notify debug manager
                else if (ReferenceTools.IsValueSafe(debugMessage))
                    new NotificationCommand<DebugArgs>(this, new DebugArgs(debugMessage + " couldnt add value to given list, "
                    + "it already contains value", logType, new StackTrace(true))).Execute();

            return added;
        }

        //TODO: Dont add directly
        //Method, safe add range to list and debug if couldnt add
        //T = List type
        public bool AddRange<T>(List<T> list, T[] valueRange, string debugMessage = null, LogType logType = LogType.Error)
        {
            //Return if could add
            bool canAdd =
                //Check parameters references
                ReferenceTools.IsValueSafe(list, " given list isn't safe")

                //Check array
                && CollectionsTools.arrayTools.IsArrayContentSafe(valueRange, " given array reference isn't safe");

            //Check collection reference
            if (canAdd)
            {
                //Itinerate range values
                foreach (T value in valueRange)
                    if (!AddValue(list, value, debugMessage))
                    {
                        canAdd = false;
                        break;
                    }
            }

            //Debug if its necessary
            if (ReferenceTools.IsValueSafe(debugMessage))
                new NotificationCommand<DebugArgs>(this,
                new DebugArgs(debugMessage, logType, new StackTrace(true))).Execute();

            return canAdd;
        }

        //Method, generate auxiliary list to avoid errors iterations
        // T = List type and object
        public bool GetAuxiliaryList<T>(List<T> list, out List<T> auxiliaryList, string debugMessage)
        {
            //Check parameters
            bool areParametersSafe =
                //Check list reference
                ReferenceTools.IsValueSafe(list, " given list isn't safe")

                //Check list
                && CollectionsTools.arrayTools.IsArrayContentSafe(list.ToArray(), " given list content isn't safe"),

            //Return if could create auxiliary list
            returnValue = true;

            //Return list
            auxiliaryList = new List<T>();

            //Check reference inst empty
            if (areParametersSafe)
                //Itinerate and copy the list
                foreach (T item in list)
                    if (!AddValue(auxiliaryList, item, debugMessage))
                    {
                        returnValue = false;
                        break;
                    }

            return returnValue;
        }

        #region REMOVE_VALUE_METHODS
        //TODO: ADD LOG TYPE ON METHODS
        //Remove objects of given type from list
        // T = list type
        public bool ListRemoveType<T>(List<T> list, Type targetType, string debugMessage = null)
        {
            //Return value
            bool returnValue = false;

            //Check parameters
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { list, targetType }, " given parameters aren't safe"))
                if (GetAuxiliaryList(list, out List<T> auxiliaryList, " couldnt get auxiliary list from list"))
                {
                    //Itinerate list and remove types coincidences
                    foreach (var value in auxiliaryList)
                        if (value.GetType().Equals(targetType))
                        {
                            list.Remove(value);
                            returnValue = true;
                        }

                    //Debug if couldnt remove
                    if (ReferenceTools.IsValueSafe(debugMessage))
                        new NotificationCommand<DebugArgs>(this,
                        new DebugArgs(debugMessage, UnityEngine.LogType.Error, new StackTrace(true))).Execute();
                }

            return returnValue;
        }

        //Safe object remove from list and debug if couldnt remove
        // T = List type and object
        public bool RemoveValue<T>(List<T> list, T value, string debugMessage = null)
        {
            //Return if could remove
            bool removed = false;

            //Check collection reference
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { list, value }, debugMessage
            + " given parameters aren't safe"))
                //Check if collection contain value
                if (list.Contains(value))
                {
                    list.Remove(value);
                    removed = true;
                }
                //Notify debug manager
                else if (ReferenceTools.IsValueSafe(debugMessage))
                    new NotificationCommand<DebugArgs>(this, new DebugArgs(debugMessage, UnityEngine.LogType.Error,
                    new StackTrace(true))).Execute();

            return removed;
        }
        #endregion
        #region TYPE_CHECKING_METHODS
        //Check if given list contains any object with the type specified of type object and return it
        // T = list type
        public bool ListContainsType<T>(List<T> list, Type targetType, out T listValue, string debugMessage = null)
        {
            //Variables
            bool returnValue = false;
            listValue = default;

            //Check parameters
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { list, targetType }, debugMessage))
                //Get auxiliary list
                if (GetAuxiliaryList(list, out List<T> auxiliaryList, debugMessage))
                {
                    //Itinerate list and check each type of objects
                    foreach (var value in list)
                        if (value.GetType().Equals(targetType))
                        {
                            //Break iteration
                            listValue = value;
                            returnValue = true;
                            break;
                        }

                    //Debug if its necessary
                    if (ReferenceTools.IsValueSafe(debugMessage))
                        new NotificationCommand<DebugArgs>(this,
                        new DebugArgs(debugMessage, LogType.Error, new StackTrace(true))).Execute();
                }


            return returnValue;
        }

        //Check if given list contains any object with the type specified and return it
        // T = List type
        // T2 = Target type
        public bool ListContainsType<T, T2>(List<T> list, out T2 listValue, string debugMessage = null)
        {
            //Variables
            bool returnValue = false;
            listValue = default;

            //Check parameters
            if (ReferenceTools.IsValueSafe(list, " given list isn't safe"))
                //Duplicate list to avoid modifications
                if (GetAuxiliaryList(list, out List<T> auxiliaryList, " couldnt get auxiliary list"))
                {
                    //Itinerate and check types
                    foreach (T value in list)
                        //Convert to type if its correct
                        if (value is T2 targetType)
                        {
                            //Change return value and break
                            listValue = targetType;
                            returnValue = true;
                            break;
                        }

                    //Debug if its necessary
                    if (ReferenceTools.IsValueSafe(debugMessage))
                        new NotificationCommand<DebugArgs>(this,
                        new DebugArgs(debugMessage, LogType.Error, new StackTrace(true))).Execute();
                }

            return returnValue;
        }
        #endregion
    }
}