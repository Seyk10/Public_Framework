using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Variables
{
    //* Base class used on variable types scriptable objects with entities involved
    //* T = Variable type
    public class AEntityVariable<T> : AVariable<T> where T : Component
    {
        //Editor values
        [Header("EntityVariable configuration")]
        [SerializeField] private string lastEntityName = null;

        //AVariable, override to debug entity information
        public override void SetValue(T value)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation =
            new BasicDebugInformation(this.GetType().ToString(), "SetValue(T value)");
            string variableName = this.name;

            //Check value
            bool isValidVariable = ReferenceTools.IsValueSafe(value,
            new ComplexDebugInformation(basicDebugInformation, "given variable to" + this.name + " isn't valid."));

            //Set value
            if (isValidVariable)
            {
                lastEntityName = value.gameObject.name;
                InternalValue = value;
                OnVariableChangeEvent?.Invoke();
                RaiseVariableChangeEvent();
            }
        }
    }
}