using MECS.Core;
using UnityEngine;

namespace MECS.GameEvents
{
    //* Profile used on game events listener components
    //* T = GameEventListenerData
    [CreateAssetMenu(fileName = "New_GameEvent_Listener_Profile", menuName = "MECS/Profiles/GameEvent_Listener")]
    public class GameEventListenerProfile : AProfile<GameEventListenerData> { }
}