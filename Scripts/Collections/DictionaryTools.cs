using System.Collections.Generic;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Collections
{
    //* Executions related to dictionary operations
    public class DictionaryTools
    {
        #region NOT_SAFE_OPERATIONS
        //Method, check if dictionary values are safe
        private bool AreValuesSafe<T, T2>(Dictionary<T, T2> dictionary, T key, T2 value, BasicDebugInformation debugInformation)
        => ReferenceTools.IsValueSafe(dictionary, debugInformation)
        && ReferenceTools.IsValueSafe(key, debugInformation)
        && ReferenceTools.IsValueSafe(value, debugInformation);

        //Method, check if dictionary values are safe
        private bool AreValuesSafe<T, T2>(Dictionary<T, T2> dictionary, T2 value, BasicDebugInformation debugInformation)
        => ReferenceTools.IsValueSafe(dictionary, debugInformation)
        && ReferenceTools.IsValueSafe(value, debugInformation);

        //Method, check if dictionary values are safe
        private bool AreValuesSafe<T, T2>(Dictionary<T, T2> dictionary, T key, BasicDebugInformation debugInformation)
        => ReferenceTools.IsValueSafe(dictionary, debugInformation)
        && ReferenceTools.IsValueSafe(key, debugInformation);

        //Method, try get value checking if given references are valid
        //T = Key
        //T2 = Value
        public bool GetValue<T, T2>(Dictionary<T, T2> dictionary, T key, out T2 value, BasicDebugInformation debugInformation)
        {
            //Return value
            bool result = false;
            value = default;

            //Check if dictionary values are safe
            if (AreValuesSafe(dictionary, key, debugInformation))
            {
                //Check if can out value
                bool canOutValue = dictionary.ContainsKey(key)
                && ReferenceTools.IsValueSafe(dictionary[key], debugInformation);

                //Set out value
                if (canOutValue)
                {
                    value = dictionary[key];
                    result = true;
                }
            }

            return result;
        }

        //Method, safe key and value add to dictionary
        // T = Key, T2 = Value
        public bool AddValue<T, T2>(Dictionary<T, T2> dictionary, T key, T2 value, BasicDebugInformation debugInformation)
        {
            //Return value
            bool hasAdded = false;

            //Check reference
            if (AreValuesSafe(dictionary, key, value, debugInformation))
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

        //Method, safe key and value remove from dictionary
        // T = Key, T2 = Value
        //Safe key and value remove to dictionary
        // T = Key, T2 = Value
        public bool RemoveValue<T, T2>(Dictionary<T, T2> dictionary, T key, BasicDebugInformation debugInformation)
        {
            //Return value
            bool hasRemove = false;

            //Check reference
            if (AreValuesSafe(dictionary, key, debugInformation))
            {
                //Check if collection contains key
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                    hasRemove = true;
                }
            }

            return hasRemove;
        }

        //Method, safe key and value remove from dictionary
        // T = Key, T2 = Value
        //Safe key and value remove to dictionary
        // T = Key, T2 = Value
        public bool RemoveValue<T, T2>(Dictionary<T, T2> dictionary, T key)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "RemoveValue<T, T2>(Dictionary<T, T2> dictionary, T key)");

            //Return value
            bool hasRemove = false;

            //Check reference
            if (AreValuesSafe(dictionary, key, basicDebugInformation))
                //Check if collection contains key
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                    hasRemove = true;
                }

            return hasRemove;
        }
        #endregion
        #region SAFE_OPERATIONS
        //Method, check if dictionary values are safe
        private bool AreValuesSafe<T, T2>(Dictionary<T, T2> dictionary, T key, ComplexDebugInformation debugInformation)
        => ReferenceTools.IsValueSafe(dictionary, debugInformation.AddTempCustomText("given dictionary isn't safe"))
        && ReferenceTools.IsValueSafe(key, debugInformation.AddTempCustomText("given key isn't safe"));

        //Method, check if dictionary values are safe
        private bool AreValuesSafe<T, T2>(Dictionary<T, T2> dictionary, T key, T2 value, ComplexDebugInformation debugInformation)
        => ReferenceTools.IsValueSafe(dictionary, debugInformation.AddTempCustomText("dictionary isn't safe"))
        && ReferenceTools.IsValueSafe(key, debugInformation.AddTempCustomText("key isn't safe"))
        && ReferenceTools.IsValueSafe(value, debugInformation.AddTempCustomText("value isn't safe"));

        //Method, check if dictionary values are safe
        private bool AreValuesSafe<T, T2>(Dictionary<T, T2> dictionary, T2 value, ComplexDebugInformation debugInformation)
        => ReferenceTools.IsValueSafe(dictionary, debugInformation)
        && ReferenceTools.IsValueSafe(value, debugInformation);

        //Method, check if dictionary values are safe
        public bool AreValuesSafe<T, T2>(Dictionary<T, T2> dictionary, ComplexDebugInformation debugInformation)
        {
            //Return value
            bool canGetContent = GetAllKeys(dictionary, out List<T> keys, debugInformation.
            AddTempCustomText("couldnt get keys from dictionary"))
            && GetAllValues(dictionary, out List<T2> values, debugInformation.
            AddTempCustomText("couldnt get values from dictionary")),
            //Check arrays
            areArraysOkay = canGetContent
            && CollectionsTools.arrayTools.IsArrayContentSafe(keys.ToArray(), debugInformation.AddTempCustomText("keys aren't safe"))
            && CollectionsTools.arrayTools.IsArrayContentSafe(keys.ToArray(), debugInformation.AddTempCustomText("values aren't safe"));

            return areArraysOkay;
        }

        //Method, create and return an auxiliary dictionary with same values as given
        // T = Key
        // T2 = Value
        public bool GetAuxiliaryDictionary<T, T2>(Dictionary<T, T2> dictionary,
        out Dictionary<T, T2> auxiliaryDictionary, ComplexDebugInformation debugInformation)
        {
            //Check given values
            bool isDictionarySafe = ReferenceTools.IsValueSafe(dictionary, debugInformation),
            canCopyDictionary = isDictionarySafe && dictionary.Count > 0;

            //Out value
            auxiliaryDictionary = new();

            //Work with dictionary
            if (canCopyDictionary)
            {
                //Itinerate and add each value
                foreach (KeyValuePair<T, T2> pair in dictionary)
                {
                    //Check values to copy
                    bool canCopyPair = ReferenceTools.IsValueSafe(pair.Key, debugInformation.AddTempCustomText("invalid key"))
                     && ReferenceTools.IsValueSafe(pair.Value, debugInformation.AddTempCustomText("invalid value"));

                    //Copy value to auxiliary dictionary
                    if (canCopyPair)
                        auxiliaryDictionary.Add(pair.Key, pair.Value);
                    //Stop copy of dictionary
                    else
                    {
                        canCopyDictionary = false;
                        break;
                    }
                }
            }
#if UNITY_EDITOR
            //Debug information
            else if (dictionary.Count <= 0)
                DebugTools.DebugError(debugInformation.AddTempCustomText("tried to copy an empty dictionary."));
#endif

            return canCopyDictionary;
        }

        //Method, try get value checking if given references are valid
        public bool GetValue<T, T2>(Dictionary<T, T2> dictionary, T key, out T2 value, ComplexDebugInformation debugInformation)
        {
            //Return value
            bool result = false;
            value = default;

            //Check if dictionary values are safe
            if (AreValuesSafe(dictionary, key, debugInformation))
            {
                //Set out value
                if (dictionary.ContainsKey(key))
                {
                    //Check value
                    if (ReferenceTools.IsValueSafe(dictionary[key], debugInformation.AddCustomText("value to get inst safe")))
                    {
                        value = dictionary[key];
                        result = true;
                    }
#if UNITY_EDITOR
                    else
                        DebugTools.DebugError(debugInformation.AddCustomText("value to get inst safe"));
#endif
                }
#if UNITY_EDITOR
                else
                    DebugTools.DebugError(debugInformation.AddCustomText("dictionary doesnt contains key"));
#endif
            }

            return result;
        }

        //Method, safe key and value add to dictionary
        // T = Key, T2 = Value
        public bool AddValue<T, T2>(Dictionary<T, T2> dictionary, T key, T2 value, ComplexDebugInformation debugInformation)
        {
            //Return value
            bool hasAdded = false;

            //Check reference
            if (AreValuesSafe(dictionary, key, value, debugInformation))
            {
                //Check if collection contains key
                if (!dictionary.ContainsKey(key))
                {
                    hasAdded = true;
                    dictionary.Add(key, value);
                }
#if UNITY_EDITOR
                else
                    DebugTools.DebugError(debugInformation.AddCustomText("dictionary already contains key"));
#endif
            }

            return hasAdded;
        }

        // Method, add range of values
        // T = key , T2 = value
        public bool AddRange<T, T2>(Dictionary<T, T2> dictionary, T[] keys, T2[] values, ComplexDebugInformation debugInformation)
        {
            //Return variable
            bool areArraysSafe = CollectionsTools.arrayTools.IsArrayContentSafe(keys,
            debugInformation.AddTempCustomText("keys aren't safe to add"))
            && CollectionsTools.arrayTools.IsArrayContentSafe(values,
            debugInformation.AddTempCustomText("values aren't safe to add")),
            //Check array length
            isLengthSame = keys.Length == values.Length,
            //Can add content
            canAddContent = areArraysSafe && isLengthSame;

            //Add values to dictionary
            if (canAddContent)
                //Itinerate and add content
                for (int index = 0; index < keys.Length; index++)
                    dictionary.Add(keys[index], values[index]);
#if UNITY_EDITOR
            //Debug error information
            else if (!isLengthSame)
                DebugTools.DebugError(debugInformation.AddTempCustomText("given arrays length inst same"));
#endif

            return canAddContent;
        }

        //Method, try get all values from dictionary and return it as list
        //T = Key
        //T2 = Value
        public bool GetAllValues<T, T2>(Dictionary<T, T2> dictionary, out List<T2> values, ComplexDebugInformation debugInformation)
        {
            //Check given values
            bool isDictionarySafe = ReferenceTools.IsValueSafe(dictionary, debugInformation),
            canGetValues = isDictionarySafe && dictionary.Count > 0;

            //Out variable
            values = new();

            //Operate dictionary
            if (canGetValues)
            {
                //Create auxiliary dictionary
                canGetValues = GetAuxiliaryDictionary(dictionary, out Dictionary<T, T2> auxiliaryDictionary, debugInformation);

                //Itinerate values on dictionary and add to list
                if (canGetValues)
                    foreach (KeyValuePair<T, T2> pair in auxiliaryDictionary)
                        values.Add(pair.Value);
            }
#if UNITY_EDITOR
            else if (dictionary.Count <= 0)

                DebugTools.DebugError(debugInformation.AddTempCustomText("tried to copy an empty dictionary."));
#endif

            return canGetValues;
        }

        //Method, try get all keys from dictionary and return it as list
        //T = Key
        //T2 = Value
        public bool GetAllKeys<T, T2>(Dictionary<T, T2> dictionary, out List<T> values, ComplexDebugInformation debugInformation)
        {
            //Check given values
            bool isDictionarySafe = ReferenceTools.IsValueSafe(dictionary, debugInformation),
            canGetValues = isDictionarySafe && dictionary.Count > 0;

            //Out variable
            values = new();

            //Operate dictionary
            if (canGetValues)
            {
                //Create auxiliary dictionary
                canGetValues = GetAuxiliaryDictionary(dictionary, out Dictionary<T, T2> auxiliaryDictionary, debugInformation);

                //Itinerate values on dictionary and add to list
                if (canGetValues)
                    foreach (KeyValuePair<T, T2> pair in auxiliaryDictionary)
                        values.Add(pair.Key);
            }
#if UNITY_EDITOR
            else if (dictionary.Count <= 0)

                DebugTools.DebugError(debugInformation.AddTempCustomText("tried to copy an empty dictionary."));
#endif

            return canGetValues;
        }

        //Method, safe key and value remove from dictionary
        // T = Key, T2 = Value
        //Safe key and value remove to dictionary
        // T = Key, T2 = Value
        public bool RemoveValue<T, T2>(Dictionary<T, T2> dictionary, T key, ComplexDebugInformation debugInformation)
        {
            //Return value
            bool hasRemove = false;

            //Check reference
            if (AreValuesSafe(dictionary, key, debugInformation))
            {
                //Check if collection contains key
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                    hasRemove = true;
                }
#if UNITY_EDITOR
                else
                    DebugTools.DebugError(debugInformation.AddCustomText("given dictionary doesnt contain key"));
#endif
            }

            return hasRemove;
        }

        //Method, add dictionary to another dictionary
        // T = Key, T2 = Value
        //Safe key adds
        public bool MergeDictionaries<T, T2>(Dictionary<T, T2> dictionary01, Dictionary<T, T2> dictionary02,
        out Dictionary<T, T2> mergedDictionary, ComplexDebugInformation complexDebugInformation)
        {
            //Final value
            mergedDictionary = new();

            //Return value
            bool canMerge = false,
            areDictionariesSafe = AreValuesSafe(dictionary01, complexDebugInformation.AddTempCustomText("dictionary01 isn't safe"))
            && AreValuesSafe(dictionary02, complexDebugInformation.AddTempCustomText("dictionary02 isn't safe"));

            //Try to add ranges
            if (areDictionariesSafe)
            {
                //Get keys
                bool canGetKeys01 = GetAllKeys(dictionary01, out List<T> keys01,
                complexDebugInformation.AddTempCustomText("cant get keys on dictionary01")),
                canGetKeys02 = GetAllKeys(dictionary02, out List<T> keys02,
                complexDebugInformation.AddTempCustomText("cant get keys on dictionary02")),
                //Summarize
                canGetKeys = canGetKeys01 && canGetKeys02;

                //Check values
                if (canGetKeys)
                {
                    //Get values
                    bool canGetValues01 = GetAllValues(dictionary01, out List<T2> values01,
                    complexDebugInformation.AddTempCustomText("cant get values on dictionary01")),
                    canGetValues02 = GetAllValues(dictionary02, out List<T2> values02,
                    complexDebugInformation.AddTempCustomText("cant get values on dictionary02")),
                    //Summarize
                    canGetValues = canGetValues01 && canGetValues02;

                    //Set ranges
                    if (canGetValues)
                        canMerge = AddRange(mergedDictionary, keys01.ToArray(), values01.ToArray(),
                        complexDebugInformation.AddTempCustomText("cant add dictionary01 content"))
                        && AddRange(mergedDictionary, keys02.ToArray(), values02.ToArray(),
                        complexDebugInformation.AddTempCustomText("cant add dictionary02 content"));
                }
            }

            return canMerge;
        }
        #endregion
    }
}