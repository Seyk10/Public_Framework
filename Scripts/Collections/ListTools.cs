using System.Collections.Generic;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Collections
{
    //* Executions related to list operations
    public class ListTools
    {
        #region ADD_VALUE_METHOD
        //Methods, and object to list
        // T = List type and object
        public bool AddValue<T>(List<T> list, T value)
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AddValue<T>(List<T> list, T value)");

            //Return if could add
            bool added = false;

            //Check collection reference
            if (ReferenceTools.AreValuesSafe(new object[] { list, value },
                new ComplexDebugInformation(basicDebugInformation, "given parameters aren't safe")))
                //Check if collection doesn't contain value
                if (!list.Contains(value))
                {
                    list.Add(value);
                    added = true;
                }

            return added;
        }

        //Methods, add object to list and debug if couldnt add
        // T = List type and object
        public bool AddValue<T>(List<T> list, T value, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "AddValue<T>(List<T> list, T value, ComplexDebugInformation debugInformation)");

            //Return if could add
            bool added = false;

            //Avoid parameters error
            if (ReferenceTools.AreValuesSafe(new object[] { list, value, complexDebugInformation },
                new ComplexDebugInformation(basicDebugInformation, "given parameters aren't safe")))
                //Check if collection doesn't contain value
                if (!list.Contains(value))
                {
                    //Add value
                    list.Add(value);
                    added = true;
                }
                else
                    DebugTools.DebugError(complexDebugInformation.AddCustomText("list already contains value"));

            return added;
        }
        #endregion
        #region ADD_RANGE_METHOD
        //Method, safe add range to list
        //T = List type
        public bool AddRange<T>(List<T> list, T[] valueRange)
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AddRange<T>(List<T> list, T[] valueRange)");

            //Check parameters
            bool areParametersSafe =
                //Check parameters references
                ReferenceTools.IsValueSafe(list,
                new ComplexDebugInformation(basicDebugInformation, "given list isn't safe"))

                //Check array
                && CollectionsTools.arrayTools.IsArrayContentSafe(valueRange,
                new ComplexDebugInformation(basicDebugInformation, "given array isn't safe")),

            //Return if could add
            added = true;

            //Set values if can try
            if (areParametersSafe)
            {
                //Itinerate range values
                foreach (T value in valueRange)
                    //Add each value
                    if (!AddValue(list, value))
                    {
                        added = false;
                        break;
                    }
            }
            else
                added = false;

            return added;
        }

        //Method, safe add range to list and debug if couldnt add
        //T = List type
        public bool AddRange<T>(List<T> list, T[] valueRange, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "AddRange<T>(List<T> list, T[] valueRange, ComplexDebugInformation debugInformation)");

            //Check parameters
            bool areParametersSafe =
                //Check list
                IsListContentSafe(list, new ComplexDebugInformation(basicDebugInformation, "given list isn't safe"))

                //Check debug information
                && ReferenceTools.IsValueSafe(complexDebugInformation,
                new ComplexDebugInformation(basicDebugInformation, "given complexDebugInformation isn't safe"))

                //Check array
                && CollectionsTools.arrayTools.IsArrayContentSafe(valueRange, new ComplexDebugInformation(basicDebugInformation,
                "given array reference isn't safe")),

            //Return if could add
            added = true;

            //Check collection reference
            if (areParametersSafe)
            {
                //Itinerate range values
                foreach (T value in valueRange)
                    if (!AddValue(list, value, complexDebugInformation.AddTempCustomText("couldnt add value to given list")))
                    {
                        added = false;
                        break;
                    }
            }
            else
            {
                added = false;
                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation, "couldnt add value to given list"));
            }

            return added;
        }
        #endregion

        //Method, generate auxiliary list to avoid errors iterations
        // T = List type and object
        public bool GetAuxiliaryList<T>(List<T> list, out List<T> auxiliaryList, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "GetAuxiliaryList<T>(List<T> list, out List<T> auxiliaryList)");

            //Check parameters
            bool areParametersSafe =
                //Check list reference
                ReferenceTools.IsValueSafe(complexDebugInformation,
                new ComplexDebugInformation(basicDebugInformation, "given complexDebugInformation isn't safe"))

                //Check list
                && IsListContentSafe(list,
                new ComplexDebugInformation(basicDebugInformation, "given list isn't safe")),

            //Return if could create auxiliary list
            returnValue = true;

            //Return list
            auxiliaryList = new List<T>();

            //Check reference inst empty
            if (areParametersSafe)
                //Itinerate and copy the list
                foreach (T item in list)
                    if (!AddValue(auxiliaryList, item, complexDebugInformation
                    .AddTempCustomText("couldnt add value to auxiliary list")))
                    {
                        returnValue = false;
                        break;
                    }

            return returnValue;
        }

        #region REMOVE_VALUE_METHOD
        //Safe object remove from list
        // T = List type and object
        public bool RemoveValue<T>(List<T> list, T value)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "RemoveValue<T>(List<T> list, T value)");

            //Return if could remove
            bool removed = false;

            //Check collection reference
            if (ReferenceTools.AreValuesSafe(new object[] { list, value },
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't safe")))
                //Check if collection contain value
                if (list.Contains(value))
                {
                    list.Remove(value);
                    removed = true;
                }

            return removed;
        }

        //Safe object remove from list
        // T = List type and object
        public bool RemoveValue<T>(List<T> list, T value, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "(List<T> list, T value, ComplexDebugInformation debugInformation)");

            //Return if could remove
            bool removed = false;

            //Check collection reference
            if (ReferenceTools.AreValuesSafe(new object[] { list, value, complexDebugInformation },
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't safe")))
                //Check if collection contain value
                if (list.Contains(value))
                {
                    list.Remove(value);
                    removed = true;
                }
                else
                    DebugTools.DebugError(complexDebugInformation.AddCustomText("given list doesnt contains value to remove"));

            return removed;
        }
        #endregion

        //Check if values inside the list and list are correct or valid
        // T = list type
        public bool IsListContentSafe<T>(List<T> list, ComplexDebugInformation complexDebugInformation) =>
            //Check list reference
            ReferenceTools.IsValueSafe(list, complexDebugInformation.AddTempCustomText("list reference isn't safe"))

            //Check list values
            && CollectionsTools.arrayTools.IsArrayContentSafe<T>(list.ToArray(),
            complexDebugInformation.AddTempCustomText("given list reference isn't valid"));

    }
}