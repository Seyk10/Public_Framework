using System.Diagnostics;
using MECS.Collections;
using MECS.Events;
using MECS.Patrons.Commands;
using MECS.Tools;

namespace MECS.LifeCycle
{
    //* Base args class used on notification phases args
    public class LifeCycleNotificationArgs : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly ILifeCycleData[] iLifeCycleDataArray = null;
        public readonly ELifeCyclePhase lifeCyclePhase = ELifeCyclePhase.NULL;

        //Default builder
        public LifeCycleNotificationArgs(ILifeCycleData[] iLifeCycleDataArray, ELifeCyclePhase lifeCyclePhase,
        string debugMessage) : base(debugMessage)
        {
            this.iLifeCycleDataArray = iLifeCycleDataArray;
            this.lifeCyclePhase = lifeCyclePhase;
        }

        //IValuesChecking method, check each args value
        public bool AreValuesValid()
        {
            //Return value            
            //Check data array
            bool areValuesSafe = CollectionsTools.arrayTools.IsArrayContentSafe(iLifeCycleDataArray,
            " given life cycle data array isn't safe");

            //Check life cycle phase
            if (areValuesSafe)
                //Life cycle phase shouldn't be null
                if (lifeCyclePhase.Equals(ELifeCyclePhase.NULL))
                {
                    areValuesSafe = false;

                    //Notify debug manager
                    new NotificationCommand<DebugArgs>(this, new DebugArgs(" given lifeCyclePhase is null",
                        UnityEngine.LogType.Error, new StackTrace(true)))
                        .Execute();
                }

            return areValuesSafe;
        }
    }
}