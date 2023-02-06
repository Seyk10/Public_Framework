using UnityEngine;

namespace MECS.Variables
{
    //* Variable to store colliders 
    [CreateAssetMenu(fileName = "New_Collider_Array_Variable", menuName = "MECS/Variables/Collider_Array")]
    public class ColliderArrayVariable : AVariable<Collider[]> { }
}