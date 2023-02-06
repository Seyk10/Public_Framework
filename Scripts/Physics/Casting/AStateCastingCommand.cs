using MECS.Patrons.StateMachine;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Abstract Scriptable object used to set states from editor invocations
    //* T = CastingComponent
    //* T2 = CastingData
    //* T3 = CastingProfile
    [CreateAssetMenu(fileName = "New_Casting_State_Command", menuName = "MECS/Commands/States/Casting_State")]
    public abstract class AStateCastingCommand : AEditorStateCommand<CastingComponent, CastingData, CastingProfile> { }
}