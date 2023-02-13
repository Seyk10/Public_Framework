using MECS.Events;
using MECS.Tools;

namespace MECS.GameEvents
{
    //* Base class of game event listeners notification args
    public abstract class ANotificationListenerArgs : AEventArgs, IValuesChecking
    {
        //Store component to register
        public readonly GameEventListenerComponent component = null;

        //Default builder
        public ANotificationListenerArgs(GameEventListenerComponent component, string debugMessage) : base(debugMessage) =>
        this.component = component;

        //Method, check if args values
        public virtual bool AreValuesValid() =>
            //Check component
            ReferenceTools.IsValueSafe(component, " component isn't valid");
    }
}