using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectsModel
{
	public class AOMDate : INotifyPropertyChanged
    {
        public string DataType { get; set; } = "DateTime";
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanges(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int AOMDateID
		{
			get;
			set;
		}
		public string ShortText
		{
			get
			{
				return "Configure the Date";
			}
			set
			{
				ShortText = "Configure the Date";
			}
		}

        DateTime dateValue;
		public DateTime DateValue
		{
			get
            {
                return dateValue;
            }
			set
            {
                dateValue = value;
                NotifyPropertyChanges("DateValue");
            }
		}
	}
}
