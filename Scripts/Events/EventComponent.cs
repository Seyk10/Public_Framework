using MECS.Core;

namespace MECS.Events
{
    //* Component used to store events on entity   
    //* T = Data type
    //* T2 = Profile type
    public class EventComponent : AComponent<EventData, EventProfile> { }
}