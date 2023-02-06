using System.Collections.Generic;
using System;
using static MECS.Tools.DebugTools;

namespace MECS.Collections
{
    //* Class used to store a responsive dictionary, raising events on each modification
    //* T = Key type
    //* T2 = Value type
    public class ResponsiveDictionary<T, T2> : IDisposable
    {
        //Variables
        public readonly Dictionary<T, T2> dictionary = new();

        //Events
        public event EventHandler<ResponsiveDictionaryArgs<T, T2>> ElementAddedEvent = null,
            ElementRemovedEvent = null,
            FirstElementAddedEvent = null,
            LastElementRemovedEvent = null;

        //Methods
        //Add element to list and notify it
        public bool AddElement(T key, T2 value, ComplexDebugInformation complexDebugInformation)
        {
            bool returnValue = CollectionsTools.dictionaryTools.AddValue(dictionary, key, value,
            new ComplexDebugInformation("ResponsiveDictionary<T, T2>", "AddElement(T key, T2 value)", "couldnt add value to dictionary"));

            //Notify if its the first element
            if (returnValue)
            {
                if (dictionary.Count == 1)
                    FirstElementAddedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, value, complexDebugInformation));
                ElementAddedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, value, complexDebugInformation));
            }

            return returnValue;
        }

        //Remove element from list and notify it
        public bool RemoveElement(T key, ComplexDebugInformation complexDebugInformation)
        {
            bool returnValue = CollectionsTools.dictionaryTools.RemoveValue(dictionary, key,
            complexDebugInformation.AddTempCustomText("couldnt remove value from dictionary"));

            //Notify if its the last element
            if (returnValue)
            {
                if (dictionary.Count == 0)
                    LastElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default, complexDebugInformation));
                ElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default, complexDebugInformation));
            }

            return returnValue;
        }

        //Remove element from list and notify it
        public bool RemoveElement(T key)
        {
            bool returnValue = CollectionsTools.dictionaryTools.RemoveValue(dictionary, key);

            //Notify if its the last element
            if (returnValue)
            {
                if (dictionary.Count == 0)
                    LastElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default));
                ElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default));
            }

            return returnValue;
        }

        //Check if list contains element
        public bool ContainsElement(T key) => dictionary.ContainsKey(key);

        //Clean all the lists
        public void Dispose() => dictionary.Clear();

        //Make sure to call dispose
        ~ResponsiveDictionary() => Dispose();
    }
}