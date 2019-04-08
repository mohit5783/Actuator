using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Actuator.Model.ActionModel
{
	public class ActionableObjectList
	{
		public List<Object> AllActionableObjectList
		{
			get;
			set;
		}
		public List<Type> AllClasses
		{
			get;
			set;
		}

		public ActionableObjectList()
		{
			AllActionableObjectList = new List<object>();
			AllClasses = new List<Type>();
			AllClasses = Assembly.GetExecutingAssembly().GetTypes().Where(t => String.Equals(t.Namespace, "Actuator.Model.ActionableObjectsModel", StringComparison.Ordinal)).ToList();
			foreach(Type t1 in AllClasses)
			{
				if(t1.Name.StartsWith("AOM"))
				{
					var instance = Activator.CreateInstance(t1);
					AllActionableObjectList.Add(instance);
				}
			}
		}
	}
}
