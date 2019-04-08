using ComponentManager;
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
	/// Interaction logic for SSDsModelView.xaml
	/// </summary>
	public partial class SSDsModelView : UserControl
	{
		public ClsComponent SelectedSSD
		{
			get;
			set;
		}
		public ClsComponentManager CompMgr
		{
			get;
			set;
		}
		public ObservableCollection<ClsComponent> CompColl
		{
			get;
			set;
		}
		public SSDsModelView()
		{
			InitializeComponent();
            ImpersonationHelper.Impersonate("CMSB", "mohit", "Password1", delegate
            {
                CompMgr = new ClsComponentManager();
                CompColl = new ObservableCollection<ClsComponent>();
                LoadSSDsList();
            });
            DataContext = this;
		}
		private void LoadSSDsList()
		{
			CompColl = CompMgr.GetAllComponentByType(typeofComponent.SSD_SmartSoftDevice);
		}

		private void SSDs_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			ItemsControl listbox = sender as ItemsControl;
			if(listbox != null)
			{
				Visual elem = (Visual) listbox.InputHitTest(e.GetPosition(listbox));
				while(elem != listbox)
				{
					if(elem is ContentPresenter)
					{
						object selectedItem = ((ContentPresenter) elem).Content;
						OnComponentClick(selectedItem as ClsComponent);
						break;
					}
					elem = (Visual) VisualTreeHelper.GetParent(elem);
				}
			}
		}

		private void OnComponentClick(ClsComponent clsComponent)
		{
			//Load Sub Feature Menu 
			// It Should be the list of Capabilties sent by SSDs
			SelectedSSD = clsComponent;
		}
	}
}
