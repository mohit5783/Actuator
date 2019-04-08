using Actuator.Model.ActionableObjectModelCollection;
using Actuator.Model.ActionableObjectsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AOMMessageBoxView.xaml
    /// </summary>
    public partial class AOMMessageBoxView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public AOMMessageBox messageBoxClsHolder { get; set; }
        public AOMMessageBoxCollection AOMMessageBoxes { get; } = AMAActionableObjects.FixedMessageBoxCollection;

        private string seconds = "00";

        public string Seconds
        {
            get { return seconds; }
            set { seconds = value; NotifyPropertyChanged("Seconds"); }
        }

        private string minutes = "00";

        public string Minutes
        {
            get { return minutes; }
            set { minutes = value; NotifyPropertyChanged("Minutes"); }
        }

        string content = "Create!!";
        public string btnContent
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                NotifyPropertyChanged("btnContent");
            }
        }

        public AOMMessageBoxView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnCreateMessageBox_Click(object sender, RoutedEventArgs e)
        {
            switch (btnContent)
            {
                case "Create!!":
                    saveAomMessageBox();
                    break;
                case "Save!!":
                    updateAomMessageBox();
                    break;
            }      
        }

        private void btnCloseGrid_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxGrid.Visibility = Visibility.Collapsed;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            gridTimeBased.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            gridTimeBased.Visibility = Visibility.Collapsed;
        }

        private void AddMessageBox_Click(object sender, RoutedEventArgs e)
        {
            btnContent = "Create!!";
            MessageBoxGrid.Visibility = Visibility.Visible;
            clearAllText();
        }

        private void messageBoxItemControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                        OnComponentClick(selectedItem as AOMMessageBox);
                        break;
                    }
                    elem = (Visual)VisualTreeHelper.GetParent(elem);
                }
            }
        }

        private void OnComponentClick(AOMMessageBox aOMMessageBox)
        {
            MessageBoxGrid.Visibility = Visibility.Visible;
            btnContent = "Save!!";
            messageBoxClsHolder = new AOMMessageBox();
            messageBoxClsHolder = aOMMessageBox;

            tboxUserMessage.Text = messageBoxClsHolder.MessageValue;
            cbTimeBased.IsChecked = messageBoxClsHolder.TimeBased;
            string duration = messageBoxClsHolder.MessageDuration.ToString();
            Minutes = duration.Substring(3, 2);
            Seconds = duration.Substring(6, 2);
        }

        private void saveAomMessageBox()
        {
            MessageBoxGrid.Visibility = Visibility.Collapsed;
            string duration = "00:00";
            TimeSpan setDuration;
            bool timeBased = false;
            if (cbTimeBased.IsChecked ?? false)
            {
                duration = Minutes + ":" + Seconds;
                setDuration = TimeSpan.Parse("0:" + duration);
                timeBased = true;
            }
            else
            {
                setDuration = TimeSpan.Parse(duration);
                timeBased = false;
            }

            int GenID = 0;
            if (AMAActionableObjects.FixedMessageBoxCollection.Count > 0)
            {
                GenID = AMAActionableObjects.FixedMessageBoxCollection.Max(x => x.AOMMessageBoxID) + 1;
            }
            AMAActionableObjects.FixedMessageBoxCollection.AddAOMMessageBox(GenID, tboxUserMessage.Text, setDuration, timeBased);
        }

        private void updateAomMessageBox()
        {
            var holder = AMAActionableObjects.FixedMessageBoxCollection.Where(x => x.AOMMessageBoxID == messageBoxClsHolder.AOMMessageBoxID);
            foreach(AOMMessageBox messageBox in holder)
            {
                string duration = "00:00";
                TimeSpan setDuration;
                bool timeBased = false;
                if (cbTimeBased.IsChecked ?? false)
                {
                    duration = Minutes + ":" + Seconds;
                    setDuration = TimeSpan.Parse("0:" + duration);
                    timeBased = true;
                }
                else
                {
                    setDuration = TimeSpan.Parse(duration);
                    timeBased = false;
                }
                messageBox.MessageValue = tboxUserMessage.Text;
                messageBox.MessageDuration = setDuration;
                messageBox.TimeBased = timeBased;

                MessageBoxGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void clearAllText()
        {
            tboxUserMessage.Text = "";
            cbTimeBased.IsChecked = false;
            Minutes = "00";
            Seconds = "00";
        }
    }
}
