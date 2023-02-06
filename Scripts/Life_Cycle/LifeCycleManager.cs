using MECS.Core;
using UnityEngine;

namespace MECS.LifeCycle
{
    //* Manager used with life cycle structure data
    [CreateAssetMenu(fileName = "New_Life_Cycle_Manager", menuName = "MECS/Managers/Life_Cycle")]
    public class LifeCycleManager : AManager<ILifeCycleData> { }
}