using System;
using MECS.Collections;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Events
{
    //* Args used to notify event system to raise these    
    public class NotifyEventsInvocationArgs : EventArgs, IValuesChecking
    {
        //Variables
        public readonly IEventData[] iEventDataArray = null;
        public readonly ComplexDebugInformation complexDebugInformation = null;

        //Default builder
        public NotifyEventsInvocationArgs(IEventData[] iEventDataArray, ComplexDebugInformation complexDebugInformation)
        {
            this.iEventDataArray = iEventDataArray;
            this.complexDebugInformation = complexDebugInformation;
        }

        //IValuesChecking method, check if values given on args are valid
        public bool AreValuesValid()
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AreValuesValid()");

            //Check array of event data
            return CollectionsTools.arrayTools.IsArrayContentSafe(iEventDataArray, new ComplexDebugInformation(basicDebugInformation,
            "event data array isn't safe"))

            //Check debug information
            && ReferenceTools.IsValueSafe(this.complexDebugInformation, new ComplexDebugInformation(basicDebugInformation,
            "debug information isn't safe"));
        }
    }
}