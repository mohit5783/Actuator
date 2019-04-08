using System;
using System.Collections.Generic;
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
	/// Interaction logic for SettingView.xaml
	/// </summary>
	public partial class SettingView : UserControl
	{
		public Feature SelectedSubFeature
		{
			get;
			set;
		}
		public FeatureCollection SubFeatures
		{
			get;
			set;
		}
		public SettingView()
		{
			InitializeComponent();
			SelectedSubFeature = new Feature();
			SubFeatures = new FeatureCollection();
			LoadSettingsFeature();
			DataContext = this;
		}

		private void LoadSettingsFeature()
		{
			SubFeatures = new FeatureCollection();
			if(File.Exists("SettingsMenu.xml"))
			{
				XElement root = XElement.Load("SettingsMenu.xml");
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

		private void SettingActionMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedSubFeature = (SettingActionMenu.SelectedItem as Feature);
			LoadRespectiveFeature(SelectedSubFeature);
		}

		private void LoadRespectiveFeature(Feature SelectedSubFeature)
		{
			if(SelectedSubFeature.Name.ToUpper() == "Email Settings".ToUpper())
			{
				EmailSettings es = new EmailSettings();
				SettingDetailsPane.Children.Add(es);
			}
		}
	}
}
