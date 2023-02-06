using MECS.Patrons.Commands;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.GameEvents
{
    //* Command used to register listeners 
    [CreateAssetMenu(fileName = "New_Scriptable_Registration_Command", menuName = "MECS/Commands/Game_Events/Registration")]
    public class ScriptableRegistrationCommand : AScriptableParameterCommand<GameEventListenerComponent>
    {
        //AScriptableParameterCommand, execute command content        
        public override void Execute(GameEventListenerComponent parameter)
        {
            //Debug information
            BasicDebugInformation debugInformation = new("ScriptableRegistrationCommand",
             "Execute(GameEventListenerComponent parameter) with " + parameter.name);

            //Notify game event listener system
            new NotificationCommand<NotificationRegisterListenerArgs>(parameter,
                new NotificationRegisterListenerArgs(parameter), debugInformation).Execute();

            //AScriptableParameterCommand 
            base.Execute(parameter);
        }
    }
}