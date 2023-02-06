using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace MECS.Patrons.Injector
{
    //* Static class used to execute code on application start
    public static class AssetsInjector
    {
        //Variables
        //Prevent to execute ExecuteOnApplicationStart() multiple times
        private static bool hasExecuteOnApplicationStart = false;
        public static bool HasExecuteOnApplicationStart => hasExecuteOnApplicationStart;

        //Store all the assets loaded
        private static List<AsyncOperationHandle<IList<IResourceLocation>>> assetsLoaded = new();

        //Methods
        //Execute only one time per application execution, load all the assets of managers and systems
        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        // private static void ExecuteOnApplicationStart()
        // {
        //     //Check if can execute
        //     if (!hasExecuteOnApplicationStart)
        //     {
        //         //Avoid more calls
        //         hasExecuteOnApplicationStart = true;

        //         //Start addressable
        //         Addressables.InitializeAsync();

        //         //Load labels of resources
        //         Addressables.LoadResourceLocationsAsync("Manager").Completed += (operation) =>
        //         {
        //             assetsLoaded.Add(operation);
        //             Debug.Log(assetsLoaded.Count);
        //         };
        //         Addressables.LoadResourceLocationsAsync("System").Completed += (operation) => { assetsLoaded.Add(operation); };

        //         //Clean resources on application quitting
        //         Application.quitting += () =>
        //         {
        //             //Itinerate and clean operations
        //             foreach (AsyncOperationHandle<IList<IResourceLocation>> operation in assetsLoaded)
        //                 Addressables.Release(operation);
        //         };
        //     }
        // }
    }
}