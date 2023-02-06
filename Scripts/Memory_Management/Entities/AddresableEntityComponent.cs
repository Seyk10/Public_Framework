
using MECS.Core;
using UnityEngine;

namespace MECS.MemoryManagement.Entity
{
    //* Component used on game objects loaded from asset references
    //* T = AddresableEntityData
    //* T2 = AddresableEntityProfile
    public class AddresableEntityComponent : AComponent<AddresableEntityData, AddresableEntityProfile> { }
}