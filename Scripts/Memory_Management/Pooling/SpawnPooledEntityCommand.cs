using System;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* Command used to notify the spawn of a entity from a given pool
    [CreateAssetMenu(fileName = "New_Spawn_Pooled_Entity_Command", menuName = "MECS/Commands/Memory_Management/Pooled/Spawn_Pooled_Entity")]
    public class SpawnPooledEntityCommand : ScriptableObject, ICommandParameter<Transform>
    {
        //Editor variables
        [SerializeField] private AddresableEntityPool addresableEntityPool = null;

        //Event
        //Notify the spawn of a new entity
        public static event EventHandler<SpawnPooledEntityArgs> SpawnPooledEntityEvent = null;
        //Notify when command ends
        public event EventHandler<Transform> CommandFinishedEvent = null;

        //Notify the spawn of an entity on given transform values
        public void Execute(Transform parameter)
        {
            SpawnPooledEntityEvent?.Invoke(this, new SpawnPooledEntityArgs(parameter, addresableEntityPool,
            " couldnt spawn from pool"));
            CommandFinishedEvent?.Invoke(this, parameter);
        }
    }
}