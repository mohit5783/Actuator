using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Actuator.Model.DeviceModel
{
	public class FunctionParameterCollection : ObservableCollection<FunctionParameter>
	{
		public FunctionParameter AddFunctionParameters(string paramname, ACTDataType paramdatatype)
		{
			FunctionParameter NewFunctionParameter = new FunctionParameter()
			{
				FPName= paramname,
				FPDataType = paramdatatype
			};
			base.Add(NewFunctionParameter);
			return NewFunctionParameter;
		}
	}
}
