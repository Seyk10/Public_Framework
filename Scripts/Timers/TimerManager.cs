using MECS.Core;
using UnityEngine;

namespace MECS.Timers
{
    //* Manager used to register all the ITimerData data
    //* T = ITimerData
    [CreateAssetMenu(fileName = "New_Timer_Manager", menuName = "MECS/Managers/Timer")]
    public class TimerManager : AManager<ITimerData> { }
}