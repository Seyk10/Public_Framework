using System;
using MECS.Collections;
using MECS.Patrons.Commands;
using UnityEngine;

namespace MECS.Conditionals
{
    //* System used to execute all the code logics related to conditionals
    //* T = IConditionalData
    [CreateAssetMenu(fileName = "New_Check_Conditionals_Command", menuName = "MECS/Commands/Conditionals/Check_Conditionals")]
    public class CheckConditionalsCommand : ScriptableObject, ICommandParameter<ConditionalComponent>
    {
        //ICommandParameter, notify when command execution ends
        public event EventHandler<ConditionalComponent> CommandFinishedEvent = null;
        public static event EventHandler<IConditionalData> CheckValuesEvent = null;

        //ICommandParameter, share each data to check it
        public void Execute(ConditionalComponent parameter)
        {
            //Store data arrays
            IConditionalData[] dataArray = parameter.Data;

            //Notify to check given data
            if (CollectionsTools.arrayTools.IsArrayContentSafe(dataArray, " given dataArray isn't safe"))
                foreach (IConditionalData data in dataArray)
                    CheckValuesEvent?.Invoke(parameter, data);

            //Notify when command ends
            CommandFinishedEvent?.Invoke(this, parameter);
        }
    }
}