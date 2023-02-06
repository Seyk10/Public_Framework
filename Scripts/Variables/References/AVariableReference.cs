using System;
using MECS.General;
using UnityEngine;

namespace MECS.Variables.References
{
    //* Base class used on reference type structures
    public abstract class AVariableReference<T, T2> : IEditableReference<T>, IEditorReferences, IVariableChange<T> where T2 : AVariable<T>
    {
        //Editor variables
        [SerializeField] private T localValue;
        [SerializeField] private T2 externalValue;
        [SerializeField] private bool useLocalValue = true;

        //Events, notify value modifications 
        public event EventHandler<T> VariableChangeEvent = null;

        //Attribute
        //Return correct value
        public T Value
        {
            get => useLocalValue ? localValue : externalValue.Value;
            set
            {
                //Check value to use and notify
                if (useLocalValue)
                {
                    localValue = value;
                    VariableChangeEvent?.Invoke(this, localValue);
                }
                else
                {
                    externalValue.Value = value;
                    VariableChangeEvent?.Invoke(this, externalValue.Value);
                }
            }
        }

        //IEditorReferences, check if editor values are valid
        public bool CheckEditorReferences()
        {
            bool validValues = true;

            //Check local value
            if (useLocalValue && localValue.Equals(null))
            {
                validValues = false;
#if UNITY_EDITOR
                Debug.LogWarning("Warning: Local value is missing on variable reference");
#endif
            }
            //Check external value
            else if (!useLocalValue && externalValue.Equals(null))
            {
                validValues = false;
#if UNITY_EDITOR
                Debug.LogWarning("Warning: External value is missing on variable reference");
#endif
            }
            else if (!useLocalValue && externalValue.Equals(null) && externalValue.Value.Equals(null))
            {
                validValues = false;
#if UNITY_EDITOR
                Debug.LogWarning("Warning: External value data is missing on variable reference");
#endif
            }

            return validValues;
        }
    }
}