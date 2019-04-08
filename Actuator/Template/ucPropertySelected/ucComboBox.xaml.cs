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
    /// Interaction logic for ucComboBox.xaml
    /// </summary>
    public partial class ucComboBox : UserControl
    {
        clsComboBox ccb;
        public ucComboBox(clsComboBox ccb)
        {
            InitializeComponent();
            this.ccb = ccb;
            updateTb();

            DataContext = this;
        }

        private void updateTb()
        {
            tbHeight.Text = ccb.propHeight.ToString();
            tbMargin.Text = ccb.propMargin.ToString();
        }


        private void tbMargin_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ccb["propMargin"] = double.Parse(tbMargin.Text);
            }
            catch { }
        }

        private void tbHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ccb["propHeight"] = double.Parse(tbHeight.Text);
            }
            catch { }
        }
    }
}
