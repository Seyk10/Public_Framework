using MECS.Core;
using MECS.Patrons.Commands;

namespace MECS.Patrons.StateMachine
{
    //* Base class for commands used to notify states on components
    //* T = Component type
    //* T2 = AData type
    //* T3 = Profile type
    public abstract class AEditorStateCommand<T, T2, T3> : AScriptableParameterCommand<T> where T : AComponent<T2, T3> where T2 : AData where T3 : AProfile<T2>
    { }
}