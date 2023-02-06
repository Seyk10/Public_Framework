using UnityEngine;

namespace MECS.Core
{
    //* Base class for all the profile files
    //* T = Data type
    public abstract class AProfile<T> : ScriptableObject, IDataArray<T> where T : AData
    {
        //Editor variables
        [TextArea(3, 7)]
#pragma warning disable 0414
        [SerializeField] private string dataDescription = null;
#pragma warning disable 0414

        [SerializeField] T[] data = default;

        public T[] Data { get => data; set => data = value; }
    }
}