using MECS.Core;
using MECS.Patrons.Commands;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Colliders
{
    //* Component used on collider detections
    //* T = ColliderData
    //* T2 = ColliderProfile
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class ColliderComponent : AComponent<ColliderData, ColliderProfile>
    {
        //Store last collision entity
        private GameObject collisionEntity = null;

        public GameObject CollisionEntity { get => collisionEntity; }

        //MonoBehaviour, notify collision enter call back
        private void OnCollisionEnter(Collision collision)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnCollisionEnter(Collision collision)");

            //Notify collision
            collisionEntity = collision.gameObject;
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(collision, null, EColliderCallback.Enter,
                new ComplexDebugInformation(basicDebugInformation, "couldnt notify collision system")), basicDebugInformation)
                .Execute();
        }

        //MonoBehaviour, notify collision exit call back
        private void OnCollisionExit(Collision collision)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnCollisionExit(Collision collision)");

            //Notify collision
            collisionEntity = collision.gameObject;
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(collision, null, EColliderCallback.Exit,
                new ComplexDebugInformation(basicDebugInformation, "couldnt notify collision system")), basicDebugInformation)
                .Execute();
        }

        //MonoBehaviour, notify trigger enter call back
        private void OnTriggerEnter(Collider collider)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnTriggerEnter(Collider collider)");

            //Notify collision
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(null, collider, EColliderCallback.Enter,
                new ComplexDebugInformation(basicDebugInformation, "couldnt notify collision system")), basicDebugInformation)
                .Execute();
        }

        //MonoBehaviour, notify trigger exit call back
        private void OnTriggerExit(Collider collider)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnTriggerExit(Collider collider)");

            //Notify collision
            new NotificationCommand<NotifyCollisionArgs>(this,
                new NotifyCollisionArgs(null, collider, EColliderCallback.Exit,
                new ComplexDebugInformation(basicDebugInformation, "couldnt notify collision system")), basicDebugInformation)
                .Execute();
        }
    }
}