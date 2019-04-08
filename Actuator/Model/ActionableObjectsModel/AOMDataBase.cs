using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectsModel
{
	public class AOMDataBase
	{
        public string DataType { get; set; } = "string";
        public string ShortText
		{
			get
			{
				return "Configure Your Database";
			}
			set
			{
				ShortText = "Configure Your Database";
			}
		}
		public int DBID
		{
			get;
			set;
		}
	}
}
