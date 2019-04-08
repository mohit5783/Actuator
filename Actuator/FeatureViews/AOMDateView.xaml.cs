using Actuator.Model.ActionableObjectModelCollection;
using Actuator.Model.ActionableObjectsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// Interaction logic for AOMDateView.xaml
	/// </summary>
	public partial class AOMDateView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public AOMDateCollection AOMDates { get; } = AMAActionableObjects.FixedDateCollection;
        public AOMDate dateClsHolder { get; set; }

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

        public AOMDateView()
		{
			InitializeComponent();
			DataContext = this;
        }
		private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            btnContent = "Create!!";
            DateGrid.Visibility = System.Windows.Visibility.Visible;
            clearAllText();
        }
		private void DateChoosen_Click(object sender, RoutedEventArgs e)
		{
            switch (btnContent)
            {
                case "Create!!":
                    saveAomDate();
                    break;
                case "Save!!":
                    updateAomDate();
                    break;
            }
		}

        private void btnCloseGrid_Click(object sender, RoutedEventArgs e)
        {
            DateGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DateItemControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                        OnComponentClick(selectedItem as AOMDate);
                        break;
                    }
                    elem = (Visual)VisualTreeHelper.GetParent(elem);
                }
            }
        }
        private void OnComponentClick(AOMDate aOMDateView)
        {
            DateGrid.Visibility = Visibility.Visible;
            btnContent = "Save!!";
            dateClsHolder = new AOMDate();
            dateClsHolder = aOMDateView;

            UserSelDate.SelectedDate = dateClsHolder.DateValue;
        }
        
        private void saveAomDate()
        {
            DateGrid.Visibility = System.Windows.Visibility.Collapsed;
            int GenID = 0;
            if (AMAActionableObjects.FixedDateCollection.Count > 0)
            {
                GenID = AMAActionableObjects.FixedDateCollection.Max(x => x.AOMDateID) + 1;
            }
            AMAActionableObjects.FixedDateCollection.AddAOMDate(GenID, UserSelDate.SelectedDate.Value);
        }

        private void updateAomDate()
        {
            var holder = AMAActionableObjects.FixedDateCollection.Where(x => x.AOMDateID == dateClsHolder.AOMDateID);
            foreach(AOMDate date in holder)
            {
                date.DateValue = UserSelDate.SelectedDate.Value;
                DateGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void clearAllText()
        {
            UserSelDate.SelectedDate = DateTime.Now;
        }
    }
}
