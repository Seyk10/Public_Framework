using System;
using MECS.Patrons.Commands;
using MECS.Tools;
using UnityEngine;

namespace MECS.Core
{
    //* Set an uniq ID to entity 
    public class SetEntityUniqIDCommand : ICommand
    {
        //Variables
        private readonly GameObject gameObject = null;

        //Default builder
        public SetEntityUniqIDCommand(GameObject gameObject) => this.gameObject = gameObject;

        //ICommand event, notify end of command
        public event EventHandler CommandFinishedEvent = null;

        //ICommand method, set uniq ID to entity
        public void Execute()
        {
            //Set uniq ID name on this entity
            string currentEntityName = gameObject.name,
            uniqName = StringTools.GetUniqID(gameObject.name, gameObject);

            //Check if uniq ID is already set
            if (!currentEntityName.Equals(uniqName))
                gameObject.name = uniqName;

            //Notify end of command
            CommandFinishedEvent?.Invoke(this, null);
        }
    }
}