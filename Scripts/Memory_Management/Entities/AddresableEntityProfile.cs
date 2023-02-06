using MECS.Core;
using UnityEngine;

namespace MECS.MemoryManagement.Entity
{
    //* Profile used on AddresableEntities
    //* T = AddresableEntityData
    [CreateAssetMenu(fileName = "New_Addresable_Entity_Profile", menuName = "MECS/Profiles/Addresable_Entity")]
    public class AddresableEntityProfile : AProfile<AddresableEntityData> { }
}