using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsImages : ObservableCollection<clsImage>
    {
        public clsImage addImage(clsImage pCi)
        {
            clsImage cI = new clsImage()
            {
                propId = pCi.propId,
                propMargin = pCi.propMargin,
                propAlignment = pCi.propAlignment,
                propImageHeight = pCi.propImageHeight,
                propImageData = pCi.propImageData,
                propImageLocation = pCi.propImageLocation,
                propImageWidth = pCi.propImageWidth
            };

            base.Add(cI);
            return cI;
        }
    }
}
