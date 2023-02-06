using System.Collections.Generic;
using UnityEngine.Events;

namespace MECS.Events.Tracking
{
    //*Class used to store all the related info of a given unity event
    public class UnityEventInfo
    {
        //Variables
        private readonly List<UnityEventTargetInfo> targetsInfo = new();

        //Default builder
        public UnityEventInfo(UnityEvent unityEvent)
        {
            //Itinerate unity event count and get each value
            for (int index = 0; index < unityEvent.GetPersistentEventCount(); index++)
                targetsInfo.Add(new UnityEventTargetInfo
                (unityEvent.GetPersistentTarget(index).ToString()
                , unityEvent.GetPersistentListenerState(index).ToString()
                ,unityEvent.GetPersistentMethodName(index)));
        }

        //Attributes
        public UnityEventTargetInfo[] TargetsInfo => targetsInfo.ToArray();
    }
}