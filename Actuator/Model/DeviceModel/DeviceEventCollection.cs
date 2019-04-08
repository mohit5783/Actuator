using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class DeviceEventCollection : ObservableCollection<DeviceEvent>
	{
		public DeviceEvent AddDeviceEvent(string id, string name)
		{
			DeviceEvent NewDeviceEvent = new DeviceEvent()
			{
				EventID = id,
				EventName = name
			};
			base.Add(NewDeviceEvent);
			return NewDeviceEvent;
		}
	}
}
