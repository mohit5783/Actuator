using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.DeviceModel
{
	public class DeviceProperty
	{
		public string PropertyID
		{
			get;
			set;
		}
		public string PropertyName
		{
			get;
			set;
		}
		public ACTDataType PropertyType
		{
			get;
			set;
		}
		public bool ReadOnly
		{
			get;
			set;
		}
	}
}
