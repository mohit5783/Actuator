using Actuator.Model.Template.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
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
    /// Interaction logic for ucTextBlock.xaml
    /// </summary>
    public partial class ucTextBlock : UserControl
    {
        clsTextblock ctb;
        public ObservableCollection<string> FontList { get; set; }
        public string selectedFont { get; set; }
        public ucTextBlock(clsTextblock ctb)
        {
            InitializeComponent();
            FontList = new ObservableCollection<string>();
            this.ctb = ctb;
            populateFontFamilyCombobox();
            updateTb();

            DataContext = this;
        }

        private void populateFontFamilyCombobox()
        {
            using (InstalledFontCollection fontsCollection = new InstalledFontCollection())
            {
                System.Drawing.FontFamily[] fontFamilies = fontsCollection.Families;
                foreach (System.Drawing.FontFamily font in fontFamilies)
                {
                    FontList.Add(font.Name);
                }
            }
        }

        private void updateTb()
        {
            tbContent.Text = ctb.propContent;
            tbHeight.Text = ctb.propFontSize.ToString();
            tbMargin.Text = ctb.propMargin.ToString();
            cbAlignment.Text = ctb.propAlignment;
            selectedFont = ctb.propFontFamily;
            tblockForegroundColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(ctb.propForeground.ToString());
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
                ctb["propFontSize"] = double.Parse(tbHeight.Text);
            }
            catch { }
        }

        private void cbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string value = cbFontFamily.SelectedItem.ToString();
                ctb["propFontFamily"] = value;
            }
            catch { }
        }

        private void tblockForegroundColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            try
            {
                ctb["propForeground"] = new SolidColorBrush(tblockForegroundColorPicker.SelectedColor);
            }
            catch { }
        }
    }
}
