using UnityEngine;

namespace MECS.Variables
{
    //* Variable to store transforms 
    [CreateAssetMenu(fileName = "New_Transform_Variable", menuName = "MECS/Variables/Transform")]
    public class TransformVariable : AEntityVariable<Transform> { }
}