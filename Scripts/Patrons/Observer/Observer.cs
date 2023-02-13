using System;
using System.Collections.Generic;
using MECS.Collections;

namespace MECS.Patrons.Observers
{
    //* Object used to store subjects and raise their calls
    public class Observer : IObserver, IDisposable
    {
        //Variables
        //Store subjects
        private readonly List<ISubject> listeners = new();
        public ISubject[] Listeners => listeners.ToArray();

        //Events, notify information of listeners
        public event EventHandler<string> ListenerAdditionEvent = null,
        ListenerRemoveEvent = null;

        //Add a new listener
        public void AddSubject(ISubject subject)
        {
            //Notify if listener is added
            if (CollectionsTools.listTools.AddValue(listeners, subject, " couldnt add subject to"))
                ListenerAdditionEvent?.Invoke(this, subject.SubjectName);
        }

        //IDisposable, clean collections
        public void Dispose() => listeners.Clear();

        //Notify to all the listeners to raise responses
        public void RaiseSubjects()
        {
            //Avoid if there aren't listeners
            if (listeners.Count > 0)
                //Itinerate listeners and raise responds
                if (CollectionsTools.listTools.GetAuxiliaryList(listeners, out List<ISubject> auxiliaryList,
                " couldnt copy listeners list"))
                    foreach (ISubject subject in auxiliaryList)
                        subject.Respond();
        }

        //Remove a listener
        public void RemoveSubject(ISubject subject)
        {
            //Avoid if there aren't listeners
            if (listeners.Count > 0)
                //Notify if listener is removed
                if (CollectionsTools.listTools.RemoveValue(listeners, subject, " couldnt remove subject from listeners"))
                    ListenerRemoveEvent?.Invoke(this, subject.SubjectName);
        }
    }
}