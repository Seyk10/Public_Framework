namespace MECS.GameEvents
{
    //* Interface used to set the variables used on game event listener data type structures
    public interface IGameEventListenerData
    {
        //Variables
        public GameEvent GameEvent { get; }
        //TODO: Add subscriptions with life cycle integration
    }
}