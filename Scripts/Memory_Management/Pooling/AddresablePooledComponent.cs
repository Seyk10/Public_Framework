using MECS.Core;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Component used on entities spawned from addresable pools.
    //* T = AddresablePooledData
    //* T2 = AddresablePooledProfile
    public class AddresablePooledComponent : AComponent<AddresablePooledData, AddresablePooledProfile> { }
}