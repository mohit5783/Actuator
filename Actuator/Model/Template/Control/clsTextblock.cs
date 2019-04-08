using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Actuator.Model.Template.Control
{
    public class clsTextblock : INotifyPropertyChanged
    {
        Guid _Id;
        string _Type = "TextBlock";
        Brush _Foreground = new SolidColorBrush(Colors.White);
        string _Content = "Default";
        double _Margin = double.NaN;
        string _Alignment = "Center";
        string _FontFamily = "Times New Roman";
        double _FontSize = 12;
        string _Name;
        
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
        public string propContent
        {
            get { return _Content; }
            set { _Content = value; }
        }
        public Brush propForeground
        {
            get { return _Foreground; }
            set { _Foreground = value; }
        }
        public double propMargin
        {
            get { return _Margin; }
            set { _Margin = value; }
        }
        public string propAlignment
        {
            get { return _Alignment; }
            set { _Alignment = value; }
        }
        public string propFontFamily
        {
            get { return _FontFamily; }
            set { _FontFamily = value; }
        }
        public double propFontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
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
