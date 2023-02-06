// - <r> pcMapReferences : PCMapReferences
// - <r> genericInputActions : GenericInputActions
// - <r> keyboardTrackingListener : KeyboardTrackingListener
// - <r> mouseTrackingListener: MouseTrackingListener

// + TrackButtonStatesCommand(pcMapReferences : PCMapReferences, genericInputActions : GenericInputActions)
// + Execute()
// + Dispose()
// ~ PCInputTrackingCommand
using System;
using MECS.Patrons.Commands;

namespace MECS.Input
{
    //* Command used to track all the necessary inputs on PC
    public class PCInputTrackingCommand : ICommand, IDisposable
    {
        //Variables
        private readonly PCMapReferences pcMapReferences = null;
        private readonly GenericInputActions genericInputActions = null;
        private readonly KeyboardTrackingListener keyboardTrackingListener = null;
        private readonly MouseTrackingListener mouseTrackingListener = null;
        private bool isDisposed = true;
        //Notify when commands ends
        public event EventHandler CommandFinishedEvent = null;

        //Default builder
        public PCInputTrackingCommand(PCMapReferences pcMapReferences, GenericInputActions genericInputActions)
        {
            this.pcMapReferences = pcMapReferences;
            this.genericInputActions = genericInputActions;
            //Set trackings
            keyboardTrackingListener = new KeyboardTrackingListener(pcMapReferences.KeyboardInput, genericInputActions);
            mouseTrackingListener = new MouseTrackingListener(pcMapReferences.MouseInput, genericInputActions);
        }

        //ICommand, start all the tracking listeners
        public void Execute()
        {
           //Avoid execute twice
           if(isDisposed)
           {
               isDisposed = false;

               //Start tracking
               keyboardTrackingListener.Subscribe();
               mouseTrackingListener.Subscribe();

               CommandFinishedEvent?.Invoke(this, null);
           }
        }

        //IDisposable, make sure to manage all the tracking listeners
        public void Dispose()
        {
           //Avoid execute twice
           if(!isDisposed)
           {
               isDisposed = true;

               //Start tracking
               keyboardTrackingListener.Unsubscribe();
               mouseTrackingListener.Unsubscribe();
           }
        }

        //Default destructor
        ~PCInputTrackingCommand() => Dispose();
    }
}