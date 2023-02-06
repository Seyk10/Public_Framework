namespace MECS.Patrons.StateMachine
{
    //* Base interface used on simple states
    public interface IState 
    {
        //Methods, execute state functionality
        public void RunState();        
    }
}