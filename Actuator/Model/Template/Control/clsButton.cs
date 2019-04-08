using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Actuator.Model.Template.Control
{
    public class clsButton : INotifyPropertyChanged
    {
        Guid _Id;
        string _Type = "Button";

        Brush _Foreground = new SolidColorBrush(Colors.Black);
        Brush _Background = new SolidColorBrush(Colors.White);
        string _Content = "Default";
        double _Height = double.NaN;
        double _Margin = double.NaN;
        double _Width = double.NaN;
        string _Alignment = "Center";
        string _FontFamily = "Times New Roman";
        double _FontSize = 12;

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
        public clsButtonAction propButtonAction { get; set; }

        public Brush propForeground
        {
            get { return _Foreground; }
            set { _Foreground = value; }
        }
        public Brush propBtnBackground
        {
            get { return _Background; }
            set { _Background = value; }
        }
        public string propContent
        {
            get { return _Content; }
            set { _Content = value; }
        }
        public double propHeight
        {
            get { return _Height; }
            set { _Height = value; }
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
