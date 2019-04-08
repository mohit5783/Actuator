using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsObjects : ObservableCollection<clsObject>
    {
        public clsObject addObject(clsObject cO)
        {
            clsObject co = new clsObject()
            {
                controlObject = cO.controlObject
            };
            base.Add(co);
            return co;
        }
    }
}
