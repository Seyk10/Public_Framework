using System;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Args used to share information spawn pooled notification command
    public class SpawnPooledEntityArgs : EventArgs
    {
        //Variables
        public readonly Transform spawnTransform = null;

        public readonly AddresableEntityPool addresableEntityPool = null;

        //Default builder
        public SpawnPooledEntityArgs(Transform spawnTransform, AddresableEntityPool addresableEntityPool)
        {
            this.spawnTransform = spawnTransform;
            this.addresableEntityPool = addresableEntityPool;
        }
    }
}