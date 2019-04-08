using Actuator.Model.ActionableObjectsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectModelCollection
{
    public class AOMMessageCollection:ObservableCollection<AOMMessage>
    {
        public AOMMessage AddAOMMessage(int id, string sMessageVal)
        {
            AOMMessage NewAOMMessage = new AOMMessage()
            {
                AOMMessageID = id,
                AOMMessageBody=sMessageVal,
            };
            base.Add(NewAOMMessage);
            return NewAOMMessage;
        }
    }
}
