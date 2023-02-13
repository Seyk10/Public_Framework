using System.Collections.Generic;
using MECS.Collections;
using MECS.Patrons.Commands;
using MECS.Tools;
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
            if (!CollectionsTools.listTools.ListContainsType(statesList, state.GetType(), out IState value))
            {
                //Add value
                canAdd = true;
                statesList.Add(state);
            }
            else
                new NotificationCommand<DebugArgs>(this, new DebugArgs(" state machine already contains given type", LogType.Error,
                    new System.Diagnostics.StackTrace(true))).Execute();

            return canAdd;
        }

        //Remove a state from available states and return if state was removed, only allow one object of the same type
        public bool RemoveState(IState state) =>
            CollectionsTools.listTools.ListRemoveType(statesList, state.GetType(), " couldnt remove given type");


        //Set a state from the availble states
        // T2 = Type of state to be used
        public void SetState<T2>() where T2 : T
        {
            //Check if list have given type and it inst running
            bool isStateRunning = currentState is T2,
            canSetState = CollectionsTools.listTools.ListContainsType<IState, T2>(statesList, out T2 state,
            " state machine doesnt contains given state type")
            && !isStateRunning;

            //Check if can set state
            if (canSetState)
            {
                //Set state and run it
                IState newState = (IState)state;
                currentState = newState;
                newState.RunState();
            }
            //Debug if state is already running
            else if (isStateRunning)
                new NotificationCommand<DebugArgs>(this, new DebugArgs(" state is already running", LogType.Error,
                new System.Diagnostics.StackTrace(true))).Execute();
        }

        //Check if state machine list has given type add to state list
        public bool ContainsState(IState state) =>
            CollectionsTools.listTools.ListContainsType<IState>(statesList, state.GetType(), out IState listValue);

        //Check if state machine list has given type add to state list
        public bool ContainsState<T2>() =>
            CollectionsTools.listTools.ListContainsType<IState, T2>(statesList, out T2 state);
    }
}