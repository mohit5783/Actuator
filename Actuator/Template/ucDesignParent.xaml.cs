using Actuator.Model.Template;
using System;
using System.Collections.Generic;
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

namespace Actuator.Template
{
    /// <summary>
    /// Interaction logic for ucDesignParent.xaml
    /// </summary>
    public partial class ucDesignParent : UserControl
    {
        public ucDesignParent()
        {
            InitializeComponent();
            DataContext = AMAActionableObjects.FixedDesignCollection;
        }

        private void AddDesign_Click(object sender, RoutedEventArgs e)
        {
            DesignGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnCloseGrid_Click(object sender, RoutedEventArgs e)
        {
            DesignGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CreateDesign_Click(object sender, RoutedEventArgs e)
        {
            DesignGrid.Visibility = System.Windows.Visibility.Collapsed;
            Guid GenID = Guid.NewGuid();
            AMAActionableObjects.FixedDesignCollection.AddDesign(GenID, tboxDesignDescription.Text);

            clsXmlInteraction cxi = new clsXmlInteraction(GenID);
            cxi.createDesignTag(tboxDesignDescription.Text);
        }

        private void designItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemsControl listbox = sender as ItemsControl;
            if (listbox != null)
            {
                Visual elem = (Visual)listbox.InputHitTest(e.GetPosition(listbox));
                while (elem != listbox)
                {
                    if (elem is ContentPresenter)
                    {
                        object selectedItem = ((ContentPresenter)elem).Content;
                        OnComponentClick(selectedItem as clsDesign);
                        break;
                    }
                    elem = (Visual)VisualTreeHelper.GetParent(elem);
                }
            }
        }

        private void OnComponentClick(clsDesign designSelected)
        {
            ucDesign ud = new ucDesign(designSelected);
            ud.ShowDialog();
        }
    }
}
