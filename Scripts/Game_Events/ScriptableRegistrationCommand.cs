using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.GameEvents
{
    //* Command used to register listeners 
    [CreateAssetMenu(fileName = "New_Scriptable_Registration_Command", menuName = "MECS/Commands/Game_Events/Registration")]
    public class ScriptableRegistrationCommand : AScriptableParameterCommand<GameEventListenerComponent>
    {
        //AScriptableParameterCommand, execute command content        
        public override void Execute(GameEventListenerComponent parameter)
        {
            //Notify game event listener system
            new NotificationCommand<NotificationRegisterListenerArgs>(parameter,
                new NotificationRegisterListenerArgs(parameter, " couldnt notify registration")).Execute();

            //AScriptableParameterCommand 
            base.Execute(parameter);
        }
    }
}