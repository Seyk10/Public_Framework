using System;
using MECS.Variables;
using UnityEngine;

namespace MECS.Input
{
    //* Store related input to keyboard
    [Serializable]
    public class MouseInput
    {
        //Editor variables
        [SerializeField] private ButtonInputStates leftClickInput = new();
        [SerializeField] private ButtonInputStates rightClickInput = new();
        [SerializeField] private Vector2Variable mouseScreenPosition = null;
        [SerializeField] private Vector2Variable mouseDeltaPosition = null;
        [SerializeField] private Vector3Variable mouseWorldPosition = null;
        public ButtonInputStates LeftClickInput => leftClickInput;
        public ButtonInputStates RightClickInput => rightClickInput;
        public Vector2Variable MouseScreenPosition { get => mouseScreenPosition; set => mouseScreenPosition = value; }
        public Vector2Variable MouseDeltaPosition { get => mouseDeltaPosition; set => mouseDeltaPosition = value; }
        public Vector3Variable MouseWorldPosition { get => mouseWorldPosition; set => mouseWorldPosition = value; }
    }
}