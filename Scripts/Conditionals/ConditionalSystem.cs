using MECS.Collections;
using MECS.Core;
using MECS.Events;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Conditionals
{
    //* System used to execute all the code logics related to conditionals
    //* T = IConditionalData
    [CreateAssetMenu(fileName = "New_Conditional_System", menuName = "MECS/Systems/Conditional")]
    public class ConditionalSystem : ASystem<IConditionalData>
    {
        //Set subscriptions on manager list calls
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

        //Remove subscriptions on manager list calls
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

        //Method, execute add numeric command on given component
        private void ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<float> args)
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation =
            new(this.GetType().Name, "ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<float> args)");

            //Check parameters values before executing
            //Check sender
            bool areParametersValid = ReferenceTools.IsValueSafe(sender,
            new ComplexDebugInformation(basicDebugInformation, "sender isn't valid"))

            //Check args
            && ReferenceTools.IsValueSafe(args,
            new ComplexDebugInformation(basicDebugInformation, "sender isn't valid"))

            //Check args values
            && args.AreValuesValid();

            //Check if parameters are valid
            if (areParametersValid)
            {
                //Local method, check conditionals conditions
                void CheckEachDataConditionalConditions(Component entityComponent)
                {
                    //Itinerate data values to execute command
                    foreach (IConditionalData data in args.Component.DataReference.GetValue())
                        //Avoid errors
                        if (IsValidData(entityComponent, data, new ComplexDebugInformation(basicDebugInformation,
                        "given data isn't valid")))
                        {
                            new AddNumericConditionalCommand(args.Value, data.NumericConditionals).Execute();
                            //Check conditions
                            CheckConditionalsConditions(args.Component, data);
                        }
                }

                //Try to convert sender to component
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                new ComplexDebugInformation(basicDebugInformation, "couldnt convert sender to component")))
                    CheckEachDataConditionalConditions(entityComponent);
                //Use args component as entity
                else
                    CheckEachDataConditionalConditions(args.Component);
            }
        }

        //Method, execute set boolean command on given component
        private void ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<bool> args)
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation =
            new(this.GetType().Name, "ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<bool> args)");

            //Check parameters values before executing
            //Check sender
            bool areParametersValid = ReferenceTools.IsValueSafe(sender,
            new ComplexDebugInformation(basicDebugInformation, "sender isn't valid"))

            //Check args
            && ReferenceTools.IsValueSafe(args,
            new ComplexDebugInformation(basicDebugInformation, "sender isn't valid"))

            //Check args values
            && args.AreValuesValid();

            //Check if parameters are valid
            if (areParametersValid)
            {
                //Local method, check conditionals conditions
                void CheckEachDataConditionalConditions(Component entityComponent)
                {
                    //Itinerate data values to execute command
                    foreach (IConditionalData data in args.Component.DataReference.GetValue())
                        //Avoid errors
                        if (IsValidData(entityComponent, data, new ComplexDebugInformation(basicDebugInformation,
                        "given data isn't valid")))
                        {
                            new SetBooleanConditionalCommand(args.Value, data.BoolConditionals).Execute();
                            //Check conditions
                            CheckConditionalsConditions(args.Component, data);
                        }
                }

                //Try to convert sender to component
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                new ComplexDebugInformation(basicDebugInformation, "couldnt convert sender to component")))
                    CheckEachDataConditionalConditions(entityComponent);
                //Use args component as entity
                else
                    CheckEachDataConditionalConditions(args.Component);
            }
        }

        //Method, execute set string command on given component
        private void ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<string> args)
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation =
            new(this.GetType().Name, "ScriptableSetConditionalResponse(object sender, ScriptableConditionalArgs<string> args)");

            //Check parameters values before executing
            //Check sender
            bool areParametersValid = ReferenceTools.IsValueSafe(sender,
            new ComplexDebugInformation(basicDebugInformation, "sender isn't valid"))

            //Check args
            && ReferenceTools.IsValueSafe(args,
            new ComplexDebugInformation(basicDebugInformation, "sender isn't valid"))

            //Check args values
            && args.AreValuesValid();

            //Check if parameters are valid
            if (areParametersValid)
            {
                //Local method, check conditionals conditions
                void CheckEachDataConditionalConditions(Component entityComponent)
                {
                    //Itinerate data values to execute command
                    foreach (IConditionalData data in args.Component.DataReference.GetValue())
                        //Avoid errors
                        if (IsValidData(entityComponent, data, new ComplexDebugInformation(basicDebugInformation,
                        "given data isn't valid")))
                        {
                            new SetStringConditionalCommand(args.Value, data.StringConditionals).Execute();
                            //Check conditions
                            CheckConditionalsConditions(args.Component, data);
                        }
                }

                //Try to convert sender to component
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                new ComplexDebugInformation(basicDebugInformation, "couldnt convert sender to component")))
                    CheckEachDataConditionalConditions(entityComponent);
                //Use args component as entity
                else
                    CheckEachDataConditionalConditions(args.Component);
            }
        }

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
                new ComplexDebugInformation(this.GetType().Name, "CheckConditionalsConditions(object sender, IConditionalData data)",
                "given array data.NumericConditionals inst safe ")))
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
                new ComplexDebugInformation(this.GetType().Name, "CheckConditionalsConditions(object sender, IConditionalData data)",
                "given array data.BoolConditionals inst safe ")))
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
                new ComplexDebugInformation(this.GetType().Name, "CheckConditionalsConditions(object sender, IConditionalData data)",
                "given array data.StringConditionals inst safe ")))
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
                bool canNumericConditions = data.UseNumericConditionals.Value && CheckNumericConditionals() || !data.UseNumericConditionals.Value,
                   canStringConditions = data.UseStringConditionals.Value && CheckStringConditionals() || !data.UseStringConditionals.Value,
                   canBooleanConditions = data.UseBoolConditionals.Value && CheckBoolConditionals() || !data.UseBoolConditionals.Value,
                   canRaise = canNumericConditions && canStringConditions && canBooleanConditions;

                //Raise responses if can
                if (canRaise)
                {
                    //Convert
                    IEventData[] eventData = { data as IEventData };

                    //Check if can invoke
                    if (ReferenceTools.IsValueSafe(eventData[0]))
                    {
                        //Debug information
                        BasicDebugInformation basicDebugInformation = new("ConditionalSystem",
                         "CheckConditionalsConditions(object sender, IConditionalData data)");

                        //Event system notification
                        new NotificationCommand<NotifyEventsInvocationArgs>(sender,
                            new NotifyEventsInvocationArgs(eventData,
                            new ComplexDebugInformation(basicDebugInformation, "couldnt invoke events from given event data")),
                            basicDebugInformation).Execute();
                    }
#if UNITY_EDITOR
                    else Debug.LogError("Warning: Given data doesnt implement IEventData interface, cant invoke " + sender.ToString());
#endif
                }
            }
