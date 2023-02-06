using MECS.Core;
using UnityEngine;

namespace MECS.MemoryManagement.Entity
{
    //* Manager used to store entities loaded with addresable
    //* T = IAddresableEntityData
    [CreateAssetMenu(fileName = "New_Addresable_Entity_Manager", menuName = "MECS/Managers/Addresable_Entity")]
    public class AddresableEntityManager : AManager<IAddresableEntityData> { }
}