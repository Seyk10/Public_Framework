using UnityEngine;

namespace MECS.Variables
{
    //* Variable to store transforms 
    [CreateAssetMenu(fileName = "New_Quaternion_Variable", menuName = "MECS/Variables/Quaternion")]
    public class QuaternionVariable : AVariable<Quaternion> { }
}