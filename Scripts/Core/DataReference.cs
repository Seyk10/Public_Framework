using System;
using MECS.Collections;
using MECS.LifeCycle;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Class used to manage variable and constant data structures
    //* T = Data type
    //* T2 = Profile type
    [Serializable]
    public class DataReference<T, T2> where T : AData where T2 : AProfile<T>
    {
        //Editor variables
        public bool useLocalValues = true;
        public T[] localValue = default;
        public T2 externalValue = default;

        //Methods
        //Return the correct value to use on data reference
        public T[] GetValue()
        {
            //value to return
            T[] returnValue = null;

            //Set value to return or debug missing reference
            if (useLocalValues)
                returnValue = localValue;
            else if (externalValue != null)
                returnValue = externalValue.Data;
            else
                ReferenceTools.DebugWarningNoValidReference("DataReference", "GetValue()", "externalValue");

            return returnValue;
        }

        //Method, notify data phase event
        public void NotifyDataPhase(MonoBehaviour sender, ELifeCyclePhase lifeCyclePhase,
        ComplexDebugInformation complexDebugInformation)
        {
            //Check parameters
            bool areParametersValid =
            ReferenceTools.AreValuesSafe(new object[] { sender, lifeCyclePhase, complexDebugInformation },
            new ComplexDebugInformation(this.GetType().Name,
                 "NotifyDataPhase(MonoBehaviour sender, ELifeCyclePhase lifeCyclePhase, "
                + "ComplexDebugInformation complexDebugInformation)", "given parameters aren't safe"));

            //Execute notifications cases
            if (areParametersValid)
            {
                //Values to notify
                T[] dataValues = GetValue();

                //Check if data array is safe
                if (CollectionsTools.arrayTools
                .IsArrayContentSafe(dataValues, complexDebugInformation.AddTempCustomText("data array isn't safe")))
                    //Switch data phase cases and call executions
                    switch (lifeCyclePhase)
                    {
                        case ELifeCyclePhase.Awake:
                            //Itinerate and execute notification
                            foreach (T data in dataValues)
                                data.NotifyDataAwake(sender, complexDebugInformation);
                            break;

                        case ELifeCyclePhase.Destroy:
                            //Itinerate and execute notification
                            foreach (T data in dataValues)
                                data.NotifyDataDestroy(sender, complexDebugInformation);
                            break;

                        case ELifeCyclePhase.Disable:
                            //Itinerate and execute notification
                            foreach (T data in dataValues)
                                data.NotifyDataDisable(sender, complexDebugInformation);
                            break;

                        case ELifeCyclePhase.Enable:
                            //Itinerate and execute notification
                            foreach (T data in dataValues)
                                data.NotifyDataEnable(sender, complexDebugInformation);
                            break;

                        case ELifeCyclePhase.NULL:
                            DebugTools.DebugError(complexDebugInformation
                            .AddTempCustomText("cant notify data with life cycle phase NULL value"));
                            break;

                        case ELifeCyclePhase.Start:
                            DebugTools.DebugError(complexDebugInformation
                                                    .AddTempCustomText("cant notify data with life cycle phase START value"));
                            break;
                    }
            }
        }
    }
}