namespace MECS.GameEvents
{
    //* Notification args used on registration
    public class NotificationRegisterListenerArgs : ANotificationListenerArgs
    {
        //Default builder
        public NotificationRegisterListenerArgs(GameEventListenerComponent component, string debugMessage) :
        base(component, debugMessage)
        { }
    }
}