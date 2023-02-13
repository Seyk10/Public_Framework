using System.Collections.Generic;
using System;

namespace MECS.Collections
{
    //* Class used to store a responsive dictionary, raising events on each modification
    //* T = Key type
    //* T2 = Value type
    public class ResponsiveDictionary<T, T2> : IDisposable, IResponsiveCollection<ResponsiveDictionaryArgs<T, T2>>
    {
        //Variables
        public readonly Dictionary<T, T2> dictionary = new();

        #region EVENTS
        //IResponsiveCollection events
        public event EventHandler<ResponsiveDictionaryArgs<T, T2>> ElementAddedEvent = null,
            ElementRemovedEvent = null,
            FirstElementAddedEvent = null,
            LastElementRemovedEvent = null;
        #endregion
        #region ADD_METHODS
        //Methods, add element to list and notify it, debug if couldnt add
        public bool AddElement(T key, T2 value, string debugMessage)
        {
            bool returnValue = CollectionsTools.dictionaryTools.AddValue(dictionary, key, value, debugMessage);

            //Notify if element is added
            if (returnValue)
            {
                //Notify first element
                if (dictionary.Count == 1)
                    FirstElementAddedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, value, debugMessage));

                //Notify element added
                ElementAddedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, value, debugMessage));
            }

            return returnValue;
        }

        //Method, add element to list and notify it
        public bool AddElement(T key, T2 value)
        {
            bool returnValue = CollectionsTools.dictionaryTools.AddValue(dictionary, key, value);

            //Notify if could add
            if (returnValue)
            {
                //Notify first element added
                if (dictionary.Count == 1)
                    FirstElementAddedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, value, " first element added"));

                //Notify element added
                ElementAddedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, value, " element added"));
            }

            return returnValue;
        }
        #endregion
        #region REMOVE_METHODS
        //Method, remove element from list and notify it, debug if couldnt remove
        public bool RemoveElement(T key, string debugMessage)
        {
            bool returnValue = CollectionsTools.dictionaryTools.RemoveValue(dictionary, key,
             debugMessage + " couldnt remove value from dictionary");

            //Notify if its the last element
            if (returnValue)
            {
                //Notify last element removed
                if (dictionary.Count == 0)
                    LastElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default, debugMessage));

                //Notify element removed
                ElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default, debugMessage));
            }

            return returnValue;
        }

        //Method, remove element from list and notify it
        public bool RemoveElement(T key)
        {
            bool returnValue = CollectionsTools.dictionaryTools.RemoveValue(dictionary, key);

            //Notify if its the last element
            if (returnValue)
            {
                //Notify last element removed
                if (dictionary.Count == 0)
                    LastElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default, " removed last element"));

                //Notify element removed
                ElementRemovedEvent?.Invoke(this, new ResponsiveDictionaryArgs<T, T2>(key, default, " element removed"));
            }

            return returnValue;
        }
        #endregion
        //IDisposable method, clean all the lists
        public void Dispose() => dictionary.Clear();

        //GC, make sure to call dispose
        ~ResponsiveDictionary() => Dispose();
    }
}