using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionModel
{
    public class WorkflowAction
    {
        public int id { get; set; }
        public string ActionName { get; set; }
        public string ObjectName { get; set; }
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
    }
}
