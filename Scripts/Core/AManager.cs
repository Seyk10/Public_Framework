using System;
using MECS.Collections;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Base class of all data managers, manage runtime sets and events related
    //* T = Interface data type
    //* T2 = Tracking information type
    public abstract class AManager<T> : ScriptableObject, IDisposable where T : class
    {
        //Variables
        private static bool isManagerLoaded = false;

        //Used to store all the target data structures
        public static ResponsiveDictionary<T, MonoBehaviour> dataDictionary = new(),
        enableDataDictionary = new(),
        disableDataDictionary = new();
        public static bool IsManagerLoaded => isManagerLoaded;

        //ScriptableObject, subscribe to notifications
        protected virtual void OnEnable()
        {
            //Clean content
            Dispose();

            //Subscribe to application quit to clean data
            Application.quitting += Dispose;

            //Subscribe to data type calls
            NotificationCommand<EntityAwakeArgs<T>>.NotificationEvent += AwakeResponse;
            NotificationCommand<EntityDestroyArgs<T>>.NotificationEvent += DestroyResponse;
            NotificationCommand<EntityDisableArgs<T>>.NotificationEvent += DisableResponse;
            NotificationCommand<EntityEnableArgs<T>>.NotificationEvent += EnableResponse;

            isManagerLoaded = true;
        }

        //ScriptableObject, unsubscribe to notifications
        protected virtual void OnDisable()
        {
            //Unsubscribe to application quit to clean data
            Application.quitting -= Dispose;

            //Unsubscribe to data type calls
            NotificationCommand<EntityAwakeArgs<T>>.NotificationEvent -= AwakeResponse;
            NotificationCommand<EntityDestroyArgs<T>>.NotificationEvent -= DestroyResponse;
            NotificationCommand<EntityDisableArgs<T>>.NotificationEvent -= DisableResponse;
            NotificationCommand<EntityEnableArgs<T>>.NotificationEvent -= EnableResponse;

            isManagerLoaded = false;

            //Clean content
            Dispose();
        }

        //Method, respond to interface data type awake event
        private void AwakeResponse(object sender, EntityAwakeArgs<T> args)
        {
            //Check parameters first
            if (ReferenceTools.AreEventParametersValid(sender, args, " given args aren't valid"))

                //Convert sender if its possible
                if (TypeTools.ConvertToType(sender, out MonoBehaviour entityMonoBehaviour,
                args.debugMessage + " couldnt convert sender to monoBehaviour"))

                    //Avoid if contains data
                    if (!dataDictionary.dictionary.ContainsKey(args.data))
                        //Add to data instances dictionary
                        dataDictionary.AddElement(args.data, entityMonoBehaviour,
                        args.debugMessage + " couldnt add data on dataDictionary from entity: " + entityMonoBehaviour.gameObject.name);
        }

        //Method, respond to interface data type destroy event
        private void DestroyResponse(object sender, EntityDestroyArgs<T> args)
        {
            //Check parameters first
            if (ReferenceTools.AreEventParametersValid(sender, args, " given args aren't valid"))

                //Convert sender if its possible
                if (TypeTools.ConvertToType(sender, out MonoBehaviour entityMonoBehaviour,
                args.debugMessage + " couldnt convert sender to monoBehaviour"))

                    //GC destroy data before some executions so its recommended to check if data is available
                    //Check if dictionary contains element
                    if (dataDictionary.dictionary.ContainsKey(args.data))
                        //Remove from data instances dictionary
                        dataDictionary.RemoveElement(args.data,
                            args.debugMessage + "couldnt remove data on dataDictionary from entity: "
                            + entityMonoBehaviour.gameObject.name);
        }

        //Method, respond to interface data type disable event
        private void DisableResponse(object sender, EntityDisableArgs<T> args)
        {
            //Check parameters first
            if (ReferenceTools.AreEventParametersValid(sender, args, " given args aren't valid"))

                //Convert sender if its possible
                if (TypeTools.ConvertToType(sender, out MonoBehaviour entityMonoBehaviour,
                args.debugMessage + " couldnt convert sender to monoBehaviour"))
                {
                    //Some elements can be instantiate as disable, also on application quit GC destroy data before some
                    //executions so its recommended to check if data is available

                    //Remove data from enable dictionary if its was enable
                    if (enableDataDictionary.dictionary.ContainsKey(args.data))
                        enableDataDictionary.RemoveElement(args.data,
                        args.debugMessage + " couldnt remove data on enableDataDictionary from entity: "
                        + entityMonoBehaviour.gameObject.name);

                    //Add data to disable dictionary if it doesnt contain
                    if (!disableDataDictionary.dictionary.ContainsKey(args.data))
                        //Add data to disable dictionary 
                        disableDataDictionary.AddElement(args.data, entityMonoBehaviour,
                        args.debugMessage + " couldnt add data on disableDataDictionary from entity: "
                        + entityMonoBehaviour.gameObject.name);
                }
        }

        //Method, respond to interface data type enable event
        private void EnableResponse(object sender, EntityEnableArgs<T> args)
        {
            //Check parameters first
            if (ReferenceTools.AreEventParametersValid(sender, args, " given args aren't valid"))

                //Convert sender if its possible
                if (TypeTools.ConvertToType(sender, out MonoBehaviour entityMonoBehaviour,
                args.debugMessage + " couldnt convert sender to monoBehaviour"))
                {
                    //Try to remove if dictionary contains value
                    disableDataDictionary.RemoveElement(args.data);

                    //Add data to enableDataDictionary if it doesnt contain
                    if (!enableDataDictionary.dictionary.ContainsKey(args.data))
                        //Try to add data on enable dictionary
                        enableDataDictionary.AddElement(args.data, entityMonoBehaviour,
                        args.debugMessage + " couldnt add data on enableDataDictionary from entity: "
                        + entityMonoBehaviour.gameObject.name);
                }
        }

        //IDisposable method, clean all the data when manager unload
        public void Dispose()
        {
            dataDictionary.Dispose();
            enableDataDictionary.Dispose();
            disableDataDictionary.Dispose();
        }
    }
}