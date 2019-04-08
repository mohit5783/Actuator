using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ConditionModel
{
    public class Condition
    {
        public int id { get; set; }
        public string Description { get; set; }
        public string OperandType { get; set; }
        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operator { get; set; }
    }
}
