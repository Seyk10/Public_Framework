using MECS.Entities.Commands;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Patrons.Singleton
{
    //*Static class used to manage singleton instance
    public abstract class AMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        //Variables
        //Static internal reference of singleton
        private static T instance = default;

        //Get singleton reference and create instance if it doesn't exist'
        public static T Instance
        {
            //Manage singleton reference
            get
            {
                //Set singleton
                if (instance == null)
                    instance = new InstantiateEntityAndComponentCommand<T>(typeof(T).FullName).Execute().GetComponent<T>();

                return instance;
            }
        }

        //MonoBehaviour
        //Avoid duplicate singletons
        protected virtual void Awake()
        {
            //Check instance value
            bool isNullInstance = instance == null,
            isSameInstance = !isNullInstance && instance == this;

            //Check if can set instance
            if (isNullInstance)
                instance = this.GetComponent<T>();
            //Check if should destroy this object
            else if (!isSameInstance)
                Destroy(gameObject);
        }

        //Remove singleton reference
        protected virtual void OnDestroy()
        {
            //Check instance value
            bool isNullInstance = instance == null,
            isSameInstance = !isNullInstance && instance == this;

            //Check if can set instance
            if (isSameInstance)
                instance = null;
#if UNITY_EDITOR
            //Check if should destroy this object
            else if (isNullInstance)
                DebugTools.DebugError(new ComplexDebugInformation
                ("AMonoSingleton<T>", "OnDestroy()", "singleton instance is null before entity is destroyed."));
#endif
        }
    }
}