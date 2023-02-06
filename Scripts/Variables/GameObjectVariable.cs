using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MECS.Variables
{
    //* Asset type game object variable
    [CreateAssetMenu(fileName = "New_GameObject_Variable", menuName = "MECS/Variables/GameObject")]
    public class GameObjectVariable : AVariable<GameObject> { }
}