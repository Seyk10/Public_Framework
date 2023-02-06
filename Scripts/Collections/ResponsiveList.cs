using System;
using System.Collections.Generic;
using static MECS.Tools.DebugTools;

namespace MECS.Collections
{
    //* Class used to store a responsive list, raising events on each list modification
    //* T = List type
    public class ResponsiveList<T> : IDisposable
    {
        //Variables
        private readonly List<T> list = new();

        public T[] ListToArray => list.ToArray();

        //Events
        public event EventHandler<T> ElementAddedEvent = null,
            ElementRemovedEvent = null,
            FirstElementAddedEvent = null,
            LastElementRemovedEvent = null;

        //Methods
        //Add element to list and notify it
        public bool AddElement(T value)
        {
            bool returnValue = CollectionsTools.listTools
            .AddValue(list, value, new ComplexDebugInformation("ResponsiveList<T>", "AddElement(T value)", "couldnt add value to list"));

            //Notify if its the first element
            if (returnValue)
            {
                if (list.Count == 1)
                    FirstElementAddedEvent?.Invoke(this, value);
                ElementAddedEvent?.Invoke(this, value);
            }

            return returnValue;
        }

        //Remove element from list and notify it
        public bool RemoveElement(T value)
        {
            bool returnValue = CollectionsTools.listTools
            .RemoveValue(list, value,
            new ComplexDebugInformation("ResponsiveList<T>", "RemoveElement(T value)", "couldnt remove value from list"));

            //Notify if its the last element
            if (returnValue)
            {
                if (list.Count == 0)
                    LastElementRemovedEvent?.Invoke(this, value);
                ElementRemovedEvent?.Invoke(this, value);
            }

            return returnValue;
        }

        //Check if list contains element
        public bool ContainsElement(T value) => list.Contains(value);

        //Clean all the lists
        public void Dispose()
        {
            //Create auxiliary copy
            T[] auxiliaryArray = list.ToArray();

            //Itinerate each element
            foreach (T value in auxiliaryArray)
                CollectionsTools.listTools.RemoveValue(list, value,
                new ComplexDebugInformation("ResponsiveList<T>", "Dispose()", "couldnt remove value from list"));
        }

        //Make sure to call dispose
        ~ResponsiveList() => Dispose();
    }
}