using System;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Entities.Commands
{
    //*Instantiate a entity with a component
    //*T = Component type
    public class InstantiateEntityAndComponentCommand<T> : ICommandReturn<T> where T : Component
    {
        //Set transform configuration
        private GameObject prefab = null;
        private readonly Vector3 position = Vector3.zero;
        private readonly Quaternion rotation = Quaternion.identity;
        private string entityName = null;

        public event EventHandler<T> CommandFinishedEvent = null;

        //Constructors
        //Set instance with default parameters
        public InstantiateEntityAndComponentCommand(string entityName) => this.entityName = entityName;

        //Take instantiate parameters
        public InstantiateEntityAndComponentCommand(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            //Set parameters
            this.prefab = prefab;
            this.position = position;
            this.rotation = rotation;
        }

        //Execute command and instantiate entity with component
        public T Execute()
        {
            //Return value
            GameObject entity = null;

            //Check if there is prefab and set values
            if (prefab) entity = MonoBehaviour.Instantiate(prefab, position, rotation);
            else
            {
                entity = new GameObject(entityName);
                entity.transform.position = position;
                entity.transform.rotation = rotation;
            }

            //Component to return
            T returnValue = entity.AddComponent<T>();

            //Notify command end
            CommandFinishedEvent?.Invoke(this, returnValue);

            return returnValue;
        }
    }
}