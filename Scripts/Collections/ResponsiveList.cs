using System;
using System.Collections.Generic;

namespace MECS.Collections
{
    //* Class used to store a responsive list, raising events on each list modification
    //* T = List type
    public class ResponsiveList<T> : IDisposable, IResponsiveCollection<T>
    {
        //Variables
        public readonly List<T> list = new();

        #region EVENTS
        //IResponsiveCollection events
        public event EventHandler<T> ElementAddedEvent = null,
            ElementRemovedEvent = null,
            FirstElementAddedEvent = null,
            LastElementRemovedEvent = null;
        #endregion
        //Method, add element to list and notify it
        public bool AddElement(T value)
        {
            bool returnValue = CollectionsTools.listTools.AddValue(list, value);

            //Notify events
            if (returnValue)
            {
                //Notify first element added
                if (list.Count == 1)
                    FirstElementAddedEvent?.Invoke(this, value);

                //Notify element added
                ElementAddedEvent?.Invoke(this, value);
            }

            return returnValue;
        }

        //Method, remove element from list and notify it
        public bool RemoveElement(T value)
        {
            bool returnValue = CollectionsTools.listTools.RemoveValue(list, value);

            //Notify events
            if (returnValue)
            {
                //Notify last element removed
                if (list.Count == 0)
                    LastElementRemovedEvent?.Invoke(this, value);

                //Notify element removed
                ElementRemovedEvent?.Invoke(this, value);
            }

            return returnValue;
        }

        //IDisposable, clean all the lists
        public void Dispose() => list.Clear();

        //GC, make sure to call dispose
        ~ResponsiveList() => Dispose();
    }
}