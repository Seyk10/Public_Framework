using MECS.Variables.References;

namespace MECS.Conditionals
{
    //* Interface used on data structure of conditionals values
    public interface IConditionalData
    {
        //Variables       
        public NumericConditional[] NumericConditionals { get; }
        public StringConditional[] StringConditionals { get; }
        public BoolConditional[] BoolConditionals { get; }
        public BoolReference UseNumericConditionals { get; }
        public BoolReference UseStringConditionals { get; }
        public BoolReference UseBoolConditionals { get; }
    }
}