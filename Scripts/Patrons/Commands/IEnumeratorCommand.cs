using System;
using System.Collections;
using UnityEngine;

namespace MECS.Patrons.Commands
{
    //* Interface used on commands with threat like functions and loops
    public interface IEnumeratorCommand 
    {
        //Event, notify when command ends
        public event EventHandler CommandFinishedEvent;
        //Store execution on play
        public Coroutine Coroutine { get; }
        //Coroutine execution
        public IEnumerator Execute();
    }
}