using Actuator.Model.ActionableObjectsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectModelCollection
{
	public class AOMDateCollection : ObservableCollection<AOMDate>
	{
		public AOMDate AddAOMDate(int id, DateTime dtval)
		{
			AOMDate NewAOMDate = new AOMDate()
			{
				AOMDateID = id,
				DateValue = dtval
			};
			base.Add(NewAOMDate);
			return NewAOMDate;
		}
	}
}
