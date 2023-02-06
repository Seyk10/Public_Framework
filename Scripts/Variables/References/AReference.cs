using UnityEngine;
using System;
using MECS.General;
using MECS.Tools;

namespace MECS.Variables.References
{
    //* Base class used on generic references structures
    //* T = value_01
    //* T2 = value_02
    public abstract class AReference<T, T2> : IGenericEditableReference, IEditorReferences
    {
        //Editor variables
        [SerializeField] private T value_01;
        [SerializeField] private T2 value_02;
        [SerializeField] private EReference valueType = EReference.Value_01;

        //Events, used to notify value changes
        public event EventHandler<T> Value_01ChangedEvent = null;
        public event EventHandler<T2> Value_02ChangedEvent = null;

        //Generic set/get
        //Check types and return correct value
        public T3 GetValue<T3>()
        {
            //Return value
            T3 resultValue = default;

            //Check value_01 type
            if (valueType == EReference.Value_01)
                resultValue = ReferenceTools.ConvertObjectToType<T3>(value_01);
            //Check value_02 type
            else if (valueType == EReference.Value_02)
                resultValue = ReferenceTools.ConvertObjectToType<T3>(value_02);

            return resultValue;
        }

        //Check types and set correct value
        public bool SetValue<T3>(T3 value)
        {
            //Return value
            bool isTypeOnUse = false;

            //Check value_01 type
            if (valueType == EReference.Value_01 && value is T t3Value_01)
            {
                value_01 = t3Value_01;
                Value_01ChangedEvent?.Invoke(this, value_01);
                isTypeOnUse = true;
            }
            //Check value_02 type
            else if (valueType == EReference.Value_02 && value is T2 t3Value_02)
            {
                value_02 = t3Value_02;
                Value_02ChangedEvent?.Invoke(this, value_02);
                isTypeOnUse = true;
            }
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: Could not find given type.");
#endif

            return isTypeOnUse;
        }

        //IEditorReferences, check if editor values are valid
        public bool CheckEditorReferences()
        {
            bool validValues = true;

            //Check value 01
            if (valueType == EReference.Value_01 && value_01 == null)
            {
                validValues = false;
#if UNITY_EDITOR
                Debug.LogWarning("Warning: Value_01 is missing.");
#endif
            }
            //Check value 02
            else if (valueType == EReference.Value_02 && value_02 == null)
            {
                validValues = false;
#if UNITY_EDITOR
                Debug.LogWarning("Warning: Value_02 is missing.");
#endif
            }

            return validValues;
        }
    }
}