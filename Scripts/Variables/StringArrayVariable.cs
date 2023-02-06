using UnityEngine;

namespace MECS.Variables
{
    //* Asset type string array variable
    [CreateAssetMenu(fileName = "New_String_Array_Variable", menuName = "MECS/Variables/String_Array")]
    public class StringArrayVariable : AVariable<string[]> { }
}