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

namespace Actuator.Template.ucPropertySelected
{
    /// <summary>
    /// Interaction logic for ucMain.xaml
    /// </summary>
    public partial class ucMain : UserControl
    {
        clsListViewDesignBackground cLvdbg;
        public ucMain(clsListViewDesignBackground cLvdbg)
        {
            InitializeComponent();
            this.cLvdbg = cLvdbg;
            updateTb();

            DataContext = this;
        }

        private void updateTb()
        {
            listViewBackgroundColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(cLvdbg.propBackground.ToString());
        }

        private void listViewBackgroundColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            try
            {
                cLvdbg["propBackground"] = new SolidColorBrush(listViewBackgroundColorPicker.SelectedColor);
            }
            catch { }
        }
    }
}
