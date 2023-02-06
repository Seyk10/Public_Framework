using System.Collections.Generic;
using MECS.Collections;
using static MECS.Tools.DebugTools;

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
        public void AddSubject(ISubjectParameter<T> subject) => CollectionsTools.listTools.AddValue(listeners, subject,
        new ComplexDebugInformation(this.GetType().Name, "AddSubject(ISubjectParameter<T> subject)",
        "couldnt add subject to listeners"));

        //Notify to all the listeners when game event
        public void RaiseSubjects(T value)
        {
            //Itinerate listeners and raise responds
            if (CollectionsTools.listTools.GetAuxiliaryList(listeners, out List<ISubjectParameter<T>> auxiliaryList,
             new ComplexDebugInformation(this.GetType().Name, "RaiseSubjects(T value)", "couldnt copy listeners list")))
                foreach (ISubjectParameter<T> subject in auxiliaryList)
                    subject.Respond(value);
        }

        //Remove subject
        public void RemoveSubject(ISubjectParameter<T> subject) => CollectionsTools.listTools.RemoveValue(listeners, subject,
        new ComplexDebugInformation(this.GetType().Name, "RemoveSubject(ISubjectParameter<T> subject)",
        "couldnt remove subject from listeners"));
    }
}