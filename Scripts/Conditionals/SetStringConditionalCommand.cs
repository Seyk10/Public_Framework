using MECS.Variables.References;

namespace MECS.Conditionals
{
    //* Command used to set string values on string based conditionals
    //* T = string
    //* T2 = StringConditional
    //* T3 = StringReference
    //* T4 = EConditional
    public class SetStringConditionalCommand : AConditionalCommand<string, StringConditional, StringReference, EConditional>
    {
        //Default builder
        public SetStringConditionalCommand(string value, StringConditional[] conditionals) : base(value, conditionals) { }

        //Set the string value on conditionals
        public override void Execute()
        {
            //Itinerate each string conditional
            foreach (StringConditional conditional in conditionals)
                //Set on value_01 new string value
                conditional.Value_01.Value = value;

            // Raise event
            base.Execute();
        }
    }
}