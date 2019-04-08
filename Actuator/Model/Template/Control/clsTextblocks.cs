using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsTextblocks : ObservableCollection<clsTextblock>
    {
        public clsTextblock addTextblock(clsTextblock ctb)
        {
            clsTextblock cTB = new clsTextblock()
            {
                propId = ctb.propId,
                propContent = ctb.propContent,
                propAlignment = ctb.propAlignment,
                propFontFamily = ctb.propFontFamily,
                propFontSize = ctb.propFontSize,
                propForeground = ctb.propForeground,
                propMargin = ctb.propMargin
            };
            base.Add(cTB);
            return cTB;
        }
    }
}
