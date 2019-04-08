using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;

namespace Actuator.Model.Template
{
    public class clsListViewDesignBackground : INotifyPropertyChanged
    {
        Brush _Background = new SolidColorBrush(Colors.Black);
        public Brush propBackground
        {
            get { return _Background; }
            set { _Background = value; }
        }
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
