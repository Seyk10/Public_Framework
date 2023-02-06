using MECS.Core;
using UnityEngine;

namespace MECS.Timers
{
    //* Profile used to share events configuration on timer data
    //* T = TimerData
    [CreateAssetMenu(fileName = "New_Timer_Profile", menuName = "MECS/Profiles/Timer")]
    public class TimerProfile : AProfile<TimerData> { }
}