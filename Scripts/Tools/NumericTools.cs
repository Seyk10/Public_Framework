using MECS.Conditionals;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Tools used on operations related with numbers
    public static class NumericTools
    {
        //Method, check if given number comparative is correct
        //Comparatives are made from value01 to value02
        public static bool IsComparativeCorrect(float value01, float value02, ENumericConditional comparativeType,
        ComplexDebugInformation complexDebugInformation)
        {
            //return value
            bool isCorrect = true;

            //Check numeric conditional cases
            switch (comparativeType)
            {
                case ENumericConditional.Bigger:
                    //Check if value01 is bigger than value02
                    if (value01 <= value02)
                    {
                        isCorrect = false;
#if UNITY_EDITOR
                        DebugTools.DebugError(complexDebugInformation.AddTempCustomText("value01 isn't bigger than value02"));
#endif
                    }
                    break;

                case ENumericConditional.BiggerOrEqual:
                    //Check if value01 is bigger or equal than value02
                    if (value01 < value02)
                    {
                        isCorrect = false;
#if UNITY_EDITOR
                        DebugTools.DebugError(complexDebugInformation.AddTempCustomText("value01 isn't bigger or equal than value02"));
#endif
                    }

                    break;
                case ENumericConditional.Equal:
                    //Check if value01 is equal than value02
                    if (value01 != value02)
                    {
                        isCorrect = false;
#if UNITY_EDITOR
                        DebugTools.DebugError(complexDebugInformation.AddTempCustomText("value01 isn't equal than value02"));
#endif
                    }

                    break;

                case ENumericConditional.Smaller:
                    //Check if value01 is smaller than value02
                    if (value01 >= value02)
                    {
                        isCorrect = false;
#if UNITY_EDITOR
                        DebugTools.DebugError(complexDebugInformation.AddTempCustomText("value01 isn't smaller than value02"));
#endif
                    }

                    break;

                case ENumericConditional.SmallerOrEqual:
                    //Check if value01 is smaller or equal than value02
                    if (value01 > value02)
                    {
                        isCorrect = false;
#if UNITY_EDITOR
                        DebugTools.DebugError(complexDebugInformation.AddTempCustomText("value01 isn't smaller or equal than value02"));
#endif
                    }

                    break;
            }

            return isCorrect;
        }

        //Method, check if given number comparative is correct
        //Comparatives are made from value01 to value02
        public static bool IsComparativeCorrect(float value01, float value02, ENumericConditional comparativeType)
        {
            //return value
            bool isCorrect = true;

            //Check numeric conditional cases
            switch (comparativeType)
            {
                case ENumericConditional.Bigger:
                    //Check if value01 is bigger than value02
                    isCorrect = value01 > value02;
                    break;

                case ENumericConditional.BiggerOrEqual:
                    //Check if value01 is bigger or equal than value02
                    isCorrect = value01 >= value02;
                    break;

                case ENumericConditional.Equal:
                    //Check if value01 is equal than value02
                    isCorrect = value01 == value02;
                    break;

                case ENumericConditional.Smaller:
                    //Check if value01 is smaller than value02
                    isCorrect = value01 < value02;
                    break;

                case ENumericConditional.SmallerOrEqual:
                    //Check if value01 is smaller or equal than value02
                    isCorrect = value01 <= value02;
                    break;
            }

            return isCorrect;
        }
    }
}