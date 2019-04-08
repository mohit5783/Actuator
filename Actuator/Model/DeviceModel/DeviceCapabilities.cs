using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class DeviceCapabilities
	{
		public DeviceEventCollection Events
		{
			get;
			set;
		}
		public DeviceFunctionCollection Functions
		{
			get;
			set;
		}
		public DevicePropertyCollection Properties
		{
			get;
			set;
		}
	}
}