#if UNITY_EDITOR
            else
                Debug.LogError("Warning: Given data doesnt have any conditional check.");
#endif
        }

        //ASystem method, used to check data values
        protected override bool IsValidData(Component entity, IConditionalData data,
        ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation =
            new(this.GetType().Name, "IsValidData(Component entity, IConditionalData data)");
            string entityName = entity.gameObject.name;

            //Check if there is any conditional active
            bool isValidData = data.UseBoolConditionals.Value
            || data.UseNumericConditionals.Value
            || data.UseStringConditionals.Value;

            //Check arrays
            if (isValidData)
            {
                //Check if bool conditionals are safe
                if (data.UseBoolConditionals.Value)
                    isValidData = CollectionsTools.arrayTools.IsArrayContentSafe(data.BoolConditionals,
                    complexDebugInformation.AddTempCustomText("given BoolConditionals array isn't safe on entity: " + entityName));

                //Check if numeric conditionals are safe
                if (isValidData && data.UseNumericConditionals.Value)
                    isValidData = CollectionsTools.arrayTools.IsArrayContentSafe(data.NumericConditionals,
                    complexDebugInformation.AddTempCustomText("given NumericConditionals array isn't safe on entity: " + entityName));

                //Check if string conditionals are safe
                if (isValidData && data.UseStringConditionals.Value)
                    isValidData = CollectionsTools.arrayTools.IsArrayContentSafe(data.StringConditionals,
                    complexDebugInformation.AddTempCustomText("given StringConditionals array isn't safe on entity: " + entityName));
            }
#if UNITY_EDITOR
            else
                DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                "IConditionalData must contain atleast one conditional type active on entity : " + entityName));
#endif
            return isValidData;
        }
    }
}