using MECS.Patrons.Commands;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.GameEvents
{
    //* Command used to unregister listeners
    [CreateAssetMenu(fileName = "New_Scriptable_Unregister_Command", menuName = "MECS/Commands/Game_Events/Unregister")]
    public class ScriptableUnregisterCommand : AScriptableParameterCommand<GameEventListenerComponent>
    {
        //AScriptableParameterCommand, execute command content
        public override void Execute(GameEventListenerComponent parameter)
        {
            //Debug information
            BasicDebugInformation debugInformation = new("ScriptableUnregisterCommand",
             "Execute(GameEventListenerComponent parameter) with " + parameter.name);

            //Notify game event listener system
            new NotificationCommand<NotificationUnregisterListenerArgs>(parameter,
                new NotificationUnregisterListenerArgs(parameter), debugInformation).Execute();

            base.Execute(parameter);
        }
    }
}
