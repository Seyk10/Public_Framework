using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MECS.Events.Tracking
{
    //* Manager used to display on editor a register of all invocations of EventsReferences
    [CreateAssetMenu(fileName = "New_Event_Tracking_Manager", menuName = "MECS/Managers/Event_Tracking_Manager")]
    public class EventTrackingManager : ScriptableObject
    {
        //Editor variables
        [SerializeField] private EventReferenceInfoDisplay[] eventInfoDisplays = null;

        //Events, notify that tracking is cleaning
        public static event Action CleanTrackingInfoEvent = null;

        //Avoid tracking out of unity editor 
#if UNITY_EDITOR
        //ScriptableObject        
        //Make subscriptions to keep tracking on events raising
        private void OnEnable()
        {
            CleanTrackingInfoEvent += CleanInformationDisplay;
            EventReference.EventReferenceRaisedInfoEvent += UpdateEventInfoDisplay;
        }

        //Remove subscriptions to keep tracking on events raising
        private void OnDisable()
        {
            CleanTrackingInfoEvent -= CleanInformationDisplay;
            EventReference.EventReferenceRaisedInfoEvent -= UpdateEventInfoDisplay;
        }

        //Static method, used to clean tracking information from editor menu
        [MenuItem("MECS/Event Tracking/Clean Information")]
        private static void MenuCleanTracking() => CleanTrackingInfoEvent?.Invoke();
#endif

        //Method, update editor information of array eventInfoDisplays with new invocations
        private void UpdateEventInfoDisplay(object sender, EventReferenceInfoArgs args)
        {
            //Make a temp list to add new information
            List<EventReferenceInfoDisplay> tempDisplayInfo = eventInfoDisplays == null ? new() : eventInfoDisplays.ToList();

            //Add new information
            EventReferenceInfoDisplay newEventReferenceInfoDisplay =
            new EventReferenceInfoDisplay(args.RaisingEntityName, args.RaisingComponentName, args.UnityEventInfo.TargetsInfo,
            args.MethodInfo != null ? args.MethodInfo.Name : null);

            tempDisplayInfo.Add(newEventReferenceInfoDisplay);

            //Set new display info
            eventInfoDisplays = tempDisplayInfo.ToArray();
        }

        //Clean information on response to event
        private void CleanInformationDisplay() => eventInfoDisplays = null;
    }
}