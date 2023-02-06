namespace MECS.GameEvents
{
    //* Notification args used on un-registration
    public class NotificationUnregisterListenerArgs : ANotificationListenerArgs
    {
        //Default builder
        public NotificationUnregisterListenerArgs(GameEventListenerComponent component) : base(component) { }
    }
}