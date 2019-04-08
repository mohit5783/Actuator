using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class DeviceFunction
	{
		public string FunctionID
		{
			get;
			set;
		}
		public string FunctionName
		{
			get;
			set;
		}
		public FunctionParameterCollection allParameters
		{
			get;
			set;
		}
		public ACTDataType ReturnType
		{
			get;
			set;
		}
	}
}
