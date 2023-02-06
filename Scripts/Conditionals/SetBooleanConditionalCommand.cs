using MECS.Variables.References;

namespace MECS.Conditionals
{
    //* Command used to set boolean values on boolean based conditionals
    //* T = bool
    //* T2 = BoolConditional
    //* T3 = BoolReference
    //* T4 = EConditional
    public class SetBooleanConditionalCommand : AConditionalCommand<bool, BoolConditional, BoolReference, EConditional>
    {
        //Default builder
        public SetBooleanConditionalCommand(bool value, BoolConditional[] conditionals) : base(value, conditionals) { }

        //Set the bool value on conditionals
        public override void Execute()
        {
            //Itinerate conditionals
            foreach (BoolConditional conditional in conditionals)
                //Set value on value_01 of conditional
                conditional.Value_01.Value = value;

            // Raise event
            base.Execute();
        }
    }
}