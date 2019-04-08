using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template.Control
{
    public class clsImage : INotifyPropertyChanged
    {
        Guid _Id;
        string _Type = "Image";
        double _Margin = double.NaN;
        double _ImageHeight = 200;
        double _ImageWidth = 150;
        public string _ImageSource;
        public string _ImageLocation;
        string _Alignment = "Center";

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
        public double propImageHeight
        {
            get { return _ImageHeight; }
            set { _ImageHeight = value; }
        }
        public double propImageWidth
        {
            get { return _ImageWidth; }
            set { _ImageWidth = value; }
        }
        public string propImageData
        {
            get { return _ImageSource; }
            set { _ImageSource = value; }
        }
        public string propImageLocation
        {
            get { return _ImageLocation; }
            set { _ImageLocation = value; }
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
