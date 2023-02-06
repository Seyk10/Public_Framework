using UnityEngine;

namespace MECS.Variables
{
    //* Asset type int variable
    //* T = int
    [CreateAssetMenu(fileName = "New_Int_Variable", menuName = "MECS/Variables/Int")]
    public class IntVariable : AVariable<int>, INumericVariable<int>
    {
        //INumericVariable, make increment on value
        public void MakeIncrementOnValue(int variation)
        {
            //Store final value
            int incrementValue = variation;

            //Switch if its necessary
            if (incrementValue < 0)
                incrementValue = -incrementValue;

            //Set new value
            Value += variation;
        }

        //INumericVariable, make decrement on value
        public void MakeDecrementOnValue(int variation)
        {
            //Store final value
            int incrementValue = variation;

            //Switch if its necessary
            if (incrementValue < 0)
                incrementValue = -variation;

            //Set new value
            Value -= incrementValue;
        }
    }
}