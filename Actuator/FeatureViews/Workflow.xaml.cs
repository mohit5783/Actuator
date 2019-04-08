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
	/// Interaction logic for Workflow.xaml
	/// </summary>
	public partial class Workflow : UserControl
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
		public Workflow()
		{
			InitializeComponent();
			SelectedSubFeature = new Feature();
			LoadSubFeatures();
			DataContext = this;
		}

		private void LoadSubFeatures()
		{
			SubFeatures = new FeatureCollection();
			if(File.Exists("WorkflowMenu.xml"))
			{
				XElement root = XElement.Load("WorkflowMenu.xml");
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

		private void ActionMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedSubFeature = (ActionMenu.SelectedItem as Feature);
			LoadRespectiveFeature(SelectedSubFeature);
		}

		private void LoadRespectiveFeature(Feature SelectedSubFeature)
		{
            WorkflowDetailsPane.Children.Clear();
			if(SelectedSubFeature.Name.ToUpper() == "CONDITIONS".ToUpper())
			{
				ConditionView cn = new ConditionView();
				WorkflowDetailsPane.Children.Add(cn);
			}
            else if (SelectedSubFeature.Name.ToUpper() == "ACTIONS".ToUpper())
            {
                ActionView av = new ActionView();
                WorkflowDetailsPane.Children.Add(av);
            }
            else if (SelectedSubFeature.Name.ToUpper() == "conditional actions".ToUpper())
            {
                ConditionalActionView cav = new ConditionalActionView();
                WorkflowDetailsPane.Children.Add(cav);
            }
            else if (SelectedSubFeature.Name.ToUpper() == "Loops".ToUpper())
            {
                LoopsView lv = new LoopsView();
                WorkflowDetailsPane.Children.Add(lv);
            }
        }
	}
}
