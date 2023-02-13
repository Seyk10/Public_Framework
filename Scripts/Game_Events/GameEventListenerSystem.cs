using UnityEngine;
using MECS.Core;
using System;
using MECS.Patrons.Observers;
using MECS.Patrons.Commands;
using MECS.Events;
using System.Collections.Generic;
using static MECS.Tools.DebugTools;
using MECS.Tools;
using MECS.Collections;

namespace MECS.GameEvents
{
    //*System used to manage game event listeners
    //* T = IGameEventListenerData
    [CreateAssetMenu(fileName = "New_Game_Event_Listener_System", menuName = "MECS/Systems/Game_Event")]
    public class GameEventListenerSystem : ASystem<IGameEventListenerData>, IDisposable
    {
        //ASystem, make subscription
        protected override void OnEnable()
        {
            //ASystem, base execution
            base.OnEnable();

            //Set subscription to notifications
            NotificationCommand<NotificationRegisterListenerArgs>.NotificationEvent += RegisterSubject;
            NotificationCommand<NotificationUnregisterListenerArgs>.NotificationEvent += UnregisterSubject;
        }

        //ASystem, remove subscription
        protected override void OnDisable()
        {
            //ASystem, base execution
            base.OnDisable();

            //Set subscription to notifications
            NotificationCommand<NotificationRegisterListenerArgs>.NotificationEvent -= RegisterSubject;
            NotificationCommand<NotificationUnregisterListenerArgs>.NotificationEvent -= UnregisterSubject;

            //Clean values
            Dispose();
        }

        //Register subject on game event
        private void RegisterSubject(object sender, NotificationRegisterListenerArgs args)
        {
            //Check if parameters are valid
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
            {
                //Check data
                foreach (IGameEventListenerData data in args.component.Data)
                    //Try to convert type to component
                    if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                    args.debugMessage + " couldnt convert sender to component"))
                        //Avoid errors
                        if (IsValidData(entityComponent, data))
                        {
                            Subject subject =
                            new Subject(args.component, data, args.component.gameObject.name + args.component.gameObject.GetHashCode());

                            data.GameEvent.Observer.AddSubject(subject);
                        }
            }
        }

        //Unregister subject on game event
        private void UnregisterSubject(object sender, NotificationUnregisterListenerArgs args)
        {
            //Work data if values are safe
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters aren't valid"))
                //Check data
                foreach (IGameEventListenerData data in args.component.Data)
                    //Try convert sender to component
                    if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                    args.debugMessage + " couldnt convert sender to component"))
                        //Avoid errors
                        if (IsValidData(entityComponent, data))
                        {
                            //Get list to avoid errors
                            ISubject[] listenersArray = data.GameEvent.Observer.Listeners;

                            //Itinerate subjects
                            foreach (ISubject iSubject in listenersArray)
                                //Convert listener to subject
                                if (TypeTools.ConvertToType<Subject>(iSubject, out Subject subject,
                                args.debugMessage + " couldnt convert listener to Subject"))
                                    //Check if iSubject is subject and remove it
                                    if (subject.component == args.component)
                                        data.GameEvent.Observer.RemoveSubject(iSubject);
                        }
        }

        //Unregister subject on game event with data
        private void UnregisterSubject(object sender, IGameEventListenerData data)
        {
            //Avoid if values aren't safe
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { sender, data }, " given parameters aren't safe"))
                //Convert sender
                if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                " couldnt convert sender to component"))
                    //Avoid errors
                    if (IsValidData(entityComponent, data))
                    {
                        //Store list to avoid list modifications
                        ISubject[] listenersArray = data.GameEvent.Observer.Listeners;

                        //Convert to subject listener
                        if (TypeTools.ConvertToType<Subject>(sender, out Subject subject, " couldnt convert sender to subject"))
                            //Itinerate subjects
                            foreach (ISubject iSubject in listenersArray)
                                //Remove from observer if there is coincidence
                                if (subject.data == data)
                                    data.GameEvent.Observer.RemoveSubject(iSubject);
                    }
        }

        //ASystem method, check if data values are valid
        protected override bool IsValidData(Component entity, IGameEventListenerData data) =>
            //Check entity value
            ReferenceTools.IsValueSafe(entity, " given component isn't safe")

            //Check data value
            && ReferenceTools.IsValueSafe(data, " given data isn't safe")

            //Check game event value
            && ReferenceTools.IsValueSafe(data.GameEvent,
            " given IGameEventListenerData values aren't valid on entity " + entity.gameObject.name);

        //IDisposable, remove all the subjects from observers
        public void Dispose()
        {
            //Intenerate all data and remove
            foreach (KeyValuePair<IGameEventListenerData, MonoBehaviour> pair in AManager<IGameEventListenerData>.dataDictionary.dictionary)
                UnregisterSubject(this, pair.Key);
        }

        //Nested class, store subject values
        private class Subject : ISubject
        {
            //Variables
            public readonly GameEventListenerComponent component = null;
            public readonly IGameEventListenerData data = null;
            private readonly string subjectName = null;
            public string SubjectName => subjectName;

            //Default builder
            public Subject(GameEventListenerComponent component, IGameEventListenerData data, string subjectName)
            {
                this.component = component;
                this.data = data;
                this.subjectName = subjectName;
            }

            //ISubject, invoke event system response
            public void Respond()
            {
                //Convert data to IEventData
                if (TypeTools.ConvertToType(data, out IEventData eventData, " couldnt convert data to IEventData"))
                    //Notify event system
                    new NotificationCommand<NotifyEventsInvocationArgs>(component,
                        new NotifyEventsInvocationArgs(new IEventData[] { eventData }, " couldnt respond to game event")).Execute();
            }
        }
    }
}