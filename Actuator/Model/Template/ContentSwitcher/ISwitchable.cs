﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.ContentSwitcher
{
    public interface ISwitchable
    {
        void UtilizeState(object state);
    }
}
