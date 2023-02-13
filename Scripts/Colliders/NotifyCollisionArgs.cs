using System.Diagnostics;
using MECS.Events;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Colliders
{
    //* Args used to share notification data to collider system
    public class NotifyCollisionArgs : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly Collision collision = null;
        public readonly Collider collider = null;
        public readonly EColliderCallback callback = EColliderCallback.Enter;

        //Default builder
        public NotifyCollisionArgs(Collision collision, Collider collider, EColliderCallback callback,
        string debugMessage) : base(debugMessage)
        {
            this.collision = collision;
            this.collider = collider;
            this.callback = callback;
        }

        //AEventArgs method, check values from args
        public bool AreValuesValid()
        {
            //Check if there is a detection
            bool hasDetection =
            //Check collision
            ReferenceTools.IsValueSafe(collision)

            //Check collider
            || ReferenceTools.IsValueSafe(collider);

            //Notify debug manager if hasn't detections 
            if (!hasDetection)
                new NotificationCommand<DebugArgs>(this,
                new DebugArgs(debugMessage + " given args parameters doesnt have detections values", LogType.Error,
                new StackTrace(true))).Execute();

            return
            //Has detection
            hasDetection

            //Check variables on args
            && ReferenceTools.IsValueSafe(callback, debugMessage + " callback value isn't safe");
        }
    }
}