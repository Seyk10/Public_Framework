using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.GameEvents
{
    //* Command used to unregister listeners
    [CreateAssetMenu(fileName = "New_Scriptable_Unregister_Command", menuName = "MECS/Commands/Game_Events/Unregister")]
    public class ScriptableUnregisterCommand : AScriptableParameterCommand<GameEventListenerComponent>
    {
        //AScriptableParameterCommand, execute command content
        public override void Execute(GameEventListenerComponent parameter)
        {
            //Notify game event listener system
            new NotificationCommand<NotificationUnregisterListenerArgs>(parameter,
                new NotificationUnregisterListenerArgs(parameter, " couldnt register GameEventListenerComponent")).Execute();

            base.Execute(parameter);
        }
    }
}