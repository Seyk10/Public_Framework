using MECS.Core;
using UnityEngine;

namespace MECS.Colliders
{
    //* Profile to store ColliderData structures
    //* T = ColliderData
    [CreateAssetMenu(fileName = "New_Collider_Profile", menuName = "MECS/Profiles/Collider")]
    public class ColliderProfile : AProfile<ColliderData> { }
}