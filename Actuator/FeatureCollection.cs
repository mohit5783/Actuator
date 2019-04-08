using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator
{
	public class FeatureCollection : ObservableCollection<Feature>
	{
		public Feature AddFeature(string name, string IconPath, string ShortTextofMenu, bool hc, string mt)
		{
			Feature NewFeature = new Feature()
			{
				Name = name,
				Icon = IconPath,
				ShortText = ShortTextofMenu,
				HasChild = hc,
				MenuType = mt
			};
			base.Add(NewFeature);
			return NewFeature;
		}
	}
}
