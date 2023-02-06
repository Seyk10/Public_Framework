using System;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Command used to initialize a pool content
    //* T = AddresableEntityPool
    [CreateAssetMenu(fileName = "New_Initialize_Pool_Command", menuName = "MECS/Commands/Memory_Management/Initialize_Pool")]
    public class InitializePoolCommand : ScriptableObject, ICommandParameter<AddresableEntityPool>
    {
        //ICommandParameter, notify when command ends
        public event EventHandler<AddresableEntityPool> CommandFinishedEvent = null;
        //Notify which pool has to be initialized
        public static event EventHandler<AddresableEntityPool> InitializePoolEvent = null;

        public void Execute(AddresableEntityPool parameter)
        {
            //Notify events
            InitializePoolEvent?.Invoke(this, parameter);
            CommandFinishedEvent?.Invoke(this, parameter);
        }
    }
}
