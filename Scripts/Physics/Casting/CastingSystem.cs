using MECS.Collections;
using MECS.Conditionals;
using MECS.Core;
using MECS.Filters;
using MECS.Patrons.Commands;
using MECS.Patrons.StateMachine;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Physics.Casting
{
    //* System used to manage executions related to castings
    //* T = ICastingData 
    [CreateAssetMenu(fileName = "New_Casting_System", menuName = "MECS/Systems/Casting")]
    public class CastingSystem : ASystem<ICastingData>, ISetState<ICastingData, ACastingState>
    {
        //ASystem, set subscriptions
        protected override void OnEnable()
        {
            //ASystem, base execution
            base.OnEnable();

            //Make subscriptions
            AManager<ICastingData>.dataDictionary.ElementAddedEvent += InitializeStateMachineStates;

            StartCastingStateCommand.StartCastingStateEvent += RespondEditorStateCommand<DetectingCastState>;
            StopCastingStateCommand.StopCastingStateEvent += RespondEditorStateCommand<SleepingState>;
        }

        //ASystem, remove subscriptions
        protected override void OnDisable()
        {
            //ASystem, base execution
            base.OnDisable();

            //Remove subscriptions
            AManager<ICastingData>.dataDictionary.ElementAddedEvent -= InitializeStateMachineStates;

            StartCastingStateCommand.StartCastingStateEvent -= RespondEditorStateCommand<DetectingCastState>;
            StopCastingStateCommand.StopCastingStateEvent -= RespondEditorStateCommand<SleepingState>;
        }

        //ISetState, set state machine states on data structure
        public void InitializeStateMachineStates(object sender, ResponsiveDictionaryArgs<ICastingData, MonoBehaviour> args)
        {
            //Check parameters
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
            {
                //Store data
                ICastingData data = args.key;

                //Check if can add states
                if (!data.IsCastingInitialized)
                {
                    data.IsCastingInitialized = true;

                    //Create states
                    DetectingCastState detectingCastState = new DetectingCastState(args.value, data);
                    SleepingState sleepingState = new SleepingState(args.value, data);

                    //Add states
                    data.CastingStateMachine.AddState(detectingCastState);
                    data.CastingStateMachine.AddState(sleepingState);

                    //Subscribe to run timer
                    detectingCastState.RaycastHitEvent += ResponseCastingDetection;
                }
            }
        }

        //ISetState, set state machine states on response to events
        public void SetStateResponse<T3>(object sender, ICastingData data) where T3 : ACastingState
        {
            //Store state machine
            CastingStateMachine stateMachine = data.CastingStateMachine;

            //Local method, run or set state
            void SetCurrentState()
            {
                //Avoid repeat timers and execute it if its the same
                if (stateMachine != null && stateMachine.CurrentState is T3)
                    //Run current state
                    stateMachine.CurrentState.RunState();
                else
                    //Check if timer has state
                    stateMachine.SetState<T3>();
            }

            //Check if state machine contains state type, initialize if it does not exist
            if (stateMachine.ContainsState<T3>())
                SetCurrentState();
            else
            {
                //Convert sender to monoBehaviour
                if (TypeTools.ConvertToType(sender, out MonoBehaviour monoBehaviour, " couldnt convert sender to MonoBehaviour"))
                {
                    InitializeStateMachineStates(sender,
                    new ResponsiveDictionaryArgs<ICastingData, MonoBehaviour>(data, monoBehaviour, " couldnt initialize state machine"));
                    SetCurrentState();
                }
            }
        }

        //ASystem, checks if ICastingData values are valid
        protected override bool IsValidData(Component entityComponent, ICastingData data)
        {
            //Return value            
            bool isDataSafe =
                //Check parameters values
                //Check entityComponent
                ReferenceTools.IsValueSafe(entityComponent, " given entityComponent isn't valid")

                //Check data
                && ReferenceTools.IsValueSafe(data, " given state machine isn't valid");

            //Makes all the checks if references are safe
            if (isDataSafe)
            {
                //Store entity name for debug
                string entityName = entityComponent.gameObject.name;

                //Check casting machine
                isDataSafe = ReferenceTools.IsValueSafe(data.CastingStateMachine,
                " given state machine isn't valid on entity: " + entityName);

                //Itinerate origin values 
                foreach (var originReference in data.OriginTransforms)
                    //Check reference values
                    if (!originReference.CheckEditorReferences())
                        isDataSafe = false;

                //Should check scale
                bool checkScale = data.CastingShape.Equals(EPhysicsCastingShape.Box)
                || data.CastingShape.Equals(EPhysicsCastingShape.Sphere)
                || data.CastingShape.Equals(EPhysicsCastingShape.Capsule);

                //Check scale reference
                if (isDataSafe && checkScale && !data.ScaleTransform.CheckEditorReferences())
                    isDataSafe = false;

                //Check direction reference
                if (isDataSafe && !data.DirectionTransform.CheckEditorReferences())
                    isDataSafe = false;

                //Check orientation on box casting
                if (isDataSafe && data.CastingShape.Equals(EPhysicsCastingShape.Box)
                && !data.OrientationQuaternion.CheckEditorReferences())
                    isDataSafe = false;

                //Check distance
                if (isDataSafe)
                    isDataSafe = NumericTools.IsComparativeCorrect(data.MaxDistance, 0, ENumericConditional.Bigger,
                    " given distance must be bigger than 0 on entity: " + entityName);
            }

            return isDataSafe;
        }

        //Response to casting detections events
        private void ResponseCastingDetection(object sender, CastingDetectionArgs args)
        {
            //Check parameters
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
                //Convert data to filter data type
                if (TypeTools.ConvertToType(args.castingData, out IFilterData filterData,
                " couldnt convert castingData to filterData"))
                    //Notify filter system
                    new NotificationCommand<NotifyFilterSystemArgs>(sender,
                        new NotifyFilterSystemArgs(args.raycastHit.collider.gameObject, new IFilterData[] { filterData },
                        args.debugMessage + " couldnt notify filter system")).Execute();
        }

        //ISetState, set states from editor commands
        public void RespondEditorStateCommand<T3>(object sender, MonoBehaviour component) where T3 : ACastingState
        {
            //Convert to target component
            if (TypeTools.ConvertToType(component, out CastingComponent castingComponent,
            " couldnt convert component to castingComponent"))
                //Itinerate data on component
                foreach (ICastingData data in castingComponent.Data)
                    SetStateResponse<T3>(component, data);
        }
    }
}