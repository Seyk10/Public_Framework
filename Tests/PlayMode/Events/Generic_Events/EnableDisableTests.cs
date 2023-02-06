namespace MECS.Tests.Events
{
    //* Unit tests used to verify enable/disable detections
    public class EnableDisableTests
    //ISceneTest<EventComponent<EventData, GenericEventProfile, DataArgs<EventData>>>
    {
        //Test enable/disable components
        // public IEnumerator SetTestingScene<T2>() where T2 : EventComponent<EventData, GenericEventProfile, DataArgs<EventData>>
        // {
        //     //Variables
        //     bool hasRaise = false;
        //     T2 component = new InstantiateEntityAndComponentCommand<T2>("Testing_Entity").Execute();

        //     //Set subscription
        //     component.EventRaise += (sender, args) => { hasRaise = true; };

        //     //Force event raise
        //     GameObject testingEntity = component.gameObject;
        //     testingEntity.SetActive(false);
        //     testingEntity.SetActive(true);

        //     //Check if event has raised
        //     Assert.IsTrue(hasRaise);

        //     //Clean
        //     MonoBehaviour.Destroy(testingEntity);

        //     yield return null;
        // }

        // // Check if enable event is raised
        // [UnityTest]
        // public IEnumerator EnableResponse() => SetTestingScene<EnableEventComponent>();

        // // Check if disable event is raised
        // [UnityTest]
        // public IEnumerator DisableResponse() => SetTestingScene<DisableEventComponent>();
    }
}