namespace MECS.Events
{
    //* Interface used to set event data values
    public interface IEventData
    {
        //Variables
        //Store events to raise
        public EventReference EventReference { get; }
    }
}