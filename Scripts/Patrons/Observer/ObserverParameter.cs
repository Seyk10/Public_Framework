using System.Collections.Generic;
using MECS.Collections;

namespace MECS.Patrons.Observers
{
    //* Object used to store subjects and raise their calls with a parameter
    public class ObserverParameter<T> : IObserverParameter<T>
    {
        //Variables
        //Store subjects
        private readonly List<ISubjectParameter<T>> listeners = new();
        public List<ISubjectParameter<T>> Listeners => listeners;

        //Add a new listener
        public void AddSubject(ISubjectParameter<T> subject) =>
            CollectionsTools.listTools.AddValue(listeners, subject, " couldnt add subject to listeners");

        //Notify to all the listeners when game event
        public void RaiseSubjects(T value)
        {
            //Itinerate listeners and raise responds
            if (CollectionsTools.listTools.GetAuxiliaryList(listeners, out List<ISubjectParameter<T>> auxiliaryList,
            " couldnt copy listeners list"))
                foreach (ISubjectParameter<T> subject in auxiliaryList)
                    subject.Respond(value);
        }

        //Remove subject
        public void RemoveSubject(ISubjectParameter<T> subject) =>
            CollectionsTools.listTools.RemoveValue(listeners, subject, " couldnt remove subject from listeners");
    }
}