using MECS.Core;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Conditionals
{
    //* Tracking data class for IConditionalData
    public class ConditionalTrackingInfo : ADataTrackingInfo, IConditionalData
    {
        //Editor variables
        [Header("Conditional tracking info")]
        [SerializeReference] private NumericConditional[] numericConditionals = null;
        [SerializeReference] private StringConditional[] stringConditionals = null;
        [SerializeReference] private BoolConditional[] boolConditionals = null;
        [SerializeReference] private BoolReference useNumericConditionals = null;
        [SerializeReference] private BoolReference useStringConditionals = null;
        [SerializeReference] private BoolReference useBoolConditionals = null;

        //Attributes
        public NumericConditional[] NumericConditionals => numericConditionals;
        public StringConditional[] StringConditionals => stringConditionals;
        public BoolConditional[] BoolConditionals => boolConditionals;
        public BoolReference UseNumericConditionals => useNumericConditionals;
        public BoolReference UseStringConditionals => useStringConditionals;
        public BoolReference UseBoolConditionals => useBoolConditionals;

        //Base builder
        public ConditionalTrackingInfo(string entityName, NumericConditional[] numericConditionals, StringConditional[] stringConditionals,
        BoolConditional[] boolConditionals, BoolReference useNumericConditionals, BoolReference useStringConditionals,
        BoolReference useBoolConditionals) : base(entityName)
        {
            this.numericConditionals = numericConditionals;
            this.stringConditionals = stringConditionals;
            this.boolConditionals = boolConditionals;
            this.useNumericConditionals = useNumericConditionals;
            this.useStringConditionals = useStringConditionals;
            this.useBoolConditionals = useBoolConditionals;
        }
    }
}