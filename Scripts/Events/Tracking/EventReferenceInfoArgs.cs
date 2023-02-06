using System;
using System.Reflection;

namespace MECS.Events.Tracking
{
    //* Args used to share information about event reference invocation
    //TODO: Add debugging complex information
    public class EventReferenceInfoArgs : EventArgs
    {
        //Variables
        private readonly UnityEventInfo unityEventInfo = null;
        private readonly MethodInfo methodInfo = null;
        private readonly string raisingEntityName = null,
            raisingComponentName = null;

        //Default builder
        public EventReferenceInfoArgs(UnityEventInfo unityEventInfo, MethodInfo methodInfo, string raisingEntityName, string raisingComponentName)
        {
            this.unityEventInfo = unityEventInfo;
            this.methodInfo = methodInfo;
            this.raisingEntityName = raisingEntityName;
            this.raisingComponentName = raisingComponentName;
        }

        //Attributes
        public UnityEventInfo UnityEventInfo => unityEventInfo;

        public MethodInfo MethodInfo => methodInfo;

        public string RaisingEntityName => raisingEntityName;

        public string RaisingComponentName => raisingComponentName;
    }
}