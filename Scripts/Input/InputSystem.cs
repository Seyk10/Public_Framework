using UnityEngine;
using UnityEngine.SceneManagement;

namespace MECS.Input
{
    //* System used to track input values to set variables and raise events
    [CreateAssetMenu(fileName = "New_Input_System", menuName = "MECS/Systems/Input")]
    public class InputSystem : ScriptableObject
    {
        //Editor variables        
        [SerializeField] private PCMapReferences pcMapReferences = new();

        //Variables
        private GenericInputActions genericInputActions = null;
        private PCInputTrackingCommand pcInputTrackingCommand = null;
        private bool isTracking = false;

        public PCMapReferences PcMapReferences => pcMapReferences;

        //Method, start input tracking
        public void TrackInputs(Scene lastScene, Scene newScene)
        {
            //Check if should set o manage values
            if (!isTracking)
            {
                isTracking = true;
                //Enable and set inputs
                genericInputActions = new GenericInputActions();
                genericInputActions.Enable();

                //Set new tracking command
                pcInputTrackingCommand = new(pcMapReferences, genericInputActions);
                pcInputTrackingCommand.Execute();
            }
        }

        //ScriptableObject
        //Subscribe to input notifications
        private void OnEnable() =>
            //Start tracking when active scene change
            SceneManager.activeSceneChanged += TrackInputs;


        //Unsubscribe from input notifications
        private void OnDisable()
        {
            //Start tracking when active scene change
            SceneManager.activeSceneChanged -= TrackInputs;
            //Manage input tracking
            if (isTracking)
            {
                isTracking = false;
                //Release tracking command
                pcInputTrackingCommand.Dispose();
                pcInputTrackingCommand = null;

                //Disable and remove inputs
                genericInputActions.Disable();
                genericInputActions = null;
            }
        }
    }
}