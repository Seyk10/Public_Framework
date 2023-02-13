using System;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Conditionals
{
    //* Base class for all scriptable conditionals commands
    //* T = ConditionalComponent
    public class AScriptableConditionalCommand<T> : AScriptableParameterCommand<ConditionalComponent>
    {
        //Editor variable
        [SerializeField] private T value = default;

        //Attributes
        public T Value { get => value; set => this.value = value; }

        //Event, notify use of scriptable command
        public static event EventHandler<ScriptableConditionalArgs<T>> ExecuteCommandEvent = null;

        //AScriptableParameterCommand, notify use of conditional command
        public override void Execute(ConditionalComponent parameter)
        {
            ExecuteCommandEvent?.Invoke(this, new ScriptableConditionalArgs<T>(value, parameter,
            " couldnt execute conditional command"));

            //AScriptableParameterCommand, base execution
            base.Execute(parameter);
        }
    }
}