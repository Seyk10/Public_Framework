using MECS.Collections;
using MECS.Events;
using MECS.Tools;
using UnityEngine;

namespace MECS.Filters
{
    //* Share values to check with filters
    public class NotifyFilterSystemArgs : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly GameObject entityToCheck = null;
        public readonly IFilterData[] iFilterDataArray = null;

        //Default builder
        public NotifyFilterSystemArgs(GameObject entityToCheck, IFilterData[] iFilterDataArray,
        string debugMessage) : base(debugMessage)
        {
            this.entityToCheck = entityToCheck;
            this.iFilterDataArray = iFilterDataArray;
        }

        //AEventArgs method, check if given values on args are valid
        public bool AreValuesValid() =>
            //Check entity to check
            ReferenceTools.IsValueSafe(entityToCheck,
                debugMessage + " given entity to check isn't valid")

            //Check if given data array is okay
            && CollectionsTools.arrayTools.IsArrayContentSafe(iFilterDataArray,
                debugMessage + " given data array check isn't valid");
    }
}