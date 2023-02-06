namespace MECS.Tests.Functionalities
{
    //* Tests used to verify timer functionalities
    public class TimerTests
    {
        // //Set timer configuration for tests
        // private TimerComponent SetTimerConfiguration(out TimerStateMachine targetStateMachine)
        // {
        //     //Create instances
        //     TimerComponent component = new GameObject().AddComponent<TimerComponent>();
        //     //Set data
        //     TimerData[] data = new TimerData[] { new TimerData() };
        //     data[0].CurrentTime = 3f;

        //     //Set data to use
        //     component.DataReference.localValue = data;
        //     component.DataReference.useLocalValues = true;

        //     //Out state machine to use
        //     targetStateMachine = data[0].StateMachine;

        //     return component;
        // }

        // //Test initialization state on timers
        // [UnityTest]
        // public IEnumerator InitializeTimer()
        // {
        //     //Create component
        //     TimerComponent component = SetTimerConfiguration(out TimerStateMachine targetStateMachine);

        //     //Set state on state machine
        //     targetStateMachine.AddState(new InitializeTimerState(component));
        //     targetStateMachine.SetState<InitializeTimerState>();

        //     //Check current time is set to default time value
        //     Assert.AreEqual(0, component.DataReference.GetValue()[0].CurrentTime);

        //     //Clean entities
        //     MonoBehaviour.Destroy(component.gameObject);

        //     yield return null;
        // }

        // //Test running state on timers
        // [UnityTest]
        // public IEnumerator RunTimer()
        // {
        //     //Create component
        //     TimerComponent component = SetTimerConfiguration(out TimerStateMachine targetStateMachine);

        //     //Set state on state machine, run timer and check current time            
        //     targetStateMachine.AddState(new RunTimerState(component));
        //     targetStateMachine.SetState<RunTimerState>();

        //     //Wait to timer
        //     Debug.LogWarning("Warning: Delay used to wait timer of 4 seconds.");
        //     yield return new WaitForSeconds(component.DataReference.GetValue()[0].CurrentTime + 1);

        //     //Check current time is set to default time value
        //     Assert.AreEqual(0, component.DataReference.GetValue()[0].CurrentTime);

        //     //Clean entities
        //     MonoBehaviour.Destroy(component.gameObject);

        //     yield return null;
        // }

        // //Test pause state on timers
        // [UnityTest]
        // public IEnumerator PauseTimer()
        // {
        //     //Create component
        //     TimerComponent component = SetTimerConfiguration(out TimerStateMachine targetStateMachine);

        //     //Add pause state
        //     targetStateMachine.AddState(new PauseTimerState(component));

        //     //Set run on state machine, run timer and check current time            
        //     targetStateMachine.AddState(new RunTimerState(component));
        //     targetStateMachine.SetState<RunTimerState>();

        //     //Wait to timer
        //     Debug.LogWarning("Warning: Delay used to wait timer of " +component.DataReference.GetValue()[0].CurrentTime+ " seconds.");
        //     yield return new WaitForSeconds(component.DataReference.GetValue()[0].CurrentTime);

        //     //Set pause state
        //     targetStateMachine.SetState<PauseTimerState>();

        //     //Check current time is different to 0
        //     Assert.IsTrue(component.DataReference.GetValue()[0].CurrentTime != 0);

        //     //Clean entities
        //     MonoBehaviour.Destroy(component.gameObject);

        //     yield return null;
        // }

        // //Test stop state on timers
        // [UnityTest]
        // public IEnumerator StopTimer()
        // {
        //      //Create component
        //     TimerComponent component = SetTimerConfiguration(out TimerStateMachine targetStateMachine);

        //     //Set stop on state machine
        //     targetStateMachine.AddState(new StopTimerState(component));
        //     targetStateMachine.SetState<StopTimerState>();            

        //     //Check current time is set to default time value
        //     Assert.AreEqual(0, component.DataReference.GetValue()[0].CurrentTime);

        //     //Clean entities
        //     MonoBehaviour.Destroy(component.gameObject);

        //     yield return null;
        // }
    }
}