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
    /// Interaction logic for ucButton.xaml
    /// </summary>
    public partial class ucButton : UserControl
    {
        clsButton cbtn;
        public ObservableCollection<string> FontList { get; set; }
        public string selectedFont { get; set; }

        public ucButton(clsButton cbtn)
        {
            InitializeComponent();
            FontList = new ObservableCollection<string>();
            this.cbtn = cbtn;
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
            tbContent.Text = cbtn.propContent;
            tbWidth.Text = cbtn.propWidth.ToString();
            tbHeight.Text = cbtn.propHeight.ToString();
            tbMargin.Text = cbtn.propMargin.ToString();
            cbAlignment.Text = cbtn.propAlignment;
            selectedFont = cbtn.propFontFamily;
            tbFontSize.Text = cbtn.propFontSize.ToString();
            tblockBackgroundColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(cbtn.propBtnBackground.ToString());
            tblockForegroundColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(cbtn.propForeground.ToString());
        }

        private void tbWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                cbtn["propWidth"] = double.Parse(tbWidth.Text);
            }
            catch { }
        }

        private void tbContent_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                cbtn["propContent"] = tbContent.Text;
            }
            catch { }
        }

        private void tbMargin_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                cbtn["propMargin"] = double.Parse(tbMargin.Text);
            }
            catch { }
        }

        private void cbAlignment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem typeItem = (ComboBoxItem)cbAlignment.SelectedItem;
                string value = typeItem.Content.ToString();
                cbtn["propAlignment"] = value;
            }
            catch { }
        }

        private void tbHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                cbtn["propHeight"] = double.Parse(tbHeight.Text);
            }
            catch { }
        }
        private void cbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string value = cbFontFamily.SelectedItem.ToString();
                cbtn["propFontFamily"] = value;
            }
            catch { }
        }

        private void tbFontSize_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                cbtn["propFontSize"] = double.Parse(tbFontSize.Text);
            }
            catch { }
        }

        private void tblockForegroundColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            try
            {
                cbtn["propForeground"] = new SolidColorBrush(tblockForegroundColorPicker.SelectedColor);
            }
            catch { }
        }

        private void tblockBackgroundColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            try
            {
                cbtn["propBtnBackground"] = new SolidColorBrush(tblockBackgroundColorPicker.SelectedColor);
            }
            catch { }
        }
    }
}
