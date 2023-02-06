using MECS.Events;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Colliders
{
    //* Args used to share notification data to collider system
    public class NotifyCollisionArgs : AEventArgs
    {
        //Variables
        public readonly Collision collision = null;
        public readonly Collider collider = null;
        public readonly EColliderCallback callback = EColliderCallback.Enter;

        //Default builder
        public NotifyCollisionArgs(Collision collision, Collider collider, EColliderCallback callback,
        ComplexDebugInformation complexDebugInformation) : base(complexDebugInformation)
        {
            this.collision = collision;
            this.collider = collider;
            this.callback = callback;
        }

        //AEventArgs method, check values from args
        public override bool AreValuesValid()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AreValuesValid()");

            //Check if there is a detection
            bool hasDetection =
            //Check collision
            ReferenceTools.IsValueSafe(collision)

            //Check collider
            || ReferenceTools.IsValueSafe(collider);

            //Debug if hasn't detection 
            if (!hasDetection)
                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                "given args parameters doesnt have detections values"));

            return
            //Has detection
            hasDetection

            //Check variables on args
            && ReferenceTools.IsValueSafe(callback,
            new ComplexDebugInformation(basicDebugInformation, "callback value isn't safe"))

            //Check debug information
            && base.AreValuesValid();
        }
    }
}