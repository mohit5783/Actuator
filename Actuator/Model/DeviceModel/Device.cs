using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class Device
	{
		public string DeviceID
		{
			get;
			set;
		}
		public string DeviceName
		{
			get;
			set;
		}

		public DeviceCapabilities Capabilities
		{
			get;
			set;
		}

	}
}
