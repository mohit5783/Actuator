using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ConditionalActionModel
{
    public class ConditionAction
    {
        public int id { get; set; }
        public Object CondAct { get; set; }
        public int ParentID { get; set; }
        public string ObjType { get; set; }
    }
}
