namespace MECS.Patrons.Observers
{
    //* Interface used to response on Observer Parameter notification
    //* T = Parameter to receive
    public interface ISubjectParameter<T>
    {
        //Respond on observer notification
        public void Respond(T value);
    }
}