using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Executions related to type operations
    public static class TypeTools
    {
        //Method, check if can convert to type object
        public static bool IsType<T>(object value) => ReferenceTools.IsValueSafe(value)
        && value is T;

        //Method, check if can convert to type object
        //T = target type
        public static bool IsType<T>(object value, ComplexDebugInformation complexDebugInformation)
        {
            //Return value
            bool isCorrect = ReferenceTools.IsValueSafe(value, complexDebugInformation.AddTempCustomText("given value isn't safe"));

            //Check type
            if (isCorrect)
            {
                isCorrect = value is T;

                //Debug value
                if (!isCorrect)
                    DebugTools.DebugError(complexDebugInformation.AddTempCustomText("given values isn't target type, value type is "
                    + value.GetType().Name + " and target type is " + typeof(T).Name));
            }

            return isCorrect;
        }

        //Method, convert value to target type and out it
        public static bool ConvertToType<T>(object value, out T convertedObject)
        {
            //Out value
            convertedObject = default;

            //Return if object is given type
            return IsType<T>(value);
        }

        //Method, convert value to target type and out it
        public static bool ConvertToType<T>(object value, out T convertedObject, ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new("TypeTools",
            "ConvertToType<T>(object value, out T convertedObject, ComplexDebugInformation complexDebugInformation)");

            //Check parameters
            bool areParametersValid = ReferenceTools.AreValuesSafe(new object[] { value, complexDebugInformation },
            new ComplexDebugInformation(basicDebugInformation, "given parameters aren't valid"));

            //Check type
            bool isCorrectType = false;
            convertedObject = default;

            //Make executions
            if (areParametersValid)
            {
                isCorrectType = IsType<T>(value, complexDebugInformation.AddTempCustomText("given value isn't target type"));

                convertedObject = isCorrectType ? (T)value : default;
            }

            //Return if value is given type
            return isCorrectType;
        }
    }
}