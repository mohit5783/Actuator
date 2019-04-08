using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Actuator.Model.Template.Control
{
    public class clsComboBox : INotifyPropertyChanged
    {
        Guid _Id;
        string _Name = "";
        string _Type = "ComboBox";
        string _InputType = "string";

        double _Margin = double.NaN;
        double _Height = double.NaN;

        List<string> _ComboboxItem;

        public Guid propId
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string propName
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string propType
        {
            get { return _Type; }
            set { }
        }

        public string propInputType
        {
            get { return _InputType; }
            set { _InputType = value; }
        }
        public double propMargin
        {
            get { return _Margin; }
            set { _Margin = value; }
        }
        public double propHeight
        {
            get { return _Height; }
            set { _Height = value; }
        }
        public List<string> propComboboxItem
        {
            get { return _ComboboxItem; }
            set { _ComboboxItem = value; }
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
