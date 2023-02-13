using System;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Patrons.Singleton
{
    //* Command used to make a safe save of singleton reference
    //* T = Singleton type
    public class CreateSingletonCommand<T> : ICommandReturn<T>
    {
        //Variables
        //Store singleton values to use
        private readonly T singletonReference;
        private readonly T newReference;

        //Notify commands ends
        public event EventHandler<T> CommandFinishedEvent = null;

        //Builder
        //Used to get target references
        public CreateSingletonCommand(T singletonReference, T newReference)
        {
            this.singletonReference = singletonReference;
            this.newReference = newReference;
        }

        //Execute command content
        public T Execute()
        {
            //Reference to return
            T returnValue = default;

            //Create singleton or destroy gameObject instance
            if (SingletonTools.CanSetInstance(singletonReference, newReference)) returnValue = newReference;
            else
            {
                //Return same singleton value
                returnValue = singletonReference;
#if UNITY_EDITOR
                //Check if references are equal
                if (singletonReference.Equals(newReference))
                    SingletonTools.DebugDuplicateSingleton();
                else if (ReferenceTools.IsValueSafe(singletonReference))
                    new NotificationCommand<DebugArgs>(this,
                    new DebugArgs(" singleton reference inst empty", LogType.Error, new System.Diagnostics.StackTrace(true))).Execute();
                else
                    ReferenceTools.IsValueSafe(newReference, " newReference isn't valid");
#endif
            }

            CommandFinishedEvent?.Invoke(this, returnValue);

            return returnValue;
        }
    }
}