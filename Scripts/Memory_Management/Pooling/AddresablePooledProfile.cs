using MECS.Core;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Profile structure for addresable pooled entities
    //* T = AddresablePooledData
    [CreateAssetMenu(fileName = "New_Addresable_Pooled_Profile", menuName = "MECS/Profiles/Pooled_Entity")]
    public class AddresablePooledProfile : AProfile<AddresablePooledData> { }
}