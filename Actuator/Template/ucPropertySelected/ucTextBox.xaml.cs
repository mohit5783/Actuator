using Actuator.Model.Template.Control;
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

namespace Actuator.Template.ucPropertySelected
{
    /// <summary>
    /// Interaction logic for ucTextBox.xaml
    /// </summary>
    public partial class ucTextBox : UserControl
    {
        clsTextBox ctb;
        public ucTextBox(clsTextBox ctb)
        {
            InitializeComponent();
            this.ctb = ctb;
            updateTb();

            DataContext = this;
        }

        private void updateTb()
        {
            tbContent.Text = ctb.propContent;
            tbWidth.Text = ctb.propWidth.ToString();
            tbHeight.Text = ctb.propHeight.ToString();
            tbMargin.Text = ctb.propMargin.ToString();
            cbAlignment.Text = ctb.propAlignment;
        }

        private void tbWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ctb["propWidth"] = double.Parse(tbWidth.Text);
            }
            catch { }
        }

        private void tbContent_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ctb["propContent"] = tbContent.Text;
            }
            catch { }
        }

        private void tbMargin_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ctb["propMargin"] = double.Parse(tbMargin.Text);
            }
            catch { }
        }

        private void cbAlignment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem typeItem = (ComboBoxItem)cbAlignment.SelectedItem;
                string value = typeItem.Content.ToString();
                ctb["propAlignment"] = value;
            }
            catch { }
        }

        private void tbHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ctb["propHeight"] = double.Parse(tbHeight.Text);
            }
            catch { }
        }
    }
}
