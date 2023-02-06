namespace MECS.Tests.Events
{
    //* Class used to test game event functionalities
    public class GameEventTests
    {

        // //Load and invoke game event to see if the process is working
        // [UnityTest]
        // public IEnumerator GameEventListenerRespond()
        // {
        //     //Variables, create the asset and component to test
        //     GameEvent gameEvent = ScriptableObject.CreateInstance<GameEvent>();
        //     GameEventListenerComponent component = new GameObject("Entity").AddComponent<GameEventListenerComponent>();

        //     //Add properties, subject to game event and event response to component
        //     gameEvent.Observer.AddSubject(component);

        //     component.DataReference.useLocalValues = true;
        //     component.DataReference.localValue = new GameEventListenerData[] { new GameEventListenerData() };
        //     component.DataReference.GetValue()[0].EventReference.ActionResponse += () => { Assert.IsTrue(true); };

        //     //Force raise
        //     gameEvent.Observer.RaiseSubjects();

        //     //Clean
        //     MonoBehaviour.Destroy(component.gameObject);
        //     MonoBehaviour.Destroy(gameEvent);

        //     yield return null;
        // }
    }
}