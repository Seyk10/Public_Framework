using System;
using System.Reflection;
using MECS.Collections;
using MECS.Events.Tracking;
using MECS.Tools;
using UnityEngine;
using UnityEngine.Events;
using static MECS.Tools.DebugTools;

namespace MECS.Events
{
    //* Structure used to hold reference to unity event and actions events
    [Serializable]
    public class EventReference
    {
        //Editor variables
        [SerializeField] private UnityEvent unityEventResponse = new();
        public UnityEvent UnityEventResponse => unityEventResponse;

        //Variables
        private Action actionResponse = null;
        private bool isActionOnUse = false;

        //Attributes
        public Action ActionResponse
        {
            get => actionResponse;
            set
            {
                //Check if new value is null or not
                isActionOnUse = ReferenceTools.IsValueSafe(value);

                actionResponse = value;
            }
        }
        public bool IsActionOnUse => isActionOnUse;

        //Event, notify that all events were raised
        public static event EventHandler<EventReferenceInfoArgs> EventReferenceRaisedInfoEvent = null;

        //Method, raise all the events and notify it
        public void RaiseEvents(string raisingEntityName, string raisingComponentName, string debugMessage)
        {
            //Check given parameters
            bool areParametersValid =
                CollectionsTools.arrayTools.HasArrayObject(new object[] { raisingEntityName, raisingComponentName },
                debugMessage + " given parameters aren't safe");

            //Raise if parameters are ok
            if (areParametersValid)
            {
                //Raise info event
                EventReferenceRaisedInfoEvent?.Invoke(this,
                new EventReferenceInfoArgs(new UnityEventInfo(unityEventResponse), actionResponse != null
                ? actionResponse.GetMethodInfo() : null, raisingEntityName, raisingComponentName));

                //Raise unity event
                unityEventResponse?.Invoke();

                //Invoke only if its on use
                if (isActionOnUse)
                    actionResponse?.Invoke();
            }
        }
    }
}