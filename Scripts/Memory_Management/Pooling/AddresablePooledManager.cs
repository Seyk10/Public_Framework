using MECS.Core;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Manager used to register all the addresable pooled components
    //* T = IAddresablePooledData
    [CreateAssetMenu(fileName = "New_Addresable_Pooled_Manager", menuName = "MECS/Managers/Addresable_Pooled")]
    public class AddresablePooledManager : AManager<IAddresablePooledData> { }
}