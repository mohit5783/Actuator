using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectsModel
{
	//Message Box either time based that means it goes automatically after certain seconds
	//or On click of button okay
	public class AOMMessageBox: INotifyPropertyChanged
	{
        public string DataType { get; set; } = "string";
        public int AOMMessageBoxID
        {
            get;
            set;
        }
        public string ShortText
		{
			get
			{
				return "Create your Message box.";
			}
			set
			{
				ShortText = "Create your Message box.";
			}
		}

        string messageValue;
        public string MessageValue
        {
            get
            {
                return messageValue;
            }
            set
            {
                messageValue = value;
                NotifyPropertyChanges("MessageValue");
            }
        }
        public TimeSpan MessageDuration
        {
            get;
            set;
        } = TimeSpan.FromSeconds(0);
        public bool TimeBased
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanges(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
