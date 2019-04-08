using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class DevicePropertyCollection : ObservableCollection<DeviceProperty>
	{
		public DeviceProperty AddDeviceProperties(string id, string name, ACTDataType dt, bool ro)
		{
			DeviceProperty NewDeviceProperty = new DeviceProperty()
			{
				PropertyID = id,
				PropertyName = name,
				PropertyType = dt,
				ReadOnly = ro
			};
			base.Add(NewDeviceProperty);
			return NewDeviceProperty;
		}
	}
}
