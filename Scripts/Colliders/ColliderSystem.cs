using System.Collections.Generic;
using MECS.Collections;
using MECS.Core;
using MECS.Filters;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Colliders
{
    //* System used to process all collision executions
    //* T = IColliderData
    [CreateAssetMenu(fileName = "New_Collider_System", menuName = "MECS/Systems/Collider")]
    public class ColliderSystem : ASystem<IColliderData>
    {
        //ASystem method, make subscriptions
        protected override void OnEnable()
        {
            //ASystem, base execution
            base.OnEnable();

            NotificationCommand<NotifyCollisionArgs>.NotificationEvent += RespondColliderNotification;
        }

        //ASystem method, remove subscriptions
        protected override void OnDisable()
        {
            //ASystem, base execution
            base.OnDisable();

            NotificationCommand<NotifyCollisionArgs>.NotificationEvent -= RespondColliderNotification;
        }

        //ASystem method, set response on data registration
        protected override void DataRegistrationResponse(object sender, ResponsiveDictionaryArgs<IColliderData, MonoBehaviour> args)
        {
            //Check response parameter 
            if (ReferenceTools.AreEventParametersValid(sender, args,
            args.debugMessage + " given event response parameters aren't safe"))
                //Set colliders if data values are valid
                if (IsValidData(args.value, args.key))
                {
                    //Local method, set collider mode
                    void SetCollidersMode(bool isTrigger)
                    {
                        //Itinerate colliders
                        foreach (Collider collider in args.key.CollidersReference.Value)
                            collider.isTrigger = isTrigger;
                    }

                    //Switch collider type and set it
                    switch (args.key.EColliderMode)
                    {
                        case EColliderMode.Collision:
                            SetCollidersMode(false);
                            break;
                        case EColliderMode.Trigger:
                            SetCollidersMode(true);
                            break;
                    }
                }
        }

        //ASystem method, check if data values are valid
        protected override bool IsValidData(Component entityComponent, IColliderData data) =>
            //Check if component is safe
            ReferenceTools.IsValueSafe(entityComponent, " entityComponent isn't safe")

            //Check if data reference is safe
            && ReferenceTools.IsValueSafe(data, " data isn't safe")

            //Check colliders array
            && CollectionsTools.arrayTools.IsArrayContentSafe(data.CollidersReference.Value,
            " given colliders array isn't safe on: " + entityComponent.gameObject.name)

            //Check collider call backs 
            && CollectionsTools.arrayTools.IsArrayContentSafe(data.EColliderCallbacks,
            "given colliders callbacks array isn't safe on: " + entityComponent.gameObject.name);

        //ASystem, respond to collider notification
        private void RespondColliderNotification(object sender, NotifyCollisionArgs args)
        {
            //Check parameters
            if (ReferenceTools.AreEventParametersValid(sender, args, args.debugMessage + "given parameters aren't valid"))

                //Try to convert sender to collider component
                if (TypeTools.ConvertToType<ColliderComponent>(sender, out ColliderComponent colliderComponent,
                args.debugMessage + "couldnt convert sender to collider component"))
                {
                    //Filter data to share
                    List<IFilterData> filterDataList = new();

                    //Itinerate each data value to store filters data references
                    foreach (IColliderData colliderData in colliderComponent.Data)

                        //Avoid errors
                        if (IsValidData(colliderComponent, colliderData))
                            foreach (EColliderCallback callback in colliderData.EColliderCallbacks)

                                //Check callback type is correct
                                if (args.callback == callback)

                                    //Convert collider data to filter data
                                    if (TypeTools.ConvertToType<IFilterData>(colliderData, out IFilterData filterData,
                                      args.debugMessage +
                                     " couldnt convert colliderData to IFilterData on entity: " + colliderComponent.gameObject.name))
                                        filterDataList.Add(filterData);

                    //Avoid errors checking list
                    if (CollectionsTools.arrayTools.IsArrayContentSafe(filterDataList.ToArray(),
                    args.debugMessage + " filter data list isn't valid on entity: " + colliderComponent.gameObject.name))
                    {
                        //Entity to share
                        GameObject entity = args.collision != null ? args.collision.gameObject : args.collider.gameObject;

                        //Notify filter system
                        new NotificationCommand<NotifyFilterSystemArgs>(sender,
                            new NotifyFilterSystemArgs(entity, filterDataList.ToArray(),
                            args.debugMessage + "couldnt notify filter system")).Execute();
                    }
                }
        }
    }
}