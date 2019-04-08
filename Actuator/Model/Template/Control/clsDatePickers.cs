using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsDatePickers : ObservableCollection<clsDatePicker>
    {
        public clsDatePicker addDatePicker(clsDatePicker cTb)
        {
            clsDatePicker ctb = new clsDatePicker()
            {
                propId = cTb.propId,
                propName = cTb.propName,
                propHeight = cTb.propHeight,
                propWidth = cTb.propWidth,
                propInputType = cTb.propInputType
            };
            base.Add(ctb);
            return ctb;
        }
    }
}
