using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MECS.Variables
{
    //* Variable to store colliders 
    [CreateAssetMenu(fileName = "New_Collider_Variable", menuName = "MECS/Variables/Collider")]
    public class ColliderVariable : AVariable<Collider> { }
}