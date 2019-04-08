using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template
{
    public class clsDesignCollection : ObservableCollection<clsDesign>
    {
        public clsDesign AddDesign(Guid id, string description)
        {
            clsDesign NewDesign = new clsDesign()
            {
                designId = id,
                description = description
            };
            base.Add(NewDesign);
            return NewDesign;
        }
    }
}
