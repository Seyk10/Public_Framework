using System;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Core
{
    [Serializable]
    //*Abstract class used as base of data type classes
    public abstract class AData
    {
        //Editor variables
        [Tooltip("Data name association")]
        [TextArea(3, 7)]
#pragma warning disable 0414
        [SerializeField] private string information = null;
#pragma warning disable 0414

        //Method, notify data phase

        protected void NotifyDataPhase<T, T2>(MonoBehaviour sender, T args) where T : AEntityArgs<T2>
        {
            //Check if event notification parameters are valid
            if (ReferenceTools.AreEventParametersValid(sender, args, " data phase notification event parameters aren't valid"))
                //Notify data phase
                new NotificationCommand<T>(sender, args).Execute();
        }

        //Method, used to notify awake data phases
        public abstract void NotifyDataAwake(MonoBehaviour sender);

        //Method, used to notify enable data phases
        public abstract void NotifyDataEnable(MonoBehaviour sender);

        //Method, used to notify awake disable phases
        public abstract void NotifyDataDisable(MonoBehaviour sender);

        //Method, used to notify awake data phases
        public abstract void NotifyDataDestroy(MonoBehaviour sender);
    }
}