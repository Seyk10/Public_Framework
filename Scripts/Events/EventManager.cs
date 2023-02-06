using MECS.Core;
using UnityEngine;

namespace MECS.Events
{
    //* Manager used to register event data values
    //* T = IEventData
    [CreateAssetMenu(fileName = "New_Event_Manager", menuName = "MECS/Managers/Event")]
    public class EventManager : AManager<IEventData> { }
}