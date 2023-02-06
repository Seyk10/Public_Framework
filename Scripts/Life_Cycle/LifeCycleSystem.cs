using MECS.Core;
using MECS.Events;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.LifeCycle
{
    //* System used to manage life cycle executions
    //* T = ILifeCycleData
    [CreateAssetMenu(fileName = "New_Life_Cycle_System", menuName = "MECS/Systems/Life_Cycle")]
    public class LifeCycleSystem : ASystem<ILifeCycleData>
    {
        //ASystem, make subscriptions
        protected override void OnEnable()
        {
            //ASystem base execution
            base.OnEnable();

            //TODO: Subscribe only registered data
            NotificationCommand<LifeCycleNotificationArgs>.NotificationEvent += ResponseLifePhase;
        }

        //ASystem, remove subscriptions
        protected override void OnDisable()
        {
            //ASystem base execution
            base.OnDisable();

            //TODO: Subscribe only registered data
            NotificationCommand<LifeCycleNotificationArgs>.NotificationEvent -= ResponseLifePhase;
        }

        //Response awake life phase
        private void ResponseLifePhase(object sender, LifeCycleNotificationArgs args)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation
            = new(this.GetType().Name, "ResponseLifePhase(object sender, LifeCycleNotificationArgs args)");

            //Check parameters values
            if (ReferenceTools.AreEventParametersValid(sender, args,
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't valid")))
                //Itinerate data
                foreach (ILifeCycleData data in args.iLifeCycleDataArray)
                {
                    //Avoid type errors
                    if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                    new ComplexDebugInformation(basicDebugInformation, "given sender isn't a component")))
                        //Check if its valid
                        if (IsValidData(entityComponent, data,
                        new ComplexDebugInformation(basicDebugInformation, "couldnt response to life phase")))
                        {
                            //Local method, invoke response
                            void InvokeResponse()
                            {
                                //Convert data to event data
                                if (TypeTools.ConvertToType(data, out IEventData eventData, args.complexDebugInformation
                                .AddTempCustomText("couldnt convert data to event data")))
                                    //Notify event system
                                    new NotificationCommand<NotifyEventsInvocationArgs>(sender,
                                        new NotifyEventsInvocationArgs(new IEventData[] { eventData },
                                        args.complexDebugInformation.AddTempCustomText("life cycle data doesnt implement IEventData"))
                                        , args.complexDebugInformation
                                        .AddTempCustomText("couldnt invoke events on life data")).Execute();
                            }

                            //Check phases
                            if (data.HasAwakeResponse.Value && args.lifeCyclePhase == ELifeCyclePhase.Awake)
                                InvokeResponse();
                            else if (data.HasEnableResponse.Value && args.lifeCyclePhase == ELifeCyclePhase.Enable)
                                InvokeResponse();
                            else if (data.HasStartResponse.Value && args.lifeCyclePhase == ELifeCyclePhase.Start)
                                InvokeResponse();
                            else if (data.HasDisableResponse.Value && args.lifeCyclePhase == ELifeCyclePhase.Disable)
                                InvokeResponse();
                            else if (data.HasDestroyResponse.Value && args.lifeCyclePhase == ELifeCyclePhase.Destroy)
                                InvokeResponse();
                            else if (data.HasDestroyResponse.Value && args.lifeCyclePhase == ELifeCyclePhase.NULL)
                                DebugTools.DebugError(new ComplexDebugInformation
                                ("LifeCycleSystem", "ResponseLifePhase(object sender, LifeCycleNotificationArgs args)",
                                "lifeCyclePhase shouldn't be NULL"));
                        }
                }
        }

        //ASystem, check if data values are valid
        protected override bool IsValidData(Component entity, ILifeCycleData data, ComplexDebugInformation complexDebugInformation)
        {
            bool isValidData = true;
            //Debug information
            string entityName = entity.gameObject.name;

            //Check if there is any life response
            bool hasResponses = data.HasAwakeResponse.Value == true
            || data.HasDestroyResponse.Value == true
            || data.HasDisableResponse.Value == true
            || data.HasEnableResponse.Value == true
            || data.HasStartResponse.Value == true;

            //Check if has any response
            if (!hasResponses)
            {
                isValidData = false;

#if UNITY_EDITOR
                DebugTools.DebugError(complexDebugInformation
                .AddTempCustomText("given ILifeCycleData must have any life response on: " + entityName));
#endif
            }

            return isValidData;
        }
    }
}