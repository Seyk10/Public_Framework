using System;
using MECS.Patrons.Commands;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MECS.MemoryManagement.Entity
{
    //* Command used to instantiate entities from asset references
    [CreateAssetMenu(fileName = "New_Instantiate_Asset_Reference_Entity", menuName = "MECS/Commands/Memory_Management/Entity/Instantiate_Asset_Reference")]
    public class InstantiateAssetReferenceEntityCommand : ScriptableObject, ICommandParameter<Transform>
    {
        //Editor variables
        [SerializeField]
        private AssetReference prefabAssetReference = null;
        //Notify when command execution ends
        public event EventHandler<Transform> CommandFinishedEvent = null;

        //Variables
        public GameObjectAddressablesLoader gameObjectAddressablesLoader = new GameObjectAddressablesLoader();

        //Event, notify to system instantiation of prefab asset reference
        public static event EventHandler<InstantiateAssetReferenceEventArgs> InstantiateAssetReference = null;

        //Notify instantiation of prefab
        public void Execute(Transform parameter)
        {
            InstantiateAssetReference?.Invoke(this, new InstantiateAssetReferenceEventArgs(prefabAssetReference, gameObjectAddressablesLoader, parameter));
            CommandFinishedEvent?.Invoke(this, parameter);
        }
    }
}