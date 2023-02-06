using MECS.Patrons.Observers;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace MECS.Input
{
    //* Tracking command to keep values from mouse inputs
    public class MouseTrackingListener : IListener
    {
        //Variables
        private readonly MouseInput mouseInput = null;
        private readonly GenericInputActions genericInputActions = null;
        private readonly TrackButtonStatesCommand leftClickInputTracking = null;
        private readonly TrackButtonStatesCommand rightClickInputTracking = null;
        private readonly TrackValueCommand<Vector2> mouseScreenPositionTracking = null;
        private readonly TrackValueCommand<Vector2> mouseDeltaPositionTracking = null;
        private bool isSubscribed = false;
        public bool IsSubscribed => isSubscribed;

        //Default builder
        public MouseTrackingListener(MouseInput mouseInput, GenericInputActions genericInputActions)
        {
            this.mouseInput = mouseInput;
            this.genericInputActions = genericInputActions;
            leftClickInputTracking = new TrackButtonStatesCommand(mouseInput.LeftClickInput);
            rightClickInputTracking = new TrackButtonStatesCommand(mouseInput.RightClickInput);
            mouseScreenPositionTracking = new TrackValueCommand<Vector2>(mouseInput.MouseScreenPosition);
            mouseDeltaPositionTracking = new TrackValueCommand<Vector2>(mouseInput.MouseDeltaPosition);
        }

        //Convert screen mouse position to world position and set on variable
        private void TrackMouseWorldPosition(object sender, CallbackContext args)
        {
            //Store current mouse screen position
            Vector2 screenPosition = mouseInput.MouseScreenPosition.Value;

            //Store main camera
            Camera mainCamera = Camera.main;

            //Avoid missing references
            if (mainCamera != null)
                //Save world position of screen point
                mouseInput.MouseWorldPosition.Value = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: Missing main camera, cant track mouse world position.");
#endif
        }

        //Subscribe to mouse inputs
        public void Subscribe()
        {
            //Avoid subscribe multiple times
            if (!isSubscribed)
            {
                isSubscribed = true;

                //Make subscriptions
                genericInputActions.Mouse.LeftClick.performed += leftClickInputTracking.Execute;
                genericInputActions.Mouse.LeftClick.canceled += leftClickInputTracking.Execute;
                genericInputActions.Mouse.RightClick.performed += rightClickInputTracking.Execute;
                genericInputActions.Mouse.RightClick.canceled += rightClickInputTracking.Execute;
                genericInputActions.Mouse.MousePosition.performed += mouseScreenPositionTracking.Execute;
                genericInputActions.Mouse.MouseDelta.performed += mouseDeltaPositionTracking.Execute;
                mouseScreenPositionTracking.CommandFinishedEvent += TrackMouseWorldPosition;
            }
        }

        //Unsubscribe from mouse inputs
        public void Unsubscribe()
        {
            //Avoid unsubscribe multiple times
            if (!isSubscribed)
            {
                isSubscribed = false;

                //Make unsubscribe
                genericInputActions.Mouse.LeftClick.performed -= leftClickInputTracking.Execute;
                genericInputActions.Mouse.LeftClick.canceled -= leftClickInputTracking.Execute;
                genericInputActions.Mouse.RightClick.performed -= rightClickInputTracking.Execute;
                genericInputActions.Mouse.RightClick.canceled -= rightClickInputTracking.Execute;
                genericInputActions.Mouse.MousePosition.performed -= mouseScreenPositionTracking.Execute;
                genericInputActions.Mouse.MouseDelta.performed -= mouseDeltaPositionTracking.Execute;
                mouseScreenPositionTracking.CommandFinishedEvent -= TrackMouseWorldPosition;
            }
        }

        //Unsubscribe on default destructor
        ~MouseTrackingListener() => Unsubscribe();
    }
}