using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MECS.Entities.Commands;

namespace MECS.Tests.Entities
{
    //* Class used to test commands related to entities
    public class EntitiesCommandsTests
    {
        //Check InstantiateEntityAndComponentCommand
        [UnityTest]
        public IEnumerator InstantiateEntityAndComponentCommand()
        {
            //Check entity creation with only custom name
            static void CheckEntityCreationWithCustomName()
            {
                Rigidbody component = new InstantiateEntityAndComponentCommand<Rigidbody>("Testing_Entity").Execute();
                //Check reference and name
                Assert.IsTrue(component != null);
                Assert.AreEqual("Testing_Entity", component.gameObject.name);

                //Avoid break other tests
                MonoBehaviour.Destroy(component.gameObject);
            }
            //Execute code
            CheckEntityCreationWithCustomName();

            //Check prefab creation
            static void CheckPrefabCreation()
            {
                GameObject prefab = new GameObject();
                Rigidbody component = new InstantiateEntityAndComponentCommand<Rigidbody>(prefab, Vector3.zero, Quaternion.identity).Execute();
                //Check reference, position and rotation
                Assert.IsTrue(component != null);
                Assert.AreEqual(component.transform.position, Vector3.zero);
                Assert.AreEqual(component.transform.rotation, Quaternion.identity);

                //Avoid break other tests
                MonoBehaviour.Destroy(component.gameObject);
            }
            //Execute code
            CheckPrefabCreation();

            yield return null;
        }
    }
}
