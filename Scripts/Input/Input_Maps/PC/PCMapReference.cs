using System;
using UnityEngine;

namespace MECS.Input
{
    //* Structure to hold all the pc mapping inputs
    [Serializable]
    public class PCMapReferences
    {
        //Editor variables
        [SerializeField] private MouseInput mouseInput = new();
        [SerializeField] private KeyboardInput keyboardInput = new();
        public MouseInput MouseInput => mouseInput;
        public KeyboardInput KeyboardInput => keyboardInput;
    }
}