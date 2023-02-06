using MECS.Core;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Events
{
    //* Class used to rise on event type data structures
    //* T = IEventData
    [CreateAssetMenu(fileName = "New_Event_System", menuName = "MECS/Systems/Event")]
    public class EventSystem : ASystem<IEventData>
    {
        //ASystem, make subscription
        protected override void OnEnable()
        {
            //ASystem, base execution
            base.OnEnable();

            //TODO: make subscription for each data 
            NotificationCommand<NotifyEventsInvocationArgs>.NotificationEvent += RespondEventsNotification;
        }

        //ASystem, remove subscription
        protected override void OnDisable()
        {
            //ASystem, base execution
            base.OnDisable();

            //TODO: make subscription for each data 
            NotificationCommand<NotifyEventsInvocationArgs>.NotificationEvent -= RespondEventsNotification;
        }

        //ASystem, used to check if data values are valid
        protected override bool IsValidData(Component entity, IEventData data, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new("EventSystem", "IsValidData(Component entity, IEventData data)");
            string entityName = entity.gameObject.name;

            //Value to return
            //Check full event reference
            bool isCorrectData =
                //Check sender parameter
                ReferenceTools.IsValueSafe(entity, new ComplexDebugInformation(basicDebugInformation, "sender isn't safe"))

                //Check data parameter
                && ReferenceTools.IsValueSafe(data, new ComplexDebugInformation(basicDebugInformation, "data isn't safe"))

                //Check data values
                && ReferenceTools.IsValueSafe(data.EventReference,
                complexDebugInformation.AddTempCustomText("given event reference isn't safe on entity: "
                + entityName))

                //Check editor event reference
                && ReferenceTools.IsValueSafe(data.EventReference.UnityEventResponse,
                complexDebugInformation.AddTempCustomText("given UnityEventResponse isn't safe on entity: "
                + entityName));

            //Check action event reference if its necessary
            if (isCorrectData && data.EventReference.IsActionOnUse)
                isCorrectData = ReferenceTools.IsValueSafe(data.EventReference.ActionResponse,
                complexDebugInformation.AddTempCustomText("given ActionResponse isn't safe on entity: " + entityName));

            return isCorrectData;
        }

        //ASystem, invoke values of IEventData
        private void RespondEventsNotification(object sender, NotifyEventsInvocationArgs args)
        {
            //Debug values
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "RespondEventsNotification(object sender, NotifyEventsInvocationArgs args)");

            //Check if event parameters are safe
            bool areParametersSafe = ReferenceTools.AreEventParametersValid(sender, args,
            new ComplexDebugInformation(basicDebugInformation, "parameters aren't safe"));

            //Avoid parameters errors
            if (areParametersSafe)
                //Check if sender is component and get values
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                new ComplexDebugInformation(basicDebugInformation, "couldnt convert sender to component")))
                {
                    //Store values to avoid over flow on iteration 
                    string entityName = entityComponent.gameObject.name,
                    componentName = entityComponent.GetType().ToString();

                    //Itinerate data and raise
                    foreach (IEventData data in args.iEventDataArray)
                        //Avoid errors
                        if (IsValidData(entityComponent, data, args.complexDebugInformation
                        .AddTempCustomText("given data isn't valid on entity: " + entityName)))
                            //Invoke data
                            data.EventReference.RaiseEvents(entityName, componentName, args.complexDebugInformation
                            .AddTempCustomText("couldnt raise events on entity: " + entityName));
                }
        }
    }
}