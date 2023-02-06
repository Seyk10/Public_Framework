using MECS.Core;
using MECS.Patrons.StateMachine;
using UnityEngine;
using MECS.Patrons.Commands;
using MECS.Events;
using MECS.Collections;
using static MECS.Tools.DebugTools;
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
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "(object sender, ResponsiveDictionaryArgs<ITimerData, MonoBehaviour> args)");

            //Check parameters
            if (ReferenceTools.AreEventParametersValid(sender, args,
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't valid")))
            {
                //Store data
                ITimerData data = args.key;

                //Check if can add states
                if (!data.IsTimerInitialized)
                {
                    data.IsTimerInitialized = true;

                    //Create states
                    //Reset timer values
                    InitializeTimerState initializeTimerState = new InitializeTimerState(args.value, data, args.complexDebugInformation
                    .AddTempCustomText("couldnt set initialize timer state"));
                    //Pause timer values
                    PauseTimerState pauseTimerState = new PauseTimerState(args.value, data, args.complexDebugInformation
                    .AddTempCustomText("couldnt set pause timer state"));
                    //Run timer values
                    RunTimerState runTimerState = new RunTimerState(args.value, data, args.complexDebugInformation
                    .AddTempCustomText("couldnt set run timer state"));
                    //Delete timer values
                    StopTimerState stopTimerState = new StopTimerState(args.value, data, args.complexDebugInformation
                    .AddTempCustomText("couldnt set stop timer state"));

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
            //Store state machine
            TimerStateMachine stateMachine = data.StateMachine;

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

            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "SetStateResponse<T3>(object sender, ITimerData data)");

            //Check if state machine contains state type, initialize if it does not exist
            if (stateMachine.ContainsState<T3>())
                SetCurrentState();
            //Try to convert sender to monoBehaviour
            else if (TypeTools.ConvertToType<MonoBehaviour>(sender, out MonoBehaviour entityMonoBehaviour,
            new ComplexDebugInformation(basicDebugInformation, "couldnt convert sender to MonoBehaviour")))
            {
                //Set state machine initialization
                InitializeStateMachineStates(sender,
                new ResponsiveDictionaryArgs<ITimerData, MonoBehaviour>(data, entityMonoBehaviour,
                new ComplexDebugInformation(basicDebugInformation, "couldnt initialize state machine on entity: "
                + entityMonoBehaviour.name)));

                //Set state
                SetCurrentState();
            }
        }

        //Invoke events when timer ends
        private void ResponseTimerEnds(object sender, ITimerData data)
        {
            //Convert
            IEventData[] eventData = { data as IEventData };

            //Check if can invoke
            if (eventData[0] != null)
            {
                //Debug information
                BasicDebugInformation basicDebugInformation = new("TimerSystem",
                 "ResponseTimerEnds(object sender, ITimerData data)");

                //Notify event system
                new NotificationCommand<NotifyEventsInvocationArgs>(sender,
                    new NotifyEventsInvocationArgs(eventData,
                    new ComplexDebugInformation(basicDebugInformation, "couldnt raise event data values")), basicDebugInformation)
                    .Execute();
            }
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: Timer data doesnt implement IEventData interface " + sender.ToString());
#endif
        }

        //ASystem, checks if data references are valid
        protected override bool IsValidData(Component entityComponent, ITimerData data,
        ComplexDebugInformation complexDebugInformation) =>
            //Check component
            ReferenceTools.IsValueSafe(entityComponent, complexDebugInformation.AddTempCustomText("entityComponent isn't safe"))

            //Check data
            && ReferenceTools.IsValueSafe(data, complexDebugInformation.AddTempCustomText("ITimerData isn't safe"))

            //Check timer value
            && NumericTools.IsComparativeCorrect(data.Time.Value, 0, ENumericConditional.Bigger,
            complexDebugInformation.AddTempCustomText("time value must be bigger than 0 on: " + entityComponent.gameObject.name));

        //ISetState, set states from editor commands
        public void RespondEditorStateCommand<T3>(object sender, MonoBehaviour component) where T3 : ATimerState
        {
            //Convert to target component
            TimerComponent timerComponent = component as TimerComponent;

            //Avoid errors
            if (timerComponent != null)
                //Itinerate data on component
                foreach (ITimerData data in timerComponent.DataReference.GetValue())
                    SetStateResponse<T3>(component, data);
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: Failed to convert MonoBehaviour to timer component");
#endif
        }
    }
}