using System.Collections.Generic;

namespace MECS.Patrons.Observers
{
    //* Interface used to set a event observer and notify subjects with parameters
    //* T = Value to share 
    public interface IObserverParameter<T>
    {
        //Variables
        public List<ISubjectParameter<T>> Listeners { get; }

        //Methods
        //Add new subject
        public void AddSubject(ISubjectParameter<T> subject);

        //Remove subject
        public void RemoveSubject(ISubjectParameter<T> subject);

        //Notify subjects
        public void RaiseSubjects(T value);
    }
}