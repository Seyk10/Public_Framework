namespace MECS.Tests.Events
{
    //* Unit tests used to verify collisions detections
    public class ColliderTests
    //: ISceneTest<FilterComponent>
    {
        // //Set the configuration to test the collider events
        // public IEnumerator SetTestingScene<T2>() where T2 : FilterComponent
        // {
        //     //Variables
        //     //Create a collision enter component
        //     GameObject firstEntity = new GameObject(),
        //     secondEntity = new GameObject();

        //     bool hasRaiseEvent = false;

        //     //Local method add required components
        //     static void AddRequirements(GameObject entity)
        //     {
        //         //Add required components
        //         entity.AddComponent<Rigidbody>().useGravity = false;
        //         entity.AddComponent<BoxCollider>();
        //     }
        //     //Set first entity
        //     AddRequirements(firstEntity);
        //     //Set second entity
        //     AddRequirements(secondEntity);

        //     //Set locations
        //     firstEntity.transform.position = new Vector3(0.5f, 2, 0);
        //     secondEntity.transform.position = Vector3.zero;

        //     //Set components
        //     T2 firstComponent = firstEntity.AddComponent<T2>();

        //     //Subscribe to check event raise
        //     firstComponent.EventRaise += (sender, args) => { hasRaiseEvent = true; };

        //     firstComponent.GetComponent<Rigidbody>().useGravity = true;

        //     yield return new WaitForSeconds(1f);
        //     Debug.Log("Info: Delay of 1 second.");

        //     MonoBehaviour.Destroy(firstEntity);
        //     MonoBehaviour.Destroy(secondEntity);

        //     //Check assert, if has raise event
        //     Assert.IsTrue(hasRaiseEvent);
        // }

        // //Test collision enter callbacks
        // [UnityTest]
        // public IEnumerator CollisionEnterRespond() => SetTestingScene<CollisionComponent>();

        // //Test collision exit callbacks
        // [UnityTest]
        // public IEnumerator CollisionExitRespond() => SetTestingScene<CollisionExitComponent>();

        // //Test trigger enter callbacks
        // [UnityTest]
        // public IEnumerator TriggerEnterRespond() => SetTestingScene<TriggerEnterComponent>();

        // //Test trigger exit callbacks
        // [UnityTest]
        // public IEnumerator TriggerExitRespond() => SetTestingScene<TriggerExitComponent>();
    }
}