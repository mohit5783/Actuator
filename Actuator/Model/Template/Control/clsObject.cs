using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsObject : INotifyPropertyChanged
    {
        public object controlObject { get; set; }
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set
            {
                this.GetType().GetProperty(propertyName).SetValue(this, value, null);
                NotifyPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
