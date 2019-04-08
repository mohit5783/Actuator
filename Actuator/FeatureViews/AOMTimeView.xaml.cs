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
	/// Interaction logic for AOMTimeView.xaml
	/// </summary>
	public partial class AOMTimeView : UserControl, INotifyPropertyChanged
	{
        public List<string> timeRepeatCollection { get; set; }

        public List<CheckBox> checkBoxList { get; set; }

        public AOMTimeCollection AOMTimes { get; } = AMAActionableObjects.FixedTimeCollection;

        public AOMTime timeClsHolder { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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

        public AOMTimeView()
		{
			InitializeComponent();
            checkBoxList = new List<CheckBox>();
            getAllCb();
            cboxRepeater.ItemsSource = Enum.GetValues(typeof(AOMTime.TimeRepeater));
            DataContext = this;
        }

        private void getAllCb()
        {
            checkBoxList.Add(cbMon);
            checkBoxList.Add(cbTue);
            checkBoxList.Add(cbWed);
            checkBoxList.Add(cbThurs);
            checkBoxList.Add(cbFri);
            checkBoxList.Add(cbSat);
            checkBoxList.Add(cbSun);
        }

        private void AddTime_Click(object sender, RoutedEventArgs e)
        {
            btnContent = "Create!!";
            TimeGrid.Visibility = Visibility.Visible;
            clearAllText();
        }

		private void TimeChoosen_Click(object sender, RoutedEventArgs e)
		{
            switch (btnContent)
            {
                case "Create!!":
                    saveAomTime();
                    break;
                case "Save!!":
                    updateAomTime();
                    break;
            }
        }

        private void cboxRepeater_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timeRepeatCollection = new List<string>();
            switch (cboxRepeater.SelectedItem.ToString())
            {
                case "Everyday":
                    timeRepeatCollection.Clear();
                    gridCustomTime.Visibility = Visibility.Collapsed;
                    timeRepeatCollection = Enum.GetValues(typeof(AOMTime.Everyday)).Cast<AOMTime.Everyday>().Select(v => v.ToString()).ToList();
                    break;
                case "Weekday":
                    timeRepeatCollection.Clear();
                    gridCustomTime.Visibility = Visibility.Collapsed;
                    timeRepeatCollection = Enum.GetValues(typeof(AOMTime.Weekday)).Cast<AOMTime.Weekday>().Select(v => v.ToString()).ToList();
                    break;
                case "Weekend":
                    timeRepeatCollection.Clear();
                    gridCustomTime.Visibility = Visibility.Collapsed;
                    timeRepeatCollection = Enum.GetValues(typeof(AOMTime.Weekend)).Cast<AOMTime.Weekend>().Select(v => v.ToString()).ToList();
                    break;
                case "Custom":
                    timeRepeatCollection.Clear();
                    gridCustomTime.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            timeRepeatCollection.Add(cb.Content.ToString());
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            timeRepeatCollection.Remove(cb.Content.ToString());
        }

        private void btnCloseGrid_Click(object sender, RoutedEventArgs e)
        {
            TimeGrid.Visibility = Visibility.Collapsed;
        }

        private void saveAomTime()
        {
            TimeGrid.Visibility = Visibility.Collapsed;
            int GenID = 0;
            if (AMAActionableObjects.FixedTimeCollection.Count > 0)
            {
                GenID = AMAActionableObjects.FixedTimeCollection.Max(x => x.AOMTimeID) + 1;
            }
            AMAActionableObjects.FixedTimeCollection.AddAOMTime(GenID, UserSelTime.SelectedTime.Value, timeRepeatCollection);
        }

        private void updateAomTime()
        {
            var holder = AMAActionableObjects.FixedTimeCollection.Where(x => x.AOMTimeID == timeClsHolder.AOMTimeID);
            foreach(AOMTime time in holder)
            {
                time.TimeValue = UserSelTime.SelectedTime.Value;
                time.TimeRepeatValues = timeRepeatCollection;
                TimeGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void clearAllText()
        {
            UserSelTime.SelectedTime = DateTime.Now;
            cboxRepeater.SelectedIndex = 0;
            gridCustomTime.Visibility = Visibility.Collapsed;
            foreach (CheckBox cb in checkBoxList) { cb.IsChecked = false; }
        }

        private void TimeItemControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                        OnComponentClick(selectedItem as AOMTime);
                        break;
                    }
                    elem = (Visual)VisualTreeHelper.GetParent(elem);
                }
            }
        }

        private void OnComponentClick(AOMTime aOMTime)
        {
            TimeGrid.Visibility = Visibility.Visible;
            btnContent = "Save!!";
            timeClsHolder = new AOMTime();
            timeClsHolder = aOMTime;
            timeRepeatCollection = new List<string>();
            UserSelTime.SelectedTime = timeClsHolder.TimeValue;

            List<string> timeRepeatCollectionWeekend = Enum.GetValues(typeof(AOMTime.Weekend)).Cast<AOMTime.Weekend>().Select(v => v.ToString()).ToList();
            List<string> timeRepeatCollectionWeekday = Enum.GetValues(typeof(AOMTime.Weekday)).Cast<AOMTime.Weekday>().Select(v => v.ToString()).ToList();
            List<string> timeRepeatCollectionEveryday = Enum.GetValues(typeof(AOMTime.Everyday)).Cast<AOMTime.Everyday>().Select(v => v.ToString()).ToList();
            bool a = compareList(timeClsHolder.TimeRepeatValues, timeRepeatCollectionWeekend);
            bool b = compareList(timeClsHolder.TimeRepeatValues, timeRepeatCollectionWeekday);
            bool c = compareList(timeClsHolder.TimeRepeatValues, timeRepeatCollectionEveryday);

            if(timeClsHolder.TimeRepeatValues[0] == "")
            {
                cboxRepeater.SelectedValue = Enum.Parse(typeof(AOMTime.TimeRepeater), "None");
            }
            else if (a)
            {
                cboxRepeater.SelectedValue = Enum.Parse(typeof(AOMTime.TimeRepeater), "Weekend");
            }
            else if (b)
            {
                cboxRepeater.SelectedValue = Enum.Parse(typeof(AOMTime.TimeRepeater), "Weekday");
            }
            else if (c)
            {
                cboxRepeater.SelectedValue = Enum.Parse(typeof(AOMTime.TimeRepeater), "Everyday");
            }
            else
            {
                cboxRepeater.SelectedValue = Enum.Parse(typeof(AOMTime.TimeRepeater), "Custom");
                for (int i = 0; i < checkBoxList.Count; i++)
                {
                    if (timeClsHolder.TimeRepeatValues.Contains(checkBoxList[i].Content))
                    {
                        checkBoxList[i].IsChecked = true;
                    }
                    else
                    {
                        checkBoxList[i].IsChecked = false;
                    }
                }
            }
        }
        
        private bool compareList(List<string> list1, List<string> list2)
        {
            var set1 = new HashSet<string>(list1);
            var set2 = new HashSet<string>(list2);
            return set1.SetEquals(set2);
        }
    }
}
