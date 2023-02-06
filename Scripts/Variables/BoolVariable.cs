using UnityEngine;

namespace MECS.Variables
{
    //* Asset type boolean variable
    [CreateAssetMenu(fileName = "New_Boolean_Variable", menuName ="MECS/Variables/Boolean")]
    public class BoolVariable : AVariable<bool> { }
}