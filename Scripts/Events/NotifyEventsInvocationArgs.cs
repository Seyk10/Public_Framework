using MECS.Collections;
using MECS.Tools;

namespace MECS.Events
{
    //* Args used to notify event system to raise these    
    public class NotifyEventsInvocationArgs : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly IEventData[] iEventDataArray = null;

        //Default builder
        public NotifyEventsInvocationArgs(IEventData[] iEventDataArray, string debugMessage) : base(debugMessage) =>
            this.iEventDataArray = iEventDataArray;

        //IValuesChecking method, check if values given on args are valid
        public bool AreValuesValid() =>
            //Check array of event data
            CollectionsTools.arrayTools.IsArrayContentSafe(iEventDataArray, " event data array isn't safe");
    }
}