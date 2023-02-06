using System;
using MECS.Collections;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.LifeCycle
{
    //* Base args class used on notification phases args
    public class LifeCycleNotificationArgs : EventArgs, IValuesChecking
    {
        //Variables
        public readonly ILifeCycleData[] iLifeCycleDataArray = null;
        public readonly ELifeCyclePhase lifeCyclePhase = ELifeCyclePhase.NULL;
        public readonly ComplexDebugInformation complexDebugInformation = null;

        //Default builder
        public LifeCycleNotificationArgs(ILifeCycleData[] iLifeCycleDataArray, ELifeCyclePhase lifeCyclePhase,
        ComplexDebugInformation complexDebugInformation)
        {
            this.iLifeCycleDataArray = iLifeCycleDataArray;
            this.lifeCyclePhase = lifeCyclePhase;
            this.complexDebugInformation = complexDebugInformation;
        }

        //IValuesChecking method, check each args value
        public bool AreValuesValid()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AreValuesValid()");

            //Return value
            bool areValuesSafe =
                //Check data array
                CollectionsTools.arrayTools.IsArrayContentSafe(iLifeCycleDataArray, new ComplexDebugInformation(basicDebugInformation,
                "given life cycle data array isn't safe"))

                //Check if debug information is safe
                && ReferenceTools.IsValueSafe(this.complexDebugInformation,
                new ComplexDebugInformation(basicDebugInformation,
                "given complexDebugInformation isn' safe"));

            //Check life cycle phase
            if (areValuesSafe)
                //Life cycle phase shouldn't be null
                if (lifeCyclePhase == ELifeCyclePhase.NULL)
                {
                    areValuesSafe = false;

                    DebugTools.DebugError(new ComplexDebugInformation(basicDebugInformation,
                    "given lifeCyclePhase is null"));
                }

            return areValuesSafe;
        }
    }
}