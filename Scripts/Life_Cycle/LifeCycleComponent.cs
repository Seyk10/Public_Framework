using MECS.Core;
using MECS.Patrons.Commands;
using static MECS.Tools.DebugTools;

namespace MECS.LifeCycle
{
    //* Raise event response on component life cycle phases
    //* T = LifeCycleData
    //* T2 = LifeCycleProfile
    public class LifeCycleComponent : AComponent<LifeCycleData, LifeCycleProfile>
    {
        //MonoBehaviour, raise response on awake state
        protected override void Awake()
        {
            //base AComponent execution
            base.Awake();

            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "Awake()");

            //Notify phase
            new NotificationCommand<LifeCycleNotificationArgs>(this,
                new LifeCycleNotificationArgs(Data, ELifeCyclePhase.Awake,
                " couldnt invoke awake responses")).Execute();
        }

        //MonoBehaviour, raise response on enable state
        protected override void OnEnable()
        {
            //base AComponent execution
            base.OnEnable();

            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnEnable()");

            //Notify phase
            new NotificationCommand<LifeCycleNotificationArgs>(this,
                new LifeCycleNotificationArgs(Data, ELifeCyclePhase.Enable,
                " couldnt invoke OnEnable responses")).Execute();
        }

        //MonoBehaviour, raise response on start state    
        private void Start()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnEnable()");

            //Notify phase
            new NotificationCommand<LifeCycleNotificationArgs>(this,
                new LifeCycleNotificationArgs(Data, ELifeCyclePhase.Start,
                " couldnt invoke Start responses")).Execute();
        }

        //MonoBehaviour, raise response on disable state
        protected override void OnDisable()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnEnable()");

            //Notify phase            
            new NotificationCommand<LifeCycleNotificationArgs>(this,
                new LifeCycleNotificationArgs(Data, ELifeCyclePhase.Disable,
                " couldnt invoke OnDisable responses")).Execute();

            //base AComponent execution
            base.OnDisable();
        }

        //MonoBehaviour, raise response on OnDestroy state
        protected override void OnDestroy()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnEnable()");

            //Notify phase
            new NotificationCommand<LifeCycleNotificationArgs>(this,
                new LifeCycleNotificationArgs(Data, ELifeCyclePhase.Destroy,
                " couldnt invoke OnDestroy responses")).Execute();

            //base AComponent execution
            base.OnDestroy();
        }
    }
}