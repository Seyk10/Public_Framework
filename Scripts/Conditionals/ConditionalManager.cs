using MECS.Core;
using UnityEngine;

namespace MECS.Conditionals
{
    //* Manager used to register conditional interfaces
    //* T = IConditionalData
    [CreateAssetMenu(fileName = "New_Conditional_Manager", menuName = "MECS/Managers/Conditional")]
    public class ConditionalManager : AManager<IConditionalData> { }
}