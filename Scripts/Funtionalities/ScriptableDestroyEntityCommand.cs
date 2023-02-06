using System;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Functionalities
{
    //* Command used to destroy entities from editor
    [CreateAssetMenu(fileName = "New_Destroy_Entity_Command", menuName = "MECS/Commands/Functionalities/Destroy_Entity")]
    public class ScriptableDestroyEntityCommand : ScriptableObject, ICommandParameter<GameObject>
    {
        //Notify command ends
        public event EventHandler<GameObject> CommandFinishedEvent = null;

        //Destroy entity
        public void Execute(GameObject parameter)
        {
            CommandFinishedEvent?.Invoke(this, parameter);
            MonoBehaviour.Destroy(parameter);            
        }
    }
}