using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MECS.Variables
{
    //* Asset type int[] variable
    [CreateAssetMenu(fileName = "New_Int_Array_Variable", menuName = "MECS/Variables/Int_Array")]
    public class IntArrayVariable : AVariable<int[]> { }
}
