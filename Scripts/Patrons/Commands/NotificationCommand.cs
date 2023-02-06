using System;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Patrons.Commands
{
    //* Command used to make notifications
    //* T = Args to share
    public class NotificationCommand<T> : ICommand where T : EventArgs
    {
        //Variables
        private readonly object sender = null;
        private readonly T args = default;
        private readonly BasicDebugInformation basicDebugInformation = default;

        //Default builder
        public NotificationCommand(object sender, T args, BasicDebugInformation basicDebugInformation)
        {
            this.sender = sender;
            this.args = args;
            this.basicDebugInformation = basicDebugInformation;
        }

        //ICommand, notify when command ends
        public event EventHandler CommandFinishedEvent = null;
        public static event EventHandler<T> NotificationEvent = null;

        //ICommand, execute command content
        public void Execute()
        {
            //Check if parameters are valid
            bool canNotify = ReferenceTools.IsValueSafe(sender, new ComplexDebugInformation(basicDebugInformation, "notification sender is null"))
            && ReferenceTools.IsValueSafe(sender, new ComplexDebugInformation(basicDebugInformation, "notification args is null"));

            //Invoke only if can
            if (canNotify)
                NotificationEvent?.Invoke(sender, args);

            //Notify ends of command
            CommandFinishedEvent?.Invoke(sender, null);
        }
    }
}