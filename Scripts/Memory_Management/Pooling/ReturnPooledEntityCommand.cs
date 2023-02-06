using System;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Command used to return instances of entities to their pool
    //* T = AddresablePooledComponent
    [CreateAssetMenu(fileName = "New_Return_Pooled_Entity_Command", menuName = "MECS/Commands/Memory_Management/Pooled/Return_Pooled_Entity")]
    public class ReturnPooledEntityCommand : ScriptableObject, ICommandParameter<AddresablePooledComponent>
    {
        //Events        
        //ICommandParameter, notify when command ends
        public event EventHandler<AddresablePooledComponent> CommandFinishedEvent = null;
        //Notify to return entity
        public static event EventHandler<AddresablePooledComponent> ReturnPooledEntityEvent = null;

        //Notify the release of given component/entity
        public void Execute(AddresablePooledComponent parameter)
        {
            //Notify events
            ReturnPooledEntityEvent?.Invoke(this, parameter);
            CommandFinishedEvent?.Invoke(this, parameter);
        }
    }
}