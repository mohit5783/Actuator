using Actuator.Model.ActionableObjectsModel;
using Actuator.Model.ActionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Actuator.FeatureViews
{
	/// <summary>
	/// Interaction logic for ActionableObjects.xaml
	/// </summary>
	public partial class ActionableObjects : UserControl
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
		public ActionableObjects()
		{
			InitializeComponent();
			SelectedSubFeature = new Feature();
			LoadSubFeatures();
			DataContext = this;
		}
		public string[] SplitCamelCase(string source)
		{
			return Regex.Split(source, @"(?<!^)(?=[A-Z])");
		}

		private void LoadSubFeatures()
		{
			ActionableObjectList aol = new ActionableObjectList();
			List<Type> ActionableObjectNames = new List<Type>();

			ActionableObjectNames = aol.AllClasses.Where(x => x.Name.StartsWith("AOM")).ToList();

			SubFeatures = new FeatureCollection();
			int indx = 0;
			foreach(Type ty in ActionableObjectNames)
			{
				string ShTxt = ty.GetProperty("ShortText").GetValue(aol.AllActionableObjectList[indx]).ToString();
				SubFeatures.AddFeature(String.Join(" ", SplitCamelCase(ty.Name.Replace("AOM", ""))), "../img/MenuImages/actionableobjects.png", ShTxt, false, "Menu");
				indx++;
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
            if (SelectedSubFeature.Name.ToUpper() == "DATE")
			{
				AOMDateView adv = new AOMDateView();
				WorkflowDetailsPane.Children.Add(adv);
			}
            if (SelectedSubFeature.Name.ToUpper() == "TIME")
            {
                AOMTimeView atv = new AOMTimeView();
                WorkflowDetailsPane.Children.Add(atv);
            }
            if (SelectedSubFeature.Name.ToUpper() == "MESSAGE")
            {
                AOMMessageView amv = new AOMMessageView();
                WorkflowDetailsPane.Children.Add(amv);
            }
            if (SelectedSubFeature.Name.ToUpper() == "MESSAGE BOX")
            {
                AOMMessageBoxView ambv = new AOMMessageBoxView();
                WorkflowDetailsPane.Children.Add(ambv);
            }
            if (SelectedSubFeature.Name.ToUpper() == "EMAIL")
            {
                AOMEmailView aev = new AOMEmailView();
                WorkflowDetailsPane.Children.Add(aev);
            }
        }
	}
}
