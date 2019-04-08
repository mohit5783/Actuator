using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Actuator.FeatureViews
{
	/// <summary>
	/// Interaction logic for SSDsView.xaml
	/// </summary>
	public partial class SSDsView : UserControl
	{
		public FeatureCollection SubFeatures
		{
			get;
			set;
		}
		public Feature SelectedFeature
		{
			get;
			set;
		}
		public SSDsView()
		{
			InitializeComponent();
			SSDsModelView smv = new SSDsModelView();
			SSDsDetailsPane.Children.Add(smv);
			LoadActionMenu();
			DataContext = this;
		}
		public void LoadActionMenu()
		{
			SubFeatures = new FeatureCollection();
			if(File.Exists("ComponentMenu.xml"))
			{
				XElement root = XElement.Load("ComponentMenu.xml");
				root.Elements("Menu").All<XElement>(xe =>
				{
					SubFeatures.AddFeature(xe.Attribute("Name").Value.ToUpper(),
						xe.Attribute("Icon").Value,
						xe.Attribute("ShortText").Value,
						Convert.ToBoolean(xe.Attribute("HasChild").Value),
						xe.Attribute("MenuType").Value);
					return true;
				});
			}
		}
		private void SSDActionMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
