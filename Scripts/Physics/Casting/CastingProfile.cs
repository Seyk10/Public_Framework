using MECS.Core;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Profile used on casting structures
    //* T = CastingData
    [CreateAssetMenu(fileName = "New_Casting_Profile", menuName = "MECS/Profiles/Casting_Profile")]
    public class CastingProfile : AProfile<CastingData> { }
}