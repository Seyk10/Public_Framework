using System.Collections.Generic;
using MECS.Collections;
using MECS.Core;
using MECS.Filters;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Colliders
{
    //* System used to process all collision executions
    //* T = IColliderData
    [CreateAssetMenu(fileName = "New_Collider_System", menuName = "MECS/Systems/Collider")]
    public class ColliderSystem : ASystem<IColliderData>
    {
        //ASystem, make subscriptions
        protected override void OnEnable()
        {
            //ASystem, base execution
            base.OnEnable();

            NotificationCommand<NotifyCollisionArgs>.NotificationEvent += RespondColliderNotification;
        }

        //ASystem, remove subscriptions
        protected override void OnDisable()
        {
            //ASystem, base execution
            base.OnDisable();

            NotificationCommand<NotifyCollisionArgs>.NotificationEvent -= RespondColliderNotification;
        }

        //ASystem, set response on data registration
        protected override void DataRegistrationResponse(object sender, ResponsiveDictionaryArgs<IColliderData, MonoBehaviour> args)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "DataRegistrationResponse(object sender, ResponsiveDictionaryArgs<IColliderData, MonoBehaviour> args)");

            //Check response parameter 
            if (ReferenceTools.AreEventParametersValid(sender, args,
            new ComplexDebugInformation(basicDebugInformation, "given event response parameters aren't safe")))
                //Set colliders if data values are valid
                if (IsValidData(args.value, args.key,
                args.complexDebugInformation.AddTempCustomText("given data values aren't valid")))
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
        protected override bool IsValidData(Component entityComponent, IColliderData data,
        ComplexDebugInformation complexDebugInformation) =>
            //Check if component is safe
            ReferenceTools.IsValueSafe(entityComponent, complexDebugInformation.AddTempCustomText("entityComponent isn't safe"))

            //Check if data reference is safe
            && ReferenceTools.IsValueSafe(data, complexDebugInformation.AddTempCustomText("data isn't safe"))

            //Check colliders array
            && CollectionsTools.arrayTools.IsArrayContentSafe(data.CollidersReference.Value,
            complexDebugInformation.AddTempCustomText("given colliders array isn't safe on: " + entityComponent.gameObject.name))

            //Check collider call backs 
            && CollectionsTools.arrayTools.IsArrayContentSafe(data.EColliderCallbacks,
            complexDebugInformation.AddTempCustomText("given colliders callbacks array isn't safe on: "
            + entityComponent.gameObject.name));

        //ASystem, respond to collider notification
        private void RespondColliderNotification(object sender, NotifyCollisionArgs args)
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "RespondColliderNotification(object sender, NotifyCollisionArgs args)");

            //Check parameters
            if (ReferenceTools.AreEventParametersValid(sender, args,
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't valid")))

                //Try to convert sender to collider component
                if (TypeTools.ConvertToType<ColliderComponent>(sender, out ColliderComponent colliderComponent,
                new ComplexDebugInformation(basicDebugInformation, "couldnt convert sender to collider component")))
                {
                    //Filter data to share
                    List<IFilterData> filterDataList = new();

                    //Itinerate each data value to store filters data references
                    foreach (IColliderData colliderData in colliderComponent.DataReference.GetValue())

                        //Avoid errors
                        if (IsValidData(colliderComponent, colliderData,
                        new ComplexDebugInformation(basicDebugInformation, "given data isn't valid")))
                            foreach (EColliderCallback callback in colliderData.EColliderCallbacks)

                                //Check callback type is correct
                                if (args.callback == callback)

                                    //Convert collider data to filter data
                                    if (TypeTools.ConvertToType<IFilterData>(colliderData, out IFilterData filterData,
                                     new ComplexDebugInformation(basicDebugInformation,
                                     "couldnt convert colliderData to IFilterData on entity: " + colliderComponent.gameObject.name)))
                                        filterDataList.Add(filterData);

                    //Avoid errors checking list
                    if (CollectionsTools.listTools.IsListContentSafe(filterDataList,
                    new ComplexDebugInformation(basicDebugInformation, "filter data list isn't valid on entity: "
                    + colliderComponent.gameObject.name)))
                    {
                        //Entity to share
                        GameObject entity = args.collision != null ? args.collision.gameObject : args.collider.gameObject;

                        //Notify filter system
                        new NotificationCommand<NotifyFilterSystemArgs>(sender,
                            new NotifyFilterSystemArgs(entity, filterDataList.ToArray(),
                            args.complexDebugInformation.AddCustomText("couldnt notify filter system")),
                            args.complexDebugInformation.AddCustomText("couldnt notify filter system")).Execute();
                    }
                }
        }
    }
}