using MECS.Patrons.Singleton;
using UnityEngine;

namespace MECS.MemoryManagement
{
    //Loads all scriptable references
    public class ScriptableLoaderComponent : AMonoSingleton<ScriptableLoaderComponent>
    {
        //Editor variables
        [Header("Configuration")]
        public ScriptableObject[] loadedScriptables = null;

        //MonoBehaviour-AMonoSingleton
        //Set singleton and don't destroy on load
        protected override void Awake()
        {
            //AMonoSingleton execution
            base.Awake();

            DontDestroyOnLoad(this);
        }
    }
}