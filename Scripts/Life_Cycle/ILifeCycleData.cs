using MECS.Variables.References;

namespace MECS.LifeCycle
{
    //* Interface used to store life cycle data values
    public interface ILifeCycleData
    {
        //Variables
        public BoolReference HasAwakeResponse { get; }
        public BoolReference HasEnableResponse { get; }
        public BoolReference HasStartResponse { get; }
        public BoolReference HasDisableResponse { get; }
        public BoolReference HasDestroyResponse { get; }
    }
}