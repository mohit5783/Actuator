using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Actuator.Model.Template.Control
{
    public class clsControls : ObservableCollection<clsControl>
    {
        public clsControl addControl(Guid pId, string pType)
        {
            clsControl cC = new clsControl()
            {
                id = pId,
                type = pType
            };
            
            base.Add(cC);
            return cC;
        }
    }
}
