using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ConditionalActionModel
{
    public class AllConditionsActions:ObservableCollection<ConditionActionCollection>
    {
        public ConditionActionCollection AddConditionActionCollection(ConditionActionCollection cac)
        {
            base.Add(cac);
            return cac;
        }
    }
}
