using Actuator.Model.ActionableObjectsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectModelCollection
{
    public class AOMMessageBoxCollection : ObservableCollection<AOMMessageBox>
    {
        public AOMMessageBox AddAOMMessageBox(int id, string sMessageVal, TimeSpan tsMessageDur, bool bTimeBased)
        {
            AOMMessageBox NewAOMMessageBox = new AOMMessageBox()
            {
                AOMMessageBoxID = id,
                MessageValue = sMessageVal,
                MessageDuration = tsMessageDur,
                TimeBased = bTimeBased
            };
            base.Add(NewAOMMessageBox);
            return NewAOMMessageBox;
        }
    }
}
