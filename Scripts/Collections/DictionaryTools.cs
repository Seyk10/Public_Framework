using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using MECS.Conditionals;

namespace MECS.Collections
{
    //* Executions related to dictionary operations
    public class DictionaryTools
    {
        #region GET_VALUE_METHODS
        //Method, try get value checking if given references are valid and debug if not
        public bool GetValue<T, T2>(Dictionary<T, T2> dictionary, T key, out T2 value, string debugMessage)
        {
            //Return value
            bool result = false;
            value = default;

            //Check if dictionary values are safe
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { dictionary, key }, debugMessage
            + " given parameters aren't safe"))
                //Set out value
                if (dictionary.ContainsKey(key))
                {
                    //Check value
                    if (ReferenceTools.IsValueSafe(dictionary[key], debugMessage + " value to get inst safe"))
                    {
                        value = dictionary[key];
                        result = true;
                    }
                }
                //Notify debug manager
                else
                    new NotificationCommand<DebugArgs>(this, new DebugArgs(debugMessage, LogType.Error, new StackTrace(true)))
                    .Execute();

            return result;
        }

        //Method, try get value checking if given references are valid
        //T = Key
        //T2 = Value
        public bool GetValue<T, T2>(Dictionary<T, T2> dictionary, T key, out T2 value)
        {
            //Return value
            bool result = false;
            value = default;

            //Check if dictionary values are safe
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { dictionary, key }, " given parameters aren't safe"))
            {
                //Check if can out value
                bool canOutValue = dictionary.ContainsKey(key)
                && ReferenceTools.IsValueSafe(dictionary[key], " target value to get isn't safe");

                //Set out value
                if (canOutValue)
                {
                    value = dictionary[key];
                    result = true;
                }
            }

            return result;
        }
        #endregion
        #region ADD_VALUE_METHODS
        //Method, safe key and value add to dictionary
        // T = Key, T2 = Value
        public bool AddValue<T, T2>(Dictionary<T, T2> dictionary, T key, T2 value)
        {
            //Return value
            bool hasAdded = false;

            //Check reference
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { dictionary, key, value }, " given parameters aren't safe"))
            {
                //Check if collection contains key
                if (!dictionary.ContainsKey(key))
                {
                    hasAdded = true;
                    dictionary.Add(key, value);
                }
            }

            return hasAdded;
        }

        //Method, safe key and value add to dictionary and debug if couldnt 
        // T = Key, T2 = Value
        public bool AddValue<T, T2>(Dictionary<T, T2> dictionary, T key, T2 value, string debugMessage)
        {
            //Return value
            bool hasAdded = false;

            //Check reference
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { dictionary, key, value }, debugMessage
            + " given parameters aren't safe"))
            {
                //Check if collection contains key
                if (!dictionary.ContainsKey(key))
                {
                    hasAdded = true;
                    dictionary.Add(key, value);
                }
                //Notify debug manager
                else
                    new NotificationCommand<DebugArgs>(this, new DebugArgs(debugMessage, LogType.Error, new StackTrace(true)))
                    .Execute();
            }

            return hasAdded;
        }

        // Method, add range of values
        // T = key , T2 = value
        public bool AddRange<T, T2>(Dictionary<T, T2> dictionary, T[] keys, T2[] values, string debugMessage)
        {
            //Check if given parameters are valid
            bool areParametersSafe = CollectionsTools.arrayTools
            .IsArrayContentSafe(new object[] { dictionary, keys, values }, debugMessage + " given parameters aren't safe"),

            //Check arrays content
            areArraysSafe =
                //Keys array
                CollectionsTools.arrayTools.IsArrayContentSafe(keys,
                debugMessage + "keys aren't safe to add")

                //Values array
                && CollectionsTools.arrayTools.IsArrayContentSafe(values,
                debugMessage + "values aren't safe to add"),

            //Check arrays length
            isLengthSame = NumericTools.IsComparativeCorrect(keys.Length, values.Length, Conditionals.ENumericConditional.Equal,
            debugMessage + " key and values arrays length isn't equal"),

            //Can add content
            canAddContent = areParametersSafe && areArraysSafe && isLengthSame;

            //Check if dictionary already contains any key
            if (canAddContent)
            {
                foreach (var key in keys)
                    //Check if contains key
                    if (dictionary.ContainsKey(key))
                    {
                        canAddContent = false;
                        break;
                    }
            }

            //Add values to dictionary
            if (canAddContent)
                //Itinerate and add content
                for (int index = 0; index < keys.Length; index++)
                    dictionary.Add(keys[index], values[index]);

            return canAddContent;
        }
        #endregion
        #region REMOVE_VALUE_METHODS
        //Method, safe key and value remove from dictionary, debug if couldnt
        // T = Key, T2 = Value
        //Safe key and value remove to dictionary
        // T = Key, T2 = Value
        public bool RemoveValue<T, T2>(Dictionary<T, T2> dictionary, T key, string debugMessage)
        {
            //Return value
            bool hasRemove = false;

            //Check reference
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { dictionary, key },
            debugMessage + " given parameters aren't safe"))
                //Check if collection contains key
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                    hasRemove = true;
                }
                //Notify debug manager if dictionary does not contain key
                else
                    new NotificationCommand<DebugArgs>(this, new DebugArgs(debugMessage + " given dictionary doesnt contain key",
                    LogType.Error, new StackTrace(true))).Execute();

            return hasRemove;
        }

        //Method, safe key and value remove from dictionary
        // T = Key, T2 = Value
        //Safe key and value remove to dictionary
        // T = Key, T2 = Value
        public bool RemoveValue<T, T2>(Dictionary<T, T2> dictionary, T key)
        {
            //Return value
            bool hasRemove = false;

            //Check reference
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { dictionary, key },
            " given parameters aren't safe"))
                //Check if collection contains key
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                    hasRemove = true;
                }

            return hasRemove;
        }
        #endregion
        #region GET_CONTENT_METHODS
        //Method, try get all values from dictionary and return it as list
        //T = Key
        //T2 = Value
        public bool GetAllValues<T, T2>(Dictionary<T, T2> dictionary, out List<T2> values, string debugMessage)
        {
            //Check given values
            bool isDictionarySafe = ReferenceTools.IsValueSafe(dictionary, debugMessage + " given dictionary isn't safe"),

            //Check dictionary content
            canGetValues = isDictionarySafe

                //Check count
                && NumericTools.IsComparativeCorrect(dictionary.Count, 0, ENumericConditional.Bigger,
                debugMessage + " given dictionary is empty");

            //Out variable
            values = new();

            //Operate dictionary
            if (canGetValues)
                //Itinerate values on dictionary and add to list
                if (GetAuxiliaryDictionary(dictionary, out Dictionary<T, T2> auxiliaryDictionary,
                debugMessage + " couldnt get auxiliary dictionary"))
                {
                    foreach (KeyValuePair<T, T2> pair in auxiliaryDictionary)
                        values.Add(pair.Value);
                }
                else
                    canGetValues = false;

            return canGetValues;
        }

        //Method, try get all keys from dictionary and return it as list
        //T = Key
        //T2 = Value
        public bool GetAllKeys<T, T2>(Dictionary<T, T2> dictionary, out List<T> values, string debugMessage)
        {
            //Check parameters
            bool isDictionarySafe = ReferenceTools.IsValueSafe(dictionary, debugMessage + " given dictionary isn't safe"),

            //Check dictionary count
            canGetValues = isDictionarySafe

            //Check dictionary content
            && NumericTools.IsComparativeCorrect(dictionary.Count, 0, Conditionals.ENumericConditional.Bigger,
            debugMessage + " given dictionary has no content");

            //Out variable
            values = new();

            //Operate dictionary
            if (canGetValues)
                //Itinerate values on dictionary and add to list
                if (GetAuxiliaryDictionary(dictionary, out Dictionary<T, T2> auxiliaryDictionary,
                debugMessage + " couldnt create auxiliary dictionary"))
                {
                    foreach (KeyValuePair<T, T2> pair in auxiliaryDictionary)
                        values.Add(pair.Key);
                }
                else
                    canGetValues = false;

            return canGetValues;
        }
        #endregion
        //Method, add dictionary to another dictionary
        // T = Key, T2 = Value
        //Safe key adds
        public bool MergeDictionaries<T, T2>(Dictionary<T, T2> dictionary01, Dictionary<T, T2> dictionary02,
        out Dictionary<T, T2> mergedDictionary, string debugMessage)
        {
            //Final value
            mergedDictionary = new();

            //Return value
            bool canMerge = false,

            //Check parameters
            areDictionariesSafe =
                //Check first dictionary
                IsDictionaryContentSafe(dictionary01, debugMessage + " dictionary01 isn't safe")

                //Check second dictionary
                && IsDictionaryContentSafe(dictionary02, debugMessage + " dictionary02 isn't safe");

            //Try to add ranges
            if (areDictionariesSafe)
            {
                //Get keys on dictionary_01
                bool canGetKeys01 = GetAllKeys(dictionary01, out List<T> keys01,
                debugMessage + " cant get keys on dictionary01"),

                //Get keys on dictionary_02
                canGetKeys02 = GetAllKeys(dictionary02, out List<T> keys02,
                debugMessage + " cant get keys on dictionary02"),

                //Summarize
                canGetKeys = canGetKeys01 && canGetKeys02;

                //Check values
                if (canGetKeys)
                {
                    //Get values on dictionary_01
                    bool canGetValues01 = GetAllValues(dictionary01, out List<T2> values01,
                    debugMessage + " cant get values on dictionary01"),

                    //Get values on dictionary_02
                    canGetValues02 = GetAllValues(dictionary02, out List<T2> values02,
                    debugMessage + " cant get values on dictionary02"),

                    //Summarize
                    canGetValues = canGetValues01 && canGetValues02;

                    //Set ranges
                    if (canGetValues)
                        canMerge =
                        //Add dictionary_01 content
                        AddRange(mergedDictionary, keys01.ToArray(), values01.ToArray(),
                        debugMessage + " cant add dictionary01 content")

                        //Add dictionary_02 content
                        && AddRange(mergedDictionary, keys02.ToArray(), values02.ToArray(),
                        debugMessage + " cant add dictionary02 content");
                }
            }

            return canMerge;
        }

        //Method, check if dictionary values are safe
        //T = key
        //T2 = value
        public bool IsDictionaryContentSafe<T, T2>(Dictionary<T, T2> dictionary, string debugMessage)
        {
            //Check parameters
            bool isParameterSafe = ReferenceTools.IsValueSafe(dictionary, debugMessage + " given dictionary isn't safe"),

            //Check dictionary count
            hasContent = isParameterSafe

            && NumericTools.IsComparativeCorrect(dictionary.Count, 0, Conditionals.ENumericConditional.Bigger,
            debugMessage + " given dictionary is empty"),

            //Check arrays content
            areArraysOkay = hasContent
                //Check keys content
                && CollectionsTools.arrayTools.IsArrayContentSafe<T>(dictionary.Keys.ToArray(), debugMessage + "keys aren't safe")

                //Check values content
                && CollectionsTools.arrayTools.IsArrayContentSafe<T2>(dictionary.Values.ToArray(), debugMessage + " values aren't safe");

            return areArraysOkay;
        }

        //Method, create and return an auxiliary dictionary with same values as given
        // T = Key
        // T2 = Value
        public bool GetAuxiliaryDictionary<T, T2>(Dictionary<T, T2> dictionary,
        out Dictionary<T, T2> auxiliaryDictionary, string debugMessage)
        {
            //Check parameters
            bool isDictionarySafe = ReferenceTools.IsValueSafe(dictionary, debugMessage + " given dictionary isn't safe"),

            //Check dictionary content
            canCopyDictionary = isDictionarySafe

            //Check content
            && IsDictionaryContentSafe(dictionary, debugMessage + " given dictionary content isn't safe");

            //Out value
            auxiliaryDictionary = new();

            //Work with dictionary
            if (canCopyDictionary)
                //Itinerate and add each value
                foreach (KeyValuePair<T, T2> pair in dictionary)
                    auxiliaryDictionary.Add(pair.Key, pair.Value);

            return canCopyDictionary;
        }
    }
}