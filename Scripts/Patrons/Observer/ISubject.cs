namespace MECS.Patrons.Observers
{
    //* Interface used to response on observer notification
    public interface ISubject
    {
        //Store subject name
        public string SubjectName { get; }
        //Respond on observer notification
        public void Respond();
    }
}