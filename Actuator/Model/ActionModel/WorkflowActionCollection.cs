using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionModel
{
    public class WorkflowActionCollection : ObservableCollection<WorkflowAction>
    {
        public WorkflowAction AddWorkflowActions(int id, string actnm, string objNm, string varnm, string varval)
        {
            WorkflowAction Newwa = new WorkflowAction()
            {
                id = id,
                ActionName = actnm,
                ObjectName = objNm,
                VariableName = varnm,
                VariableValue = varval
            };
            base.Add(Newwa);
            return Newwa;
        }
    }
}
