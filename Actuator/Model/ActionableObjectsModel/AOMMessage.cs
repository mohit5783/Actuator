using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectsModel
{
    public class AOMMessage
    {
        public string DataType { get; set; } = "string";
        public int AOMMessageID { get; set; }
        public string AOMMessageBody { get; set; }
        public string ShortText
        {
            get
            {
                return "Create Messages for FB, Twitter.";
            }
            set
            {
                ShortText = "Create Messages for FB, Twitter.";
            }
        }


    }
}
