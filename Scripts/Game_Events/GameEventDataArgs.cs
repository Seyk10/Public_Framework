using MECS.Core;
using MECS.Patrons.Observers;

namespace MECS.GameEvents
{
    //* Args class used to share the observer who made the notification and component data 
    public class GameEventDataArgs : DataArgs<GameEventListenerData>
    {
        //Variables
        private readonly IObserver observer = null;
        public IObserver Observer => observer;

        //Base builder
        public GameEventDataArgs(IObserver observer, GameEventListenerData[] dataArray) : base(dataArray) => this.observer = observer;
    }
}