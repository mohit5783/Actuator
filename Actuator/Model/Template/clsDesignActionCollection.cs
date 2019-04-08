using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template
{
    public class clsDesignActionCollection : ObservableCollection<clsDesignAction>
    {
        public clsDesignAction AddDesignAction(string name, string IconPath, string ShortTextofMenu, bool hc, string mt)
        {
            clsDesignAction NewDesignAction = new clsDesignAction()
            {
                Name = name,
                Icon = IconPath,
                ShortText = ShortTextofMenu,
                HasChild = hc,
                MenuType = mt
            };
            base.Add(NewDesignAction);
            return NewDesignAction;
        }
    }
}
