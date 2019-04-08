using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsTextBox : INotifyPropertyChanged
    {
        Guid _Id;
        string _Type = "TextBox";
        string _Name = "";
        string _InputType = "string";

        string _Content = "Hint For User";
        double _Margin = double.NaN;
        double _Width = double.NaN;
        string _Alignment = "Center";
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


        public string propInputDataType
        {
            get { return _InputType; }
            set { _InputType = value; }
        }
        public string propContent
        {
            get { return _Content; }
            set { _Content = value; }
        }
        public double propMargin
        {
            get { return _Margin; }
            set { _Margin = value; }
        }
        public double propWidth
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public string propAlignment
        {
            get { return _Alignment; }
            set { _Alignment = value; }
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
