using System;

namespace MECS.Variables.References
{
    //* Reference used to switch between numeric references
    //* T = IntReference
    //* T2 = FloatReference
    [Serializable]
    public class NumericReference : AReference<IntReference, FloatReference>, IVariableChange<float>, IDisposable
    {
        //IVariableChange, used to notify when any reference values change
        public event EventHandler<float> VariableChangeEvent = null;

        //Default builder
        public NumericReference()
        {
            //Subscribe to values changes

        }

        //IDisposable, unsubscribe to references values changes
        public void Dispose()
        {

        }
    }
}