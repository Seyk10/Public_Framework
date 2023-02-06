using UnityEngine;

namespace MECS.Patrons.Singleton
{
    //*Static class used to manage singleton instance
    public static class SingletonTools
    {
        //Methods
        //Check if a singleton instace allready exists
        public static bool CanSetInstance<T>(T singletonReference, T newInstance) => singletonReference == null && newInstance != null;
        //Debug if singleton instace exists error
        public static void DebugDuplicateSingleton() => Debug.LogWarning("Warning: Tried to duplicate singleton instance");
    }
}
