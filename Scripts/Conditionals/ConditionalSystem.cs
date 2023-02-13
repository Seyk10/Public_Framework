using System.Diagnostics;
using MECS.Collections;
using MECS.Core;
using MECS.Events;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Conditionals
{
    //* System used to execute all the code logics related to conditionals
    //* T = IConditionalData
    [CreateAssetMenu(fileName = "New_Conditional_System", menuName = "MECS/Systems/Conditional")]
    public class ConditionalSystem : ASystem<IConditionalData>
    {
        #region LIFE_CYCLE_ASYSTEM_METHODS
        //ASystem method, set subscriptions on manager list calls
        protected override void OnEnable()
        {
            //ASystem base execution
            base.OnEnable();

            //Response to check command
            CheckConditionalsCommand.CheckValuesEvent += CheckConditionalsConditions;

            //Subscribe responses to scriptable conditional commands
            ScriptableAddNumericConditionalCommand.ExecuteCommandEvent += ScriptableSetConditionalResponse;
            ScriptableSetBooleanConditionalCommand.ExecuteCommandEvent += ScriptableSetConditionalResponse;
            ScriptableSetStringConditionalCommand.ExecuteCommandEvent += ScriptableSetConditionalResponse;
        }

        //ASystem method, remove subscriptions on manager list calls
        protected override void OnDisable()
        {
            //ASystem base execution
            base.OnDisable();

            //Response to check command
            CheckConditionalsCommand.CheckValuesEvent -= CheckConditionalsConditions;

            //Unsubscribe responses to scriptable conditional commands
            ScriptableAddNumericConditionalCommand.ExecuteCommandEvent -= ScriptableSetConditionalResponse;
            ScriptableSetBooleanConditionalCommand.ExecuteCommandEvent -= ScriptableSetConditionalResponse;
            ScriptableSetStringConditionalCommand.ExecuteCommandEvent -= ScriptableSetConditionalResponse;
        }
        #endregion

        #region CONDITIONAL_RESPONSES_METHODS
        //Method, execute add numeric command on given component
        private void ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<float> args)
        {
            //Check if parameters are valid
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
            {
                //Local method, check conditionals conditions
                void CheckEachDataConditionalConditions(Component entityComponent)
                {
                    //Itinerate data values to execute command
                    foreach (IConditionalData data in args.Component.Data)
                        //Avoid errors
                        if (IsValidData(entityComponent, data))
                        {
                            new AddNumericConditionalCommand(args.Value, data.NumericConditionals).Execute();
                            //Check conditions
                            CheckConditionalsConditions(args.Component, data);
                        }
                }

                //Try to convert sender to component
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                args.debugMessage + " couldnt convert sender to component"))
                    CheckEachDataConditionalConditions(entityComponent);
                //Use args component as entity
                else
                    CheckEachDataConditionalConditions(args.Component);
            }
        }

        //Method, execute set boolean command on given component
        private void ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<bool> args)
        {
            //Check if parameters are valid
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
            {
                //Local method, check conditionals conditions
                void CheckEachDataConditionalConditions(Component entityComponent)
                {
                    //Itinerate data values to execute command
                    foreach (IConditionalData data in args.Component.Data)
                        //Avoid errors
                        if (IsValidData(entityComponent, data))
                        {
                            new SetBooleanConditionalCommand(args.Value, data.BoolConditionals).Execute();
                            //Check conditions
                            CheckConditionalsConditions(args.Component, data);
                        }
                }

                //Try to convert sender to component
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                args.debugMessage + "couldnt convert sender to component"))
                    CheckEachDataConditionalConditions(entityComponent);
                //Use args component as entity
                else
                    CheckEachDataConditionalConditions(args.Component);
            }
        }

        //Method, execute set string command on given component
        private void ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<string> args)
        {
            //Check if parameters are valid
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
            {
                //Local method, check conditionals conditions
                void CheckEachDataConditionalConditions(Component entityComponent)
                {
                    //Itinerate data values to execute command
                    foreach (IConditionalData data in args.Component.Data)
                        //Avoid errors
                        if (IsValidData(entityComponent, data))
                        {
                            new SetStringConditionalCommand(args.Value, data.StringConditionals).Execute();
                            //Check conditions
                            CheckConditionalsConditions(args.Component, data);
                        }
                }

                //Try to convert sender to component
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                args.debugMessage + "couldnt convert sender to component"))
                    CheckEachDataConditionalConditions(entityComponent);
                //Use args component as entity
                else
                    CheckEachDataConditionalConditions(args.Component);
            }
        }
        #endregion

        //Method, check conditionals on data array and raise events if its necessary
        private void CheckConditionalsConditions(object sender, IConditionalData data)
        {
            //Local method, check numeric conditionals
            bool CheckNumericConditionals()
            {
                //Return type
                bool returnValue = true;

                //Check conditionals
                if (CollectionsTools.arrayTools.IsArrayContentSafe(data.NumericConditionals,
                " given array data.NumericConditionals inst safe "))
                    foreach (NumericConditional numericConditional in data.NumericConditionals)
                        if (!numericConditional.IsOperationCorrect())
                        {
                            returnValue = false;
                            break;
                        }

                return returnValue;
            }

            //Local method, check bool conditionals
            bool CheckBoolConditionals()
            {
                //Return type
                bool returnValue = true;

                //Check conditionals
                if (CollectionsTools.arrayTools.IsArrayContentSafe(data.BoolConditionals,
                " given array data.BoolConditionals inst safe "))
                    foreach (BoolConditional boolConditional in data.BoolConditionals)
                        if (!boolConditional.IsOperationCorrect())
                        {
                            returnValue = false;
                            break;
                        }

                return returnValue;
            }

            //Local method, check string conditionals
            bool CheckStringConditionals()
            {
                //Return type
                bool returnValue = true;

                //Check conditionals
                if (CollectionsTools.arrayTools.IsArrayContentSafe(data.StringConditionals,
                " given array data.StringConditionals inst safe "))
                    foreach (StringConditional stringConditional in data.StringConditionals)
                        if (!stringConditional.IsOperationCorrect())
                        {
                            returnValue = false;
                            break;
                        }

                return returnValue;
            }

            //Check if any conditional type is active
            bool isAnyConditionalActive = data.UseNumericConditionals.Value
            || data.UseStringConditionals.Value
            || data.UseBoolConditionals.Value;

            if (isAnyConditionalActive)
            {
                //Check each conditional type
                //Check numeric conditionals
                bool canNumericConditions = data.UseNumericConditionals.Value && CheckNumericConditionals()
                    || !data.UseNumericConditionals.Value,

                //Check string conditionals
                canStringConditions = data.UseStringConditionals.Value && CheckStringConditionals()
                    || !data.UseStringConditionals.Value,

                //Check bool conditionals
                canBooleanConditions = data.UseBoolConditionals.Value && CheckBoolConditionals()
                    || !data.UseBoolConditionals.Value,

                //Check if there is any conditional active and availble
                canRaise = canNumericConditions && canStringConditions && canBooleanConditions;

                //Raise responses if its possible
                if (canRaise)
                    //Convert data to event data type
                    if (TypeTools.ConvertToType<IEventData>(data, out IEventData eventData,
                    " couldnt convert conditional data as event data"))
                        //Event system notification
                        new NotificationCommand<NotifyEventsInvocationArgs>(sender,
                                            new NotifyEventsInvocationArgs(new IEventData[] { eventData },
                                            " couldnt invoke events from given event data")).Execute();
            }
            //Notify debug manager if there isn't any conditional checking, convert sender to component and get name
            else if (TypeTools.ConvertToType<Component>(sender, out Component component,
                    " couldnt convert sender to component"))
                //Execute notification
                new NotificationCommand<DebugArgs>(this, new DebugArgs(" given data on entity: "
                + component.gameObject.name + " hasn't any conditional checking active", LogType.Error,
                new System.Diagnostics.StackTrace(true))).Execute();
        }

        //ASystem method, used to check data values
        protected override bool IsValidData(Component entity, IConditionalData data)
        {
            //Return value
            bool isValidData = false;

            //Avoid parameters errors
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { entity, data },
            " given parameters aren't safe"))
            {
                string entityName = entity.gameObject.name;

                //Check if there is any conditional active
                bool hasConditionalChecking = data.UseBoolConditionals.Value
                || data.UseNumericConditionals.Value
                || data.UseStringConditionals.Value;

                //Check arrays
                if (hasConditionalChecking)
                {
                    //Check if bool conditionals are safe
                    if (data.UseBoolConditionals.Value)
                        hasConditionalChecking = CollectionsTools.arrayTools.IsArrayContentSafe(data.BoolConditionals,
                        " given BoolConditionals array isn't safe on entity: " + entityName);

                    //Check if numeric conditionals are safe
                    if (hasConditionalChecking && data.UseNumericConditionals.Value)
                        hasConditionalChecking = CollectionsTools.arrayTools.IsArrayContentSafe(data.NumericConditionals,
                        " given NumericConditionals array isn't safe on entity: " + entityName);

                    //Check if string conditionals are safe
                    if (hasConditionalChecking && data.UseStringConditionals.Value)
                        hasConditionalChecking = CollectionsTools.arrayTools.IsArrayContentSafe(data.StringConditionals,
                        " given StringConditionals array isn't safe on entity: " + entityName);
                }
                //Notify debug editor
                else
                    new NotificationCommand<DebugArgs>(entity,
                    new DebugArgs("IConditionalData must contain atleast one conditional type active on entity : " + entityName,
                    LogType.Error, new StackTrace(true)))
                    .Execute();
            }

            return isValidData;
        }
    }
}