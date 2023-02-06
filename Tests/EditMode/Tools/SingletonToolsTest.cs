using NUnit.Framework;
using MECS.Patrons.Singleton;

namespace MECS.Tests.Tools
{
    //* Test executions related to singleton tools
    public class SingletonToolsTest
    {
        // Check if can create singleton execution is correct
        [Test]
        public void TestCanSetInstance()
        {
            //Create instance to test and singleton instance
            int[] instance = new int[1];
            int[] testingSingleton = null;

            //Test if can set instance
            void CheckInstanceCreation() => Assert.IsTrue(SingletonTools.CanSetInstance(testingSingleton, instance));
            //Execute code
            CheckInstanceCreation();

            //Set singleton and try again
            void CheckAfterSetSingleton()
            {
                testingSingleton = instance;
                Assert.IsFalse(SingletonTools.CanSetInstance(testingSingleton, instance));
            }
            //Execute code
            CheckAfterSetSingleton();

            //Set on singleton a different instance and try again
            void CheckAfterSetNewSingleton()
            {
                testingSingleton = new int[2];
                Assert.AreEqual(SingletonTools.CanSetInstance(testingSingleton, instance), false);
            }
            //Execute code
            CheckAfterSetNewSingleton();
        }

        //Check singleton creation command
        [Test]
        public void TestCreateSingletonCommand()
        {
            //Create two instances
            int[] firstInstance = new int[1];
            int[] singleton = null;

            //Check if set singleton
            void CheckSetSingleton()
            {
                singleton = new CreateSingletonCommand<int[]>(singleton, firstInstance).Execute();
                Assert.AreEqual(singleton, firstInstance);
            }
            //Execute code
            CheckSetSingleton();

            //Try to add a second instance on singleton
            void CheckAddSecondInstance()
            {
                int[] secondInstance = new int[2];

                singleton = new CreateSingletonCommand<int[]>(singleton, secondInstance).Execute();
                Assert.AreEqual(singleton, firstInstance);
            }
            //Execute code
            CheckAddSecondInstance();
        }
    }
}