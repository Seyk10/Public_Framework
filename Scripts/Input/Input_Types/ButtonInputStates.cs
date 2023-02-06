using System;
using UnityEngine;

namespace MECS.Input
{
    //TODO: Add all the action phases
    //* Store button states of input
    [Serializable]
    public class ButtonInputStates
    {
        //Editor variables
        [SerializeField] private InputReference<bool> buttonPerformed = new();
        [SerializeField] private InputReference<bool> buttonCanceled = new();
        public InputReference<bool> ButtonPerformed => buttonPerformed;
        public InputReference<bool> ButtonCanceled => buttonCanceled;
    }
}