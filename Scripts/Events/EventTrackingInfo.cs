using MECS.Core;
using UnityEngine;

namespace MECS.Events
{
    //* Tracking information for IEventData
    public class EventTrackingInfo : ADataTrackingInfo, IEventData
    {
        //Editor variables
        [Header("Event tracking info")]
        [SerializeReference] private EventReference eventReference = null;

        //Attributes
        public EventReference EventReference => eventReference;

        //Base builder
        public EventTrackingInfo(string entityName, EventReference eventReference) : base(entityName) =>
        this.eventReference = eventReference;
    }
}