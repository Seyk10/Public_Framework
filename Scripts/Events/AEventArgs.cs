using System;

namespace MECS.Events
{
    //* Abstract class used on args with debug information implicated
    public abstract class AEventArgs : EventArgs
    {
        //Variables
        public readonly string debugMessage = null;

        //Default builder
        public AEventArgs(string debugMessage) => this.debugMessage = debugMessage;
    }
}