using MECS.Core;
using UnityEngine;

namespace MECS.Events
{
    //* Profile used to store data related to events
    [CreateAssetMenu(fileName = "New_Event_Profile", menuName = "MECS/Profiles/Event")]
    public class EventProfile : AProfile<EventData> { }
}