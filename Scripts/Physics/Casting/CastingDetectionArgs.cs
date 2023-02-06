using System;
using MECS.Events;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Physics.Casting
{
    //* Args used to share detections results
    public class CastingDetectionArgs : AEventArgs
    {
        //Variables
        public readonly RaycastHit raycastHit = default;
        public readonly ICastingData iCastingData = null;

        //Default builder
        public CastingDetectionArgs(RaycastHit raycastHit, ICastingData iCastingData,
        ComplexDebugInformation complexDebugInformation) : base(complexDebugInformation)
        {
            this.raycastHit = raycastHit;
            this.iCastingData = iCastingData;
        }

        //AEventArgs method, check variables on args
        public override bool AreValuesValid()
        {
            //Basic information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AreValuesValid()");

            return
            //Check references
            ReferenceTools.AreValuesSafe(new object[] { raycastHit, iCastingData },
            new ComplexDebugInformation(basicDebugInformation, "given values on args aren't valid"))

            //Check complex debug information
            && base.AreValuesValid();
        }
    }
}