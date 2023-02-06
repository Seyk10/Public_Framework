using System;
using MECS.Patrons.Observers;

namespace MECS.Input
{
    //* Tracking command to keep values from keyboard inputs
    public class KeyboardTrackingListener : IListener
    {
        //Variables
        private readonly KeyboardInput keyboardInput = null;
        private readonly GenericInputActions genericInputActions = null;
        private readonly TrackButtonStatesCommand wButtonInputTracking = null;
        private readonly TrackButtonStatesCommand aButtonInputTracking = null;
        private readonly TrackButtonStatesCommand sButtonInputTracking = null;
        private readonly TrackButtonStatesCommand dButtonInputTracking = null;
        private readonly TrackButtonStatesCommand spaceButtonInputTracking = null;
        private readonly TrackButtonStatesCommand escButtonInputTracking = null;
        private bool isSubscribed = false;
        public bool IsSubscribed => isSubscribed;

        //Default variables
        public KeyboardTrackingListener(KeyboardInput keyboardInput, GenericInputActions genericInputActions)
        {
            this.keyboardInput = keyboardInput;
            this.genericInputActions = genericInputActions;
            wButtonInputTracking = new TrackButtonStatesCommand(keyboardInput.WButtonInput);
            aButtonInputTracking = new TrackButtonStatesCommand(keyboardInput.AButtonInput);
            sButtonInputTracking = new TrackButtonStatesCommand(keyboardInput.SButtonInput);
            dButtonInputTracking = new TrackButtonStatesCommand(keyboardInput.DButtonInput);
            spaceButtonInputTracking = new TrackButtonStatesCommand(keyboardInput.SpaceButtonInput);
            escButtonInputTracking = new TrackButtonStatesCommand(keyboardInput.EscButtonInput);
        }

        //IListener, set all the subscriptions to input parameters
        public void Subscribe()
        {
            //Execute only if wasn't subscribed before
            if (!isSubscribed)
            {
                isSubscribed = true;

                //Set subscriptions
                genericInputActions.Keyboard.W.performed += wButtonInputTracking.Execute;
                genericInputActions.Keyboard.W.canceled += wButtonInputTracking.Execute;
                genericInputActions.Keyboard.A.performed += aButtonInputTracking.Execute;
                genericInputActions.Keyboard.A.canceled += aButtonInputTracking.Execute;
                genericInputActions.Keyboard.S.performed += sButtonInputTracking.Execute;
                genericInputActions.Keyboard.S.canceled += sButtonInputTracking.Execute;
                genericInputActions.Keyboard.D.performed += dButtonInputTracking.Execute;
                genericInputActions.Keyboard.D.canceled += dButtonInputTracking.Execute;
                genericInputActions.Keyboard.Space.performed += spaceButtonInputTracking.Execute;
                genericInputActions.Keyboard.Space.canceled += spaceButtonInputTracking.Execute;
                genericInputActions.Keyboard.Esc.performed += escButtonInputTracking.Execute;
                genericInputActions.Keyboard.Esc.canceled += escButtonInputTracking.Execute;
            }
        }

        //IListener, clean all subscriptions to inputs
        public void Unsubscribe()
        {
            //Execute only if was subscribed before
            if (isSubscribed)
            {
                isSubscribed = false;

                //Remove subscriptions
                genericInputActions.Keyboard.W.performed -= wButtonInputTracking.Execute;
                genericInputActions.Keyboard.W.canceled -= wButtonInputTracking.Execute;
                genericInputActions.Keyboard.A.performed -= aButtonInputTracking.Execute;
                genericInputActions.Keyboard.A.canceled -= aButtonInputTracking.Execute;
                genericInputActions.Keyboard.S.performed -= sButtonInputTracking.Execute;
                genericInputActions.Keyboard.S.canceled -= sButtonInputTracking.Execute;
                genericInputActions.Keyboard.D.performed -= dButtonInputTracking.Execute;
                genericInputActions.Keyboard.D.canceled -= dButtonInputTracking.Execute;
                genericInputActions.Keyboard.Space.performed -= spaceButtonInputTracking.Execute;
                genericInputActions.Keyboard.Space.canceled -= spaceButtonInputTracking.Execute;
                genericInputActions.Keyboard.Esc.performed -= escButtonInputTracking.Execute;
                genericInputActions.Keyboard.Esc.canceled -= escButtonInputTracking.Execute;
            }
        }

        //Default destructor
        ~KeyboardTrackingListener() => Unsubscribe();
    }
}