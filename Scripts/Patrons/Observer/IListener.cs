namespace MECS.Patrons.Observers
{
    //* Interface used on structures with event subscriptions
    public interface IListener
    {
        //Variables
        public bool IsSubscribed { get; }
        //Methods
        //Set subscriptions
        public void Subscribe();

        //Remove subscriptions
        public void Unsubscribe();
    }
}