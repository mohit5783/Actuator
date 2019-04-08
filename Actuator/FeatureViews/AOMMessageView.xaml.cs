using Actuator.Model.ActionableObjectModelCollection;
using Actuator.Model.ActionableObjectsModel;
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

namespace Actuator.FeatureViews
{
    /// <summary>
    /// Interaction logic for AOMMessageView.xaml
    /// </summary>
    public partial class AOMMessageView : UserControl
    {
        public AOMMessageCollection AOMMessages { get; set; }
        public AOMMessageView()
        {
            InitializeComponent();
            AOMMessages = new AOMMessageCollection();
            AOMMessages = AMAActionableObjects.FixedMessageCollection;
            DataContext = this;
        }

        private void btnCreateMessage_Click(object sender, RoutedEventArgs e)
        {
            AOMMessage am = new AOMMessage();
            am.AOMMessageID = AOMMessages.Count + 1;
            am.DataType = "string";
            am.AOMMessageBody = tboxUserMessage.Text;
            AOMMessages.Add(am);
            AMAActionableObjects.FixedMessageCollection = AOMMessages;
            tboxUserMessage.Text = "";
            MessageGrid.Visibility = Visibility.Collapsed;
        }

        private void btnCloseGrid_Click(object sender, RoutedEventArgs e)
        {
            MessageGrid.Visibility = Visibility.Collapsed;
        }

        private void AddMessageBox_Click(object sender, RoutedEventArgs e)
        {
            MessageGrid.Visibility = Visibility.Visible;
        }
    }
}
