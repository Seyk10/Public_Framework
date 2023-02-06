using System.Linq;
using System;
using System.Collections.Generic;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Collections
{
    //*Tool class used to generic collection solutions
    public static class CollectionsTools
    {
        //Objects with different executions stored
        public static readonly ListTools listTools = new();
        public static readonly ArrayTools arrayTools = new();
        public static readonly DictionaryTools dictionaryTools = new();

        //Check if given list contains any object with the type specified of type object and return it
        // T = list type
        public static bool ListContainsType<T>(List<T> list, Type targetType, out T listValue)
        {
            //Variables
            bool returnValue = false;
            listValue = default;

            //Itinerate list and check each type of objects
            foreach (var value in list)
                if (value.GetType().Equals(targetType))
                {
                    //Break iteration
                    listValue = value;
                    returnValue = true;
                    break;
                }

            return returnValue;
        }

        //Check if given list contains any object with the type specified and return it
        // T = List type
        // T2 = Target type
        public static bool ListContainsType<T, T2>(List<T> list, out T2 listValue)
        {
            //Variables
            bool returnValue = false;
            listValue = default;

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

            return returnValue;
        }

        //Remove objects of given type from list
        // T = list type
        public static bool ListRemoveType<T>(List<T> list, Type targetType)
        {
            //Variables
            bool returnValue = false;
            BasicDebugInformation basicDebugInformation =
            new BasicDebugInformation("CollectionsTools", "ListRemoveType<T>(List<T> list, Type targetType)");

            if (listTools.GetAuxiliaryList(list, out List<T> auxiliaryList,
            new ComplexDebugInformation(basicDebugInformation, "couldnt get auxiliary list from list")))
                //Itinerate list and remove types coincidences
                foreach (var value in auxiliaryList)
                    if (value.GetType().Equals(targetType)) list.Remove(value);

            return returnValue;
        }

        //Check if values inside dictionary are safe
        // T & T2 = dictionary types
        public static bool IsDictionarySafe<T, T2>(Dictionary<T, T2> dictionary, BasicDebugInformation basicDebugInformation)
        {
            bool areValuesSafe = true;

            ComplexDebugInformation complexDebugInformation =
            new ComplexDebugInformation(basicDebugInformation,
             "CollectionsTools, IsDictionarySafe<T, T2>(Dictionary<T, T2> dictionary)");

            //Check dictionary values
            if (ReferenceTools.IsValueSafe(dictionary))
                areValuesSafe = arrayTools.IsArrayContentSafe<T>(dictionary.Keys.ToArray(), complexDebugInformation)
                && arrayTools.IsArrayContentSafe<T2>(dictionary.Values.ToArray(), complexDebugInformation);
            else
                areValuesSafe = false;

            return areValuesSafe;
        }

        //Check if values inside dictionary are safe
        // T & T2 = dictionary types
        public static bool IsDictionarySafe<T, T2>(Dictionary<T, T2> dictionary)
        {
            bool areValuesSafe = true;

            //Check dictionary values
            if (ReferenceTools.IsValueSafe(dictionary))
                areValuesSafe = arrayTools.IsArraySafe<T>(dictionary.Keys.ToArray())
                && arrayTools.IsArraySafe<T2>(dictionary.Values.ToArray());
            else
                areValuesSafe = false;

            return areValuesSafe;
        }

        //Check if dictionary given key is safe
        // T =  Key type
        // T2 = Value type
        public static bool IsDictionaryKeySafe<T, T2>(Dictionary<T, T2> dictionary, T key,
        BasicDebugInformation basicDebugInformation)
        {
            //Checking value
            bool isKeySafe = IsDictionarySafe(dictionary, basicDebugInformation)
            && dictionary.ContainsKey(key);

#if UNITY_EDITOR
            //Check if list is safe
            if (!isKeySafe)
                DebugTools.DebugWarning(new ComplexDebugInformation(basicDebugInformation, "given key doesnt exists on dictionary"));
#endif

            return isKeySafe;
        }


        //Check if given index is safe
        // T = array list
        public static bool IsArrayIndexSafe<T>(T[] array, int index)
        {
            //Return value
            bool isIndexSafe = index >= 0 && index < array.Length;

#if UNITY_EDITOR
            if (!isIndexSafe)
                Debug.LogWarning("Warning: Given index isn't on array limits.");
#endif

            return isIndexSafe;
        }
    }
}