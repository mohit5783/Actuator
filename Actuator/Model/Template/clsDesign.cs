using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.Template
{
    public class clsDesign : INotifyPropertyChanged
    {
        public Guid designId { get; set; }

        string _description;
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanges("description");
            }
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
