using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ConditionalActionModel
{
    public class ConditionActionCollection : ObservableCollection<ConditionAction>
    {
        public int ConditionActionID { get; set; }
        public void AddConditionAction(List<ConditionAction> lca)
        {
            foreach (ConditionAction ca in lca)
                base.Add(ca);
        }
    }
}
