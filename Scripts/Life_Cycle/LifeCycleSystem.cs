using System.Diagnostics;
using MECS.Collections;
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
            //Check parameters values
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
                //Itinerate data
                foreach (ILifeCycleData data in args.iLifeCycleDataArray)
                {
                    //Avoid type errors
                    if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                    args.debugMessage + " given sender isn't a component"))
                        //Check if its valid
                        if (IsValidData(entityComponent, data))
                        {
                            //Local method, invoke response
                            void InvokeResponse()
                            {
                                //Convert data to event data
                                if (TypeTools.ConvertToType(data, out IEventData eventData,
                                args.debugMessage + " couldnt convert data to event data"))
                                    //Notify event system
                                    new NotificationCommand<NotifyEventsInvocationArgs>(sender,
                                        new NotifyEventsInvocationArgs(new IEventData[] { eventData },
                                        args.debugMessage + " couldnt invoke events on life data")).Execute();
                            }

                            //Check phases
                            if (data.HasAwakeResponse.Value && args.lifeCyclePhase.Equals(ELifeCyclePhase.Awake))
                                InvokeResponse();
                            else if (data.HasEnableResponse.Value && args.lifeCyclePhase.Equals(ELifeCyclePhase.Enable))
                                InvokeResponse();
                            else if (data.HasStartResponse.Value && args.lifeCyclePhase.Equals(ELifeCyclePhase.Start))
                                InvokeResponse();
                            else if (data.HasDisableResponse.Value && args.lifeCyclePhase.Equals(ELifeCyclePhase.Disable))
                                InvokeResponse();
                            else if (data.HasDestroyResponse.Value && args.lifeCyclePhase.Equals(ELifeCyclePhase.Destroy))
                                InvokeResponse();
                            //Notify debug manager if its null
                            else if (data.HasDestroyResponse.Value && args.lifeCyclePhase.Equals(ELifeCyclePhase.NULL))
                                new NotificationCommand<DebugArgs>(sender,
                                new DebugArgs(args.debugMessage + " lifeCyclePhase shouldn't be NULL", LogType.Error,
                                new StackTrace(true))).Execute();
                        }
                }
        }

        //ASystem, check if data values are valid
        protected override bool IsValidData(Component entity, ILifeCycleData data)
        {
            //Return value
            bool isValidData = false;

            //Check parameters
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { entity, data }, " given parameters aren't valid"))
            {
                //Debug information
                string entityName = entity.gameObject.name;

                //Check if there is any life response
                bool hasResponses = data.HasAwakeResponse.Value == true
                || data.HasDestroyResponse.Value == true
                || data.HasDisableResponse.Value == true
                || data.HasEnableResponse.Value == true
                || data.HasStartResponse.Value == true;

                //Check if has any response
                if (hasResponses)
                    isValidData = true;
                //Notify debug manager
                else
                    new NotificationCommand<DebugArgs>(entity,
                    new DebugArgs("given ILifeCycleData must have any life response on: " + entity.gameObject.name,
                    LogType.Error, new StackTrace(true))).Execute();
            }

            return isValidData;
        }
    }
}