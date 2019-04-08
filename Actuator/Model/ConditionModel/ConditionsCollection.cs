using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ConditionModel
{
    public class ConditionsCollection : ObservableCollection<Condition>
    {
        public Condition AddCondition(int id, string description, string operandType, string firstOperand, string secondOperand, string op)
        {
            Condition NewCondition = new Condition()
            {
                id= id,
                Description = description,
                OperandType = operandType,
                FirstOperand = firstOperand,
                SecondOperand = secondOperand,
                Operator = op
            };
            base.Add(NewCondition);
            return NewCondition;
        }
    }
}
