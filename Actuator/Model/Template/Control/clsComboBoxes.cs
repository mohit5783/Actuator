using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsComboBoxes : ObservableCollection<clsComboBox>
    {
        public clsComboBox addComboBox(clsComboBox cBb)
        {
            clsComboBox cbb = new clsComboBox()
            {
                propId = cBb.propId,
                propName = cBb.propName,
                propHeight = cBb.propHeight,
                propMargin = cBb.propMargin,
                propComboboxItem = cBb.propComboboxItem,
                propInputType = cBb.propInputType
            };
            base.Add(cbb);
            return cbb;
        }
    }
}
