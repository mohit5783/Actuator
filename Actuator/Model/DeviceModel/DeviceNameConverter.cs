using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Actuator.Model.DeviceModel
{
    public class DeviceNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string DevName = value as string;
            if (DevName.Length >= 15)
                DevName = DevName.Substring(0, 5) + "..." + DevName.Substring(DevName.Length - 6, 6);
            return DevName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
