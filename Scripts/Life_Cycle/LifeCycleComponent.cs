using MECS.Core;
using MECS.Patrons.Commands;
using UnityEngine;
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
                new LifeCycleNotificationArgs(DataReference.GetValue(), ELifeCyclePhase.Awake,
                new ComplexDebugInformation(basicDebugInformation, "couldnt invoke awake responses")), basicDebugInformation)
                .Execute();
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
                new LifeCycleNotificationArgs(DataReference.GetValue(), ELifeCyclePhase.Enable,
                new ComplexDebugInformation(basicDebugInformation, "couldnt invoke OnEnable responses")), basicDebugInformation)
                .Execute();
        }

        //MonoBehaviour, raise response on start state    
        private void Start()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnEnable()");

            //Notify phase
            new NotificationCommand<LifeCycleNotificationArgs>(this,
                new LifeCycleNotificationArgs(DataReference.GetValue(), ELifeCyclePhase.Start,
                new ComplexDebugInformation(basicDebugInformation, "couldnt invoke Start responses")), basicDebugInformation)
                .Execute();
        }

        //MonoBehaviour, raise response on disable state
        protected override void OnDisable()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnEnable()");

            //Notify phase            
            new NotificationCommand<LifeCycleNotificationArgs>(this,
                new LifeCycleNotificationArgs(DataReference.GetValue(), ELifeCyclePhase.Disable,
                new ComplexDebugInformation(basicDebugInformation, "couldnt invoke OnDisable responses")), basicDebugInformation)
                .Execute();

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
                new LifeCycleNotificationArgs(DataReference.GetValue(), ELifeCyclePhase.Destroy,
                new ComplexDebugInformation(basicDebugInformation, "couldnt invoke OnDestroy responses")), basicDebugInformation)
                .Execute();

            //base AComponent execution
            base.OnDestroy();
        }
    }
}