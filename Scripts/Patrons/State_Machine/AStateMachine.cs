using System.Collections.Generic;
using MECS.Collections;
using UnityEngine;

namespace MECS.Patrons.StateMachine
{
    //* Base class used on state machine type objects
    //* T = Base type of states
    public abstract class AStateMachine<T>
    {
        //Variables
        private readonly List<IState> statesList = new();
        private IState currentState = null;
        public IState CurrentState { get => currentState; }

        //Methods
        //Add a state to the availble states and return if state was added, only allow one object of the same type
        public bool AddState(IState state)
        {
            bool canAdd = false;

            //Check if list contains type
            if (!CollectionsTools.ListContainsType(statesList, state.GetType(), out IState value))
            {
                //Add value
                canAdd = true;
                statesList.Add(state);
            }
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: State machine already contains given type.");
#endif

            return canAdd;
        }

        //Remove a state from available states and return if state was removed, only allow one object of the same type
        public bool RemoveState(IState state)
        {
            //Variables
            bool returnValue = true;

            //Check if list have given type
            if (!CollectionsTools.ListRemoveType(statesList, state.GetType()))
            {
                returnValue = false;
#if UNITY_EDITOR
                Debug.LogWarning("Warning: Given type doesnt exist in availble states of state machine.");
#endif
            }

            return returnValue;
        }

        //Set a state from the availble states
        // T2 = Type of state to be used
        public void SetState<T2>() where T2 : T
        {
            //Check if list have given type and it inst running
            bool isStateRunning = currentState is T2,
            canSetState = CollectionsTools.ListContainsType<IState, T2>(statesList, out T2 state) && !isStateRunning;

            //Check if can set state
            if (canSetState)
            {
                //Set state and run it
                IState newState = (IState)state;
                currentState = newState;
                newState.RunState();
            }
#if UNITY_EDITOR
            else if (isStateRunning) Debug.LogWarning("Warning: State is already running.");
            else Debug.LogWarning("Warning: State machine doesnt contains given state type.");
#endif
        }

        //Check if state machine list has given type add to state list
        public bool ContainsState(IState state) => CollectionsTools.ListContainsType<IState>(statesList, state.GetType(), out IState listValue);

        //Check if state machine list has given type add to state list
        public bool ContainsState<T2>() => CollectionsTools.ListContainsType<IState, T2>(statesList, out T2 state);
    }
}