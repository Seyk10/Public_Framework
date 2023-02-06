using MECS.Patrons.ObjectPooling;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Scriptable object used to save configurations of game object pools based on asset references
    [CreateAssetMenu(fileName = "New_Addresable_Entity_Pool", menuName = "MECS/Entities/Addresable_Entity_Pool")]
    public class AddresableEntityPool : ScriptableObject
    {
        //Editor variables
        [SerializeField] private AssetReference assetReference = null;
        [SerializeField] private int poolMaxSize = 0;
        [SerializeField] private int poolDangerSize = 3;
        [SerializeField] private bool canReturnEntities = true;
        [SerializeField] private bool canExpandPool = true;
        [SerializeField] private bool setPositionOnSpawn = true;
        [SerializeField] private bool setRotationOnSpawn = true;

        //Variables
        private GameObjectAddressablesLoader gameObjectAddressablesLoader = new();
        private ObjectPool<GameObject> objectPool = null;

        public ObjectPool<GameObject> ObjectPool { get => objectPool; set => objectPool = value; }
        public GameObjectAddressablesLoader GameObjectAddressablesLoader { get => gameObjectAddressablesLoader; }
        public AssetReference AssetReference { get => assetReference; }
        public int PoolMaxSize { get => poolMaxSize; }
        public bool CanReturnEntities { get => canReturnEntities; }
        public bool CanExpandPool { get => canExpandPool; }
        public bool SetPositionOnSpawn { get => setPositionOnSpawn; }
        public bool SetRotationOnSpawn { get => setRotationOnSpawn; }
        public int PoolDangerSize { get => poolDangerSize; }
    }
}