using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsDatePicker : INotifyPropertyChanged
    {
        Guid _Id;
        string _Type = "DatePicker";
        string _Name = "";
        string _InputType = "Date";

        double _Width = double.NaN;
        double _Height = double.NaN;

        public Guid propId
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string propType
        {
            get { return _Type; }
            set { }
        }
        public string propName
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string propInputType
        {
            get { return _InputType; }
            set { _InputType = value; }
        }
        public double propWidth
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public double propHeight
        {
            get { return _Height; }
            set { _Height = value; }
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
