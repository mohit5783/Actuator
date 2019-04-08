using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class DeviceFunctionCollection : ObservableCollection<DeviceFunction>
	{
		public DeviceFunction AddDeviceFunctions(string id, string name, FunctionParameterCollection fpc, ACTDataType rt)
		{
			DeviceFunction NewDeviceFunction = new DeviceFunction()
			{
				FunctionID = id,
				FunctionName = name,
				allParameters = fpc,
				ReturnType = rt
			};
			base.Add(NewDeviceFunction);
			return NewDeviceFunction;
		}
	}
}
