using MECS.Core;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Colliders
{
    //* Component used on collider detections
    //* T = ColliderData
    //* T2 = ColliderProfile
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class ColliderComponent : AComponent<ColliderData, ColliderProfile>
    {
        //MonoBehaviour method, notify collision enter call back
        private void OnCollisionEnter(Collision collision) =>
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(collision, null, EColliderCallback.Enter, " couldnt notify collision system")).Execute();

        //MonoBehaviour method, notify collision exit call back
        private void OnCollisionExit(Collision collision) =>
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(collision, null, EColliderCallback.Exit, " couldnt notify collision system")).Execute();

        //MonoBehaviour method, notify trigger enter call back
        private void OnTriggerEnter(Collider collider) =>
            //Notify collision
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(null, collider, EColliderCallback.Enter, " couldnt notify collision system")).Execute();


        //MonoBehaviour method, notify trigger exit call back
        private void OnTriggerExit(Collider collider) =>
            //Notify collision
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(null, collider, EColliderCallback.Exit, " couldnt notify collision system")).Execute();
    }
}