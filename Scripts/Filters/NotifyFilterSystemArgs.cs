using System;
using MECS.Collections;
using MECS.Events;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Filters
{
    //* Share values to check with filters
    public class NotifyFilterSystemArgs : AEventArgs
    {
        //Variables
        public readonly GameObject entityToCheck = null;
        public readonly IFilterData[] iFilterDataArray = null;

        //Default builder
        public NotifyFilterSystemArgs(GameObject entityToCheck, IFilterData[] iFilterDataArray,
        ComplexDebugInformation complexDebugInformation) : base(complexDebugInformation)
        {
            this.entityToCheck = entityToCheck;
            this.iFilterDataArray = iFilterDataArray;
        }

        //AEventArgs method, check if given values on args are valid
        public override bool AreValuesValid()
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AreValuesValid()");

            //Check entity to check
            return ReferenceTools.IsValueSafe(entityToCheck,
                new ComplexDebugInformation(basicDebugInformation, "given entity to check isn't valid"))

            //Check if given data array is okay
            && CollectionsTools.arrayTools.IsArrayContentSafe(iFilterDataArray,
                new ComplexDebugInformation(basicDebugInformation, "given data array check isn't valid"))

            //Check complex debug information
            && base.AreValuesValid();
        }
    }
}