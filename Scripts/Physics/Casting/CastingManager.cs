using MECS.Core;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Manager used to store casting data information
    //* T = ICastingData
    [CreateAssetMenu(fileName = "New_Casting_Manager", menuName = "MECS/Managers/Casting")]
    public class CastingManager : AManager<ICastingData> { }
}