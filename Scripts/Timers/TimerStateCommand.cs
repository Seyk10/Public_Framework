using MECS.Patrons.StateMachine;

namespace MECS.Timers
{
    //* Abstract command used to set the run state on target timer
    //* T = TimerComponent
    //* T2 = TimerData
    //* T3 = TimerProfile
    public abstract class ATimerStateCommand : AEditorStateCommand<TimerComponent, TimerData, TimerProfile> { }
}