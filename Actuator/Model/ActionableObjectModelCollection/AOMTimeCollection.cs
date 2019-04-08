using Actuator.Model.ActionableObjectsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectModelCollection
{
	public class AOMTimeCollection : ObservableCollection<AOMTime>
	{
		public AOMTime AddAOMTime(int id, DateTime tval, List<string> tRepeatVal)
		{
			AOMTime NewAOMTime = new AOMTime()
			{
				AOMTimeID = id,
				TimeValue = tval,
                TimeRepeatValues = tRepeatVal
			};
			base.Add(NewAOMTime);
			return NewAOMTime;
		}
	}
}
