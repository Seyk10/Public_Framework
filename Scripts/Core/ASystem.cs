using MECS.Collections;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Base class for system type objects
    //* T = Data interface type
    public abstract class ASystem<T> : ScriptableObject where T : class
    {
        //Variables
        private static bool isSystemLoaded = false;

        public static bool IsSystemLoaded => isSystemLoaded;

        //ScriptableObject, set subscriptions
        protected virtual void OnEnable()
        {
            AManager<T>.dataDictionary.ElementAddedEvent += DataRegistrationResponse;
            isSystemLoaded = true;
        }

        //ScriptableObject, remove subscriptions
        protected virtual void OnDisable()
        {
            AManager<T>.dataDictionary.ElementAddedEvent -= DataRegistrationResponse;
            isSystemLoaded = false;
        }

        //Set responses to data registration
        //Sender is dictionary
        //Entity can be found on value
        //Data can be found on key
        protected virtual void DataRegistrationResponse(object sender, ResponsiveDictionaryArgs<T, MonoBehaviour> args)
        {
            //basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
            "DataRegistrationResponse(object sender, ResponsiveDictionaryArgs<T, MonoBehaviour> args)");

            //Check if values are safe
            bool areValuesSafe = ReferenceTools.IsValueSafe(sender, new ComplexDebugInformation(basicDebugInformation, "sender isn't safe"))
                && ReferenceTools.IsValueSafe(args, new ComplexDebugInformation(basicDebugInformation, "args aren't safe"))
                && args.AreValuesValid();

            //Try to convert sender
            if (areValuesSafe)
                //Convert value
                if (TypeTools.ConvertToType<Component>(args.value, out Component entityComponent,
                new ComplexDebugInformation(basicDebugInformation, "tried to register a non component value")))
                    //Check data values
                    //Use args debug information on next executions
                    IsValidData(entityComponent, args.key, args.complexDebugInformation);
        }

        //Check if registered data has valid values
        protected abstract bool IsValidData(Component entity, T data, ComplexDebugInformation complexDebugInformation);
    }
}