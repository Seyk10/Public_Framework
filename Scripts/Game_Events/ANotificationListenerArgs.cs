using System;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.GameEvents
{
    //* Base class of game event listeners notification args
    public abstract class ANotificationListenerArgs : EventArgs, IValuesChecking
    {
        //Store component to register
        public readonly GameEventListenerComponent component = null;

        //Default builder
        public ANotificationListenerArgs(GameEventListenerComponent component) => this.component = component;

        //Method, check if args values
        public virtual bool AreValuesValid() =>
            //Check component
            ReferenceTools.IsValueSafe(component,
            new ComplexDebugInformation(this.GetType().Name, "AreValuesValid()",
            "component isn't valid"));
    }
}