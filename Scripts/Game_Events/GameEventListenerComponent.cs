using MECS.Core;

namespace MECS.GameEvents
{
    //* Component used to register responses on game event raise
    //* T = GameEventListenerData
    //* T2 = GameEventListenerProfile
    public class GameEventListenerComponent : AComponent<GameEventListenerData, GameEventListenerProfile> { }
}