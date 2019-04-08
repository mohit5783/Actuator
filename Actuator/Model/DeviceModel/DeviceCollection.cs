using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class DeviceCollection : ObservableCollection<Device>
	{
		public Device AddDevice(string id, string name, DeviceCapabilities dc)
		{
			Device NewDevice = new Device()
			{
				DeviceID = id,
				DeviceName= name,
				Capabilities = dc
			};
			base.Add(NewDevice);
			return NewDevice;
		}
	}
}
