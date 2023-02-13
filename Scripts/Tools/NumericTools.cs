using System.Diagnostics;
using MECS.Collections;
using MECS.Conditionals;
using MECS.Patrons.Commands;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Tools used on operations related with numbers
    public static class NumericTools
    {
        //Method, check if given number comparative is correct
        //Comparatives are made from value01 to value02
        public static bool IsComparativeCorrect(float value01, float value02, ENumericConditional comparativeType,
        string debugMessage)
        {
            //Check if given parameters are correct
            bool areParametersCorrect = CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { value01, value02 },
            debugMessage + " given parameters aren't safe"),

            //Return value
            isCorrect = areParametersCorrect;

            //Avoid parameters errors
            if (areParametersCorrect)
            {
                //Notification to debug manager
                NotificationCommand<DebugArgs> debugNotificationCommand = new(null,
                new DebugArgs(null, UnityEngine.LogType.Error, null));

                //Check numeric conditional cases
                switch (comparativeType)
                {
                    case ENumericConditional.Bigger:
                        //Check if value01 is bigger than value02
                        if (value01 <= value02)
                        {
                            isCorrect = false;

                            //Set notification values
                            debugNotificationCommand.args.debugMessage = debugMessage + " value01 isn't bigger than value02";
                            debugNotificationCommand.args.stackTrace = new StackTrace(true);

                            //Execute command
                            debugNotificationCommand.Execute();
                        }
                        break;

                    case ENumericConditional.BiggerOrEqual:
                        //Check if value01 is bigger or equal than value02
                        if (value01 < value02)
                        {
                            isCorrect = false;

                            //Set notification values
                            debugNotificationCommand.args.debugMessage = debugMessage + " value01 isn't bigger or equal than value02";
                            debugNotificationCommand.args.stackTrace = new StackTrace(true);

                            //Execute command
                            debugNotificationCommand.Execute();
                        }

                        break;
                    case ENumericConditional.Equal:
                        //Check if value01 is equal than value02
                        if (value01 != value02)
                        {
                            isCorrect = false;

                            //Set notification values
                            debugNotificationCommand.args.debugMessage = debugMessage + " value01 isn't equal than value02";
                            debugNotificationCommand.args.stackTrace = new StackTrace(true);

                            //Execute command
                            debugNotificationCommand.Execute();
                        }

                        break;

                    case ENumericConditional.Smaller:
                        //Check if value01 is smaller than value02
                        if (value01 >= value02)
                        {
                            isCorrect = false;

                            //Set notification values
                            debugNotificationCommand.args.debugMessage = debugMessage + " value01 isn't smaller than value02";
                            debugNotificationCommand.args.stackTrace = new StackTrace(true);

                            //Execute command
                            debugNotificationCommand.Execute();
                        }

                        break;

                    case ENumericConditional.SmallerOrEqual:
                        //Check if value01 is smaller or equal than value02
                        if (value01 > value02)
                        {
                            isCorrect = false;

                            //Set notification values
                            debugNotificationCommand.args.debugMessage = debugMessage + " value01 isn't smaller or equal than value02";
                            debugNotificationCommand.args.stackTrace = new StackTrace(true);

                            //Execute command
                            debugNotificationCommand.Execute();
                        }

                        break;
                }
            }

            return isCorrect;
        }

        //Method, check if given number comparative is correct
        //Comparatives are made from value01 to value02
        public static bool IsComparativeCorrect(float value01, float value02, ENumericConditional comparativeType)
        {
            //Check parameters
            bool areParametersCorrect = CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { value01, value02 },
            " given parameters aren't safe"),

            //Return value
            isCorrect = areParametersCorrect;

            //Avoid parameters errors
            if (areParametersCorrect)
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