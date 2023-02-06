using MECS.Collections;
using UnityEngine;

namespace MECS.Patrons.StateMachine
{
    //* Interface used on objects with set state functionalities
    //* T = Data interface type
    //* T2 = State type
    public interface ISetState<T, T2> where T : class where T2 : AState<T>
    {
        //Initialize state machine states
        public void InitializeStateMachineStates(object sender, ResponsiveDictionaryArgs<T, MonoBehaviour> args);

        //Set state on state machine inside a component, use state types
        // T3 = StateType
        public void SetStateResponse<T3>(object sender, T data) where T3 : T2;

        //Set state on response to editor commands
        //T3 = StateType
        public void RespondEditorStateCommand<T3>(object sender, MonoBehaviour component) where T3 : T2;
    }
}