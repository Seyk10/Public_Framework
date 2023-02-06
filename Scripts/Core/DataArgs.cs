using System;

namespace MECS.Core
{
    //* Class used as args for data structures
    //* T = AData type
    public class DataArgs<T> : EventArgs where T : AData
    {
        //Data from component
        public readonly T[] dataArray = null;

        //Default constructor, store component data
        public DataArgs(T[] dataArray) => this.dataArray = dataArray;
    }
}