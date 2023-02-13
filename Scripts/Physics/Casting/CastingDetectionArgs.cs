using MECS.Collections;
using MECS.Events;
using MECS.Tools;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Args used to share detections results
    public class CastingDetectionArgs : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly RaycastHit raycastHit = default;
        public readonly ICastingData castingData = null;

        //Default builder
        public CastingDetectionArgs(RaycastHit raycastHit, ICastingData iCastingData,
        string debugMessage) : base(debugMessage)
        {
            this.raycastHit = raycastHit;
            this.castingData = iCastingData;
        }

        //AEventArgs method, check variables on args
        public bool AreValuesValid() =>
            //Check references
            CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { raycastHit, castingData },
            debugMessage + " given values on args aren't valid");
    }
}