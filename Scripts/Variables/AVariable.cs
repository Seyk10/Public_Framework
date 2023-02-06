using System;
using MECS.Tools;
using UnityEngine;
using UnityEngine.Events;
using static MECS.Tools.DebugTools;

namespace MECS.Variables
{
    //* Base class used on variable types scriptable objects
    //* T = Variable type
    public abstract class AVariable<T> : ScriptableObject, IVariableChange<T>
    {
        //Variables
        [TextArea(3, 7)]
#pragma warning disable 0414
        [SerializeField] private string dataDescription = null;
#pragma warning disable 0414
        [SerializeField] private T value = default;
        [SerializeField] private T defaultValue = default;
        [SerializeField] private UnityEvent onVariableChangeEvent = null;

        //Event to raise when value changes
        public event EventHandler<T> VariableChangeEvent = null;

        //Attributes
        public T Value
        {
            get => value;
            set
            {
                //Raise event and share value
                this.value = value;
                onVariableChangeEvent?.Invoke();
                VariableChangeEvent?.Invoke(this, this.value);
            }
        }

        protected UnityEvent OnVariableChangeEvent => onVariableChangeEvent;

        protected T InternalValue { get => value; set => this.value = value; }

        //Method, set value from another variable
        public virtual void SetValue(AVariable<T> variable)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation =
            new BasicDebugInformation(this.GetType().ToString(), "AVariable<T> variable");
            string variableName = this.name;

            //Check value
            bool isValidVariable = ReferenceTools.IsVariableSafe(variable,
            new ComplexDebugInformation(basicDebugInformation, "given variable to" + this.name + " isn't valid."));

            //Set value
            if (isValidVariable)
            {
                this.value = variable.Value;
                onVariableChangeEvent?.Invoke();
                VariableChangeEvent?.Invoke(this, this.value);
            }
        }

        //Method, set value
        public virtual void SetValue(T value)
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
                this.value = value;
                onVariableChangeEvent?.Invoke();
                VariableChangeEvent?.Invoke(this, this.value);
            }
        }

        //Clean variable value
        public void ResetVariable() => Value = defaultValue;

        //Method, raise internal event
        protected void RaiseVariableChangeEvent() => VariableChangeEvent?.Invoke(this, this.value);
    }
}
