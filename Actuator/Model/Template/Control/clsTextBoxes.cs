using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsTextBoxes : ObservableCollection<clsTextBox>
    {
        public clsTextBox addTextBox(clsTextBox cTb)
        {
            clsTextBox ctb = new clsTextBox()
            {
                propId = cTb.propId,
                propName = cTb.propName,
                propContent = cTb.propContent,
                propHeight = cTb.propHeight,
                propMargin = cTb.propMargin,
                propWidth = cTb.propWidth,
                propAlignment = cTb.propAlignment,
                propInputDataType = cTb.propInputDataType
            };
            base.Add(ctb);
            return ctb;
        }
    }
}
