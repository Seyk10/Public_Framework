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
            if (valueType.Equals(EReference.Value_01))
            {
                if (TypeTools.ConvertToType<T3>(value_01, out T3 newValue_01, " couldnt convert value01 to target type"))
                    resultValue = newValue_01;
            }
            //Check value_02 type
            else if (valueType.Equals(EReference.Value_02))
                if (TypeTools.ConvertToType<T3>(value_02, out T3 newValue_02, " couldnt convert value02 to target type"))
                    resultValue = newValue_02;

            return resultValue;
        }

        //Check types and set correct value
        public bool SetValue<T3>(T3 value)
        {
            //Return value
            bool isTypeOnUse = false;

            //Check value_01 type
            if (valueType.Equals(EReference.Value_01))
            {
                //Convert to target type
                if (TypeTools.ConvertToType(value, out T t3Value_01, " couldnt convert value to target type"))
                {
                    value_01 = t3Value_01;
                    Value_01ChangedEvent?.Invoke(this, value_01);
                    isTypeOnUse = true;
                }
            }
            //Check value_02 type
            else if (valueType.Equals(EReference.Value_02))
            {
                //Convert to target type
                if (TypeTools.ConvertToType(value, out T2 t3Value_02, " couldnt convert value to target type"))
                {
                    value_02 = t3Value_02;
                    Value_02ChangedEvent?.Invoke(this, value_02);
                    isTypeOnUse = true;
                }
            }

            return isTypeOnUse;
        }

        //IEditorReferences method, check if editor values are valid
        public bool CheckEditorReferences() =>
            //Check value01
            valueType.Equals(EReference.Value_01)
            && ReferenceTools.IsValueSafe(value_01, " value01 isn't safe")

            //Check value02
            || valueType.Equals(EReference.Value_02)
            && ReferenceTools.IsValueSafe(value_02, " value02 isn't safe");
    }
}