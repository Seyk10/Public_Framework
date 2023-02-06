using MECS.Core;
using UnityEngine;

namespace MECS.LifeCycle
{
    //* Profile used to store life cycle data structures
    //* T = LifeCycleData
    [CreateAssetMenu(fileName = "New_Life_Cycle_Profile", menuName = "MECS/Profiles/Life_Cycle")]
    public class LifeCycleProfile : AProfile<LifeCycleData> { }
}