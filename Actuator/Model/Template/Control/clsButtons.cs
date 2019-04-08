using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsButtons : ObservableCollection<clsButton>
    {
        public clsButton addButton(clsButton cBtn)
        {
            clsButton cbtn = new clsButton()
            {
                propId = cBtn.propId,
                propForeground = cBtn.propForeground,
                propBtnBackground = cBtn.propBtnBackground,
                propContent = cBtn.propContent,
                propHeight = cBtn.propHeight,
                propMargin = cBtn.propMargin,
                propWidth = cBtn.propWidth,
                propAlignment = cBtn.propAlignment,
                propFontFamily = cBtn.propFontFamily,
                propFontSize = cBtn.propFontSize,
                propButtonAction = cBtn.propButtonAction
            };
            base.Add(cbtn);
            return cbtn;
        }

    }
}
