using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectsModel
{
	public class AOMTime : INotifyPropertyChanged
	{
        public string DataType { get; set; } = "DateTime";
        public int AOMTimeID
		{
			get;
			set;
		}
		public string ShortText
		{
			get
			{
				return "Configure your time";
			}
			set
			{
				ShortText = "Configure your time";
			}
		}

        DateTime timeValue;
		public DateTime TimeValue
		{
			get
            {
                return timeValue;
            }
			set
            {
                timeValue = value;
                NotifyPropertyChanges("TimeValue");
            }
		}
        public List<string> TimeRepeatValues
        {
            get;
            set;
        }
        public enum TimeRepeater
        {
            None,
            Everyday,
            Weekday,
            Weekend,
            Custom
        }
        public enum Everyday
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        public enum Weekday
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday
        }
        public enum Weekend
        {
            Saturday,
            Sunday
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
