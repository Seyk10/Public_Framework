using System.Collections.Generic;
using NUnit.Framework;
using MECS.Collections;

namespace MECS.Tests.Tools
{
    //* Run tests of list executions on CollectionTools
    public class CollectionListToolsTests
    {
        //Test the creation of auxiliary lists
        [Test]
        public void TestAuxiliaryList()
        {
            // //Create and copy list            
            // List<int> originalList = new();
            // originalList.Add(1);

            // List<int> auxiliaryList = CollectionsTools.GetAuxiliaryList(originalList);

            // //Check value and count are the same
            // Assert.AreEqual(originalList.Count, auxiliaryList.Count);
            // Assert.AreEqual(originalList[0], auxiliaryList[0]);
        }

        //Test if save adding works as expected
        [Test]
        public void TestListAdd()
        {
            // //Create a array to test object adding
            // int[] obj = new int[] { 1 };

            // //Create list and add value
            // List<int[]> list = new();

            // //Make safe add
            // Assert.IsTrue(CollectionsTools.SafeListAdd(list, obj));
            // //Check if list has value
            // Assert.AreEqual(list[0], obj);
            // //Try to add same value again and Make safe add
            // Assert.IsFalse(CollectionsTools.SafeListAdd(list, obj));
            // //Check still with same count
            // Assert.AreEqual(list.Count, 1);
        }

        //Test if save removing works as expected
        [Test]
        public void TestListRemove()
        {
            // //Create a array to test object adding
            // int[] obj = new int[] { 1 };

            // //Create list and add value
            // List<int[]> list = new();
            // list.Add(obj);

            // //Make safe remove
            // Assert.IsTrue(CollectionsTools.SafeListRemove(list, obj));
            // //Try to remove same value again and make safe remove
            // Assert.IsFalse(CollectionsTools.SafeListRemove(list, obj));
            // //Check if list has value
            // Assert.AreEqual(list.Count, 0);
        }

        // //Test if given array has given value inside
        // [Test]
        // public void TestHasArrayObject()
        // {
        //     //Create value and array
        //     int value = 1;
        //     int[] array = new int[] { value };

        //     //Check if array has value
        //     Assert.IsTrue(CollectionsTools.HasArrayObject(array, value));

        //     //Create a array
        //     int[] secondArray = new int[1];

        //     //Check if array has value
        //     Assert.IsFalse(CollectionsTools.HasArrayObject(secondArray, value));
        // }
    }
}