namespace MECS.Variables
{
    //* Interfaced used on numeric variables to make values variations on increment or decrement
    //* T = numeric type
    public interface INumericVariable<T>
    {
        //Make a increment on variable value
        public void MakeIncrementOnValue(T variation);

        //Make a decrement on variable value
        public void MakeDecrementOnValue(T variation);
    }
}