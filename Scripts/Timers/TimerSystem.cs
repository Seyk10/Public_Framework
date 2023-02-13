using MECS.Core;
using MECS.Patrons.StateMachine;
using UnityEngine;
using MECS.Patrons.Commands;
using MECS.Events;
using MECS.Collections;
using MECS.Tools;
using MECS.Conditionals;

namespace MECS.Timers
{
    //* System used to execute timers content
    //* T = ITimerData
    [CreateAssetMenu(fileName = "New_Timer_System", menuName = "MECS/Systems/Timer_System")]
    public class TimerSystem : ASystem<ITimerData>, ISetState<ITimerData, ATimerState>
    {
        //Subscribe to manager and add to commands observers
        protected override void OnEnable()
        {
            //ASystem base execution
            base.OnEnable();

            //Subscribe to timer commands
            InitializeTimerCommand.InitializeTimerEvent += RespondEditorStateCommand<InitializeTimerState>;
            PauseTimerCommand.PauseTimerEvent += RespondEditorStateCommand<PauseTimerState>;
            RunTimerCommand.RunTimerEvent += RespondEditorStateCommand<RunTimerState>;
            StopTimerCommand.StopTimerEvent += RespondEditorStateCommand<StopTimerState>;

            //Subscribe to timer manager            
            AManager<ITimerData>.dataDictionary.ElementAddedEvent += InitializeStateMachineStates;
        }

        //Unsubscribe from manager and remove to commands observers
        protected override void OnDisable()
        {
            //ASystem base execution
            base.OnDisable();

            //Unsubscribe from timer commands
            InitializeTimerCommand.InitializeTimerEvent -= RespondEditorStateCommand<InitializeTimerState>;
            PauseTimerCommand.PauseTimerEvent -= RespondEditorStateCommand<PauseTimerState>;
            RunTimerCommand.RunTimerEvent -= RespondEditorStateCommand<RunTimerState>;
            StopTimerCommand.StopTimerEvent -= RespondEditorStateCommand<StopTimerState>;

            //Unsubscribe to timer manager
            AManager<ITimerData>.dataDictionary.ElementAddedEvent -= InitializeStateMachineStates;
        }

        //ISetState, create all the necessary states and set these on data state machines
        public void InitializeStateMachineStates(object sender, ResponsiveDictionaryArgs<ITimerData, MonoBehaviour> args)
        {
            //Check parameters
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
            {
                //Store data
                ITimerData data = args.key;

                //Check if can add states
                if (!data.IsTimerInitialized)
                {
                    data.IsTimerInitialized = true;

                    //Create states
                    //Reset timer values
                    InitializeTimerState initializeTimerState = new InitializeTimerState(args.value, data);
                    //Pause timer values
                    PauseTimerState pauseTimerState = new PauseTimerState(args.value, data);
                    //Run timer values
                    RunTimerState runTimerState = new RunTimerState(args.value, data);
                    //Delete timer values
                    StopTimerState stopTimerState = new StopTimerState(args.value, data);

                    //Add states to state machine
                    data.StateMachine.AddState(initializeTimerState);
                    data.StateMachine.AddState(pauseTimerState);
                    data.StateMachine.AddState(runTimerState);
                    data.StateMachine.AddState(stopTimerState);

                    //Subscribe to run timer
                    runTimerState.TimerEndsEvent += ResponseTimerEnds;
                }
            }
        }

        //ISetState, set timers states on component data
        public void SetStateResponse<T3>(object sender, ITimerData data) where T3 : ATimerState
        {
            //Check given parameters
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { sender, data }, " given parameters aren't safe"))
            {
                //Store state machine
                TimerStateMachine stateMachine = data.StateMachine;

                //Local method, run or set state
                void SetCurrentState()
                {
                    //Avoid repeat timers and execute it if its the same
                    if (ReferenceTools.IsValueSafe(stateMachine)
                    && stateMachine.CurrentState is T3)
                        //Run current state
                        stateMachine.CurrentState.RunState();
                    else
                        //Check if timer has state
                        stateMachine.SetState<T3>();
                }

                //Check if state machine contains state type, initialize if it does not exist
                if (stateMachine.ContainsState<T3>())
                    SetCurrentState();
                //Try to convert sender to monoBehaviour
                else if (TypeTools.ConvertToType<MonoBehaviour>(sender, out MonoBehaviour entityMonoBehaviour,
                " couldnt convert sender to MonoBehaviour"))
                {
                    //Set state machine initialization
                    InitializeStateMachineStates(sender,
                    new ResponsiveDictionaryArgs<ITimerData, MonoBehaviour>(data, entityMonoBehaviour,
                    " couldnt initialize state machine on entity: "
                    + entityMonoBehaviour.name));

                    //Set state
                    SetCurrentState();
                }
            }
        }

        //Invoke events when timer ends
        private void ResponseTimerEnds(object sender, ITimerData data)
        {
            //Check parameters
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { sender, data }, " given parameters aren't safe"))
                //Try to convert data to event data
                if (TypeTools.ConvertToType(data, out IEventData eventData, " couldnt convert data to IEventData from "
                + sender.ToString()))

                    //Notify event system
                    new NotificationCommand<NotifyEventsInvocationArgs>(sender,
                        new NotifyEventsInvocationArgs(new IEventData[] { eventData }, " couldnt raise event data values"))
                        .Execute();
        }

        //ASystem, checks if data references are valid
        protected override bool IsValidData(Component entityComponent, ITimerData data) =>
            //Check component
            ReferenceTools.IsValueSafe(entityComponent, " entityComponent isn't safe")

            //Check data
            && ReferenceTools.IsValueSafe(data, " ITimerData isn't safe")

            //Check timer value
            && NumericTools.IsComparativeCorrect(data.Time.Value, 0, ENumericConditional.Bigger,
            " time value must be bigger than 0 on: " + entityComponent.gameObject.name);

        //ISetState, set states from editor commands
        public void RespondEditorStateCommand<T3>(object sender, MonoBehaviour component) where T3 : ATimerState
        {
            //Check parameters
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { sender, component }, " given parameters aren't safe"))
                //Convert component to timer component
                if (TypeTools.ConvertToType(component, out TimerComponent timerComponent,
                " failed to convert MonoBehaviour to timer component"))
                    //Itinerate data on component
                    foreach (ITimerData data in timerComponent.Data)
                        SetStateResponse<T3>(component, data);
        }
    }
}