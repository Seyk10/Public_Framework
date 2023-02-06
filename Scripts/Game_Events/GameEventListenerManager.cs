using MECS.Core;
using UnityEngine;

namespace MECS.GameEvents
{
    //* Manager used to collect all the game event listener components
    //* T = IGameEventListenerData
    [CreateAssetMenu(fileName = "New_Game_Event_Listener_Manager", menuName = "MECS/Managers/Game_Event")]
    public class GameEventListenerManager : AManager<IGameEventListenerData> { }
}