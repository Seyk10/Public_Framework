using UnityEngine;

namespace MECS.Variables
{
    //* Asset type string variable
    [CreateAssetMenu(fileName = "New_String_Variable", menuName = "MECS/Variables/String")]
    public class StringVariable : AVariable<string> { }
}