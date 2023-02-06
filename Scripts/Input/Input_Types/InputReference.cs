using System;
using MECS.Events;
using MECS.GameEvents;
using MECS.Variables;
using UnityEngine;

namespace MECS.Input
{
    //* Reference used to store variable and events related to input
    [Serializable]
    public class InputReference<T>
    {
        //Editor variables
        [SerializeField] private AVariable<T> variableValue = null;
        [SerializeField] private GameEvent gameEvent = null;
        [SerializeField] private EventReference eventReference = new();
        //Attributes
        public EventReference EventReference => eventReference;
        public AVariable<T> VariableValue { get => variableValue; set => variableValue = value; }
        public GameEvent GameEvent { get => gameEvent; set => gameEvent = value; }
    }
}