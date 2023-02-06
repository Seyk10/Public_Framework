using System;
using UnityEngine;

namespace MECS.Input
{
    //* Store related input to keyboard
    [Serializable]
    public class KeyboardInput
    {
        //Editor variables
        //WASD
        [SerializeField] private ButtonInputStates wButtonInput = new();
        [SerializeField] private ButtonInputStates aButtonInput = new();
        [SerializeField] private ButtonInputStates sButtonInput = new();
        [SerializeField] private ButtonInputStates dButtonInput = new();
        //Misc
        [SerializeField] private ButtonInputStates escButtonInput = new();
        [SerializeField] private ButtonInputStates spaceButtonInput = new();
        //Attributes
        public ButtonInputStates WButtonInput => wButtonInput;
        public ButtonInputStates AButtonInput => aButtonInput;
        public ButtonInputStates SButtonInput => sButtonInput;
        public ButtonInputStates DButtonInput => dButtonInput;
        public ButtonInputStates SpaceButtonInput => spaceButtonInput;
        public ButtonInputStates EscButtonInput => escButtonInput;
    }
}