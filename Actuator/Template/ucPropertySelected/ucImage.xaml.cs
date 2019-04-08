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
    /// Interaction logic for ucImage.xaml
    /// </summary>
    public partial class ucImage : UserControl
    {
        clsImage ci;
        public ucImage(clsImage ci)
        {
            InitializeComponent();
            this.ci = ci;
            updateTb(ci.propImageWidth.ToString(), ci.propImageHeight.ToString(), ci.propMargin.ToString(), ci.propAlignment);

            DataContext = this;
        }

        private void updateTb(string width, string height, string margin, string alignment)
        {
            tBoxImageWidth.Text = width;
            tBoxImageHeight.Text = height;
            tbMargin.Text = margin;
            cbAlignment.Text = alignment;
        }

        private void tbMargin_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ci["propMargin"] = double.Parse(tbMargin.Text);
            }
            catch { }
        }

        private void cbAlignment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem typeItem = (ComboBoxItem)cbAlignment.SelectedItem;
                string value = typeItem.Content.ToString();
                ci["propAlignment"] = value;
            }
            catch { }
        }

        private void tBoxImageWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ci["propImageWidth"] = double.Parse(tBoxImageWidth.Text);
            }
            catch { }
        }

        private void tBoxImageHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ci["propImageHeight"] = double.Parse(tBoxImageHeight.Text);
            }
            catch { }
        }

        private void btnPropImgSource_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*|Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                ci["propImageLocation"] = filename;
            }
        }
    }
}
