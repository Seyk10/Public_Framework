using MECS.Events;
using MECS.Tools;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Args used to share information spawn pooled notification command
    public class SpawnPooledEntityArgs : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly Transform spawnTransform = null;
        public readonly AddresableEntityPool addresableEntityPool = null;

        //Default builder
        public SpawnPooledEntityArgs(Transform spawnTransform, AddresableEntityPool addresableEntityPool, string debugMessage)
        : base(debugMessage)
        {
            this.spawnTransform = spawnTransform;
            this.addresableEntityPool = addresableEntityPool;
        }

        //IValuesChecking method, check args values
        public bool AreValuesValid() =>
            //Check spawnTransform
            ReferenceTools.IsValueSafe(spawnTransform, " given spawnTransform isn't valid")

            //Check addresableEntityPool
            && ReferenceTools.IsValueSafe(addresableEntityPool, " given addresableEntityPool isn't valid");
    }
}