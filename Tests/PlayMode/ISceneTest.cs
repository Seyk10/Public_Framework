using System.Collections;

namespace MECS.Tests 
{
    //* Interface used to set values on scene before testing
    public interface ISceneTest<T>
    {
        //Set scene values
        public IEnumerator SetTestingScene<T2>() where T2 : T;
    }
}