using System.Collections.Generic;

namespace MECS.Patrons.Observers
{
    //* Interface used to set a event observer and notify subjects
    public interface IObserver
    {
        //Variables
        public ISubject[] Listeners { get; }

        //Methods
        //Add new subject
        public void AddSubject(ISubject subject);

        //Remove subject
        public void RemoveSubject(ISubject subject);

        //Notify subjects
        public void RaiseSubjects();
    }
}