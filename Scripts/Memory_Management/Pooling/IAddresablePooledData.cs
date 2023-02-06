using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Interface used on addresable pooled entities data structures
    public interface IAddresablePooledData
    {
        //Variables
        public bool IsPooledObject { get; set; }
        public GameObject AssociatedEntity { get; set; }
        public AddresableEntityPool AddresableEntityPool { get; set; }
    }
}