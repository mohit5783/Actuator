using Actuator.Model.Template;
using Actuator.Model.Template.ContentSwitcher;
using Actuator.Model.Template.Control;
using Actuator.Model.Template.ControlCreator;
using Actuator.Template.ucPropertySelected;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Actuator.Template
{
    /// <summary>
    /// Interaction logic for ucDesign.xaml
    /// </summary>
    public partial class ucDesign : Window
    {
        public rightMenuViewModel rvm { get; set; }
        public createControl controlCreator { get; set; }
        public clsDesignAction selectedDesignAction { get; set; }
        public clsListViewDesignBackground listViewDesignBgClass { get; set; }
        public clsXmlInteraction xmlInteractionClass { get; set; }

        public clsControl control { get; set; }
        public clsControls controlCollection { get; set; }
        public clsTextblock textBlockClass { get; set; }
        public clsTextblocks textBlockCollection { get; set; }
        public clsImage imageClass { get; set; }
        public clsImages imageCollection { get; set; }
        public clsButton buttonClass { get; set; }
        public clsButtons buttonCollection { get; set; }
        public clsButtonAction buttonActionClass { get; set; }
        public clsTextBox textBoxClass { get; set; }
        public clsTextBoxes textBoxCollection { get; set; }
        public clsComboBox comboboxClass { get; set; }
        public clsComboBoxes comboboxCollection { get; set; }
        public clsDatePicker datePickerClass { get; set; }
        public clsDatePickers datePickerCollection { get; set; }

        public clsObjects objectCollection { get; set; }

        public int countSaveClick { get; set; }
        public string tableName { get; set; }
        public string connectionStrings { get; set; }
        public int countDistinct { get; set; }

        Dictionary<string, string> dictionaryForFieldNameAndInputType;
        Dictionary<string, int> dictionaryForFieldNameAndId;
        Dictionary<string, List<string>> dictionaryForComboboxItem;
        Dictionary<string, string> dictionaryInputDatatype;
        List<string> comboboxOption;
        public string currentDictionaryKey { get; set; }
        public Guid templateId { get; set; }

        public clsDesign designClass { get; set; }

        public ucDesign(clsDesign designSelected)
        {
            InitializeComponent();
            designClass = designSelected;
            //templateId = Guid.NewGuid();
            templateId = designSelected.designId;
            countSaveClick = 1;
            rvm = new rightMenuViewModel();
            controlCreator = new createControl();
            listViewDesignBgClass = new clsListViewDesignBackground();
            controlCollection = new clsControls();
            textBlockCollection = new clsTextblocks();
            imageCollection = new clsImages();
            buttonCollection = new clsButtons();
            textBoxCollection = new clsTextBoxes();
            comboboxCollection = new clsComboBoxes();
            datePickerCollection = new clsDatePickers();
            buttonActionClass = new clsButtonAction();
            Switcher.pageSwitcher = this;
            dictionaryForFieldNameAndInputType = new Dictionary<string, string>();
            dictionaryForComboboxItem = new Dictionary<string, List<string>>();
            dictionaryInputDatatype = new Dictionary<string, string>();
            dictionaryForFieldNameAndId = new Dictionary<string, int>();

            //clsXmlInteraction.createFirstXml(templateId);
            loadDesignedTemplate();
            tbDescription.Text = designSelected.description;

            DataContext = this;
            
        }

        /// <summary>
        /// Used to load designed Template - need to provide ID of the template
        /// </summary>
        private void loadDesignedTemplate()
        {
            xmlInteractionClass = new clsXmlInteraction(templateId);
            xmlInteractionClass.loadDesignedTemplate(listViewDesignBgClass);
            objectCollection = new clsObjects();
            objectCollection = xmlInteractionClass.objectCollection;
            foreach (clsObject cO in objectCollection)
            {
                Grid grid = (Grid)cO.controlObject;
                listViewDesign.Items.Add(grid);
            }
        }

        private void designControlListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedDesignAction = (clsDesignAction)((ListBox)sender).SelectedItem;
            switch (selectedDesignAction.Name)
            {
                case "DATA ENTRY TEMPLATE":
                    createDataEntryTemplate();
                    break;
                case "TEXTBLOCK":
                    Grid textBlockGrid = (Grid)controlCreator.createCompleteTextblock();
                    listViewDesign.Items.Add(textBlockGrid);
                    break;
                case "IMAGE":
                    customWindowForImageSource.Visibility = Visibility.Visible;
                    break;
                case "BUTTON":
                    Grid buttonGrid = (Grid)controlCreator.createCompleteButton();
                    listViewDesign.Items.Add(buttonGrid);
                    break;
                case "CLEAR ALL":
                    clearDesign();
                    break;
                case "SAVE":
                    (countSaveClick == 1 ? (Action)saveData : updateData)();
                    break;
            }
        }

        private void clearClassCollection()
        {
            controlCollection.Clear();
            textBlockCollection.Clear();
            imageCollection.Clear();
            buttonCollection.Clear();
            textBoxCollection.Clear();
            comboboxCollection.Clear();
            datePickerCollection.Clear();
        }

        private void populateCollection()
        {
            clearClassCollection();
            foreach (Grid g in listViewDesign.Items)
            {
                control = new clsControl();
                switch (g.Name)
                {
                    case "TextBlock":
                        textBlockClass = (clsTextblock)g.DataContext;
                        textBlockCollection.addTextblock(textBlockClass);
                        control.id = textBlockClass.propId;
                        control.type = textBlockClass.propType;
                        break;
                    case "Image":
                        imageClass = (clsImage)g.DataContext;
                        imageCollection.addImage(imageClass);
                        control.id = imageClass.propId;
                        control.type = imageClass.propType;
                        break;
                    case "Button":
                        buttonClass = (clsButton)g.DataContext;
                        buttonCollection.addButton(buttonClass);
                        control.id = buttonClass.propId;
                        control.type = buttonClass.propType;
                        break;
                    case "TextBox":
                        textBoxClass = (clsTextBox)g.DataContext;
                        textBoxCollection.addTextBox(textBoxClass);
                        control.id = textBoxClass.propId;
                        control.type = textBoxClass.propType;
                        break;
                    case "ComboBox":
                        comboboxClass = (clsComboBox)g.DataContext;
                        comboboxCollection.addComboBox(comboboxClass);
                        control.id = comboboxClass.propId;
                        control.type = comboboxClass.propType;
                        break;
                    case "DatePicker":
                        datePickerClass = (clsDatePicker)g.DataContext;
                        datePickerCollection.addDatePicker(datePickerClass);
                        control.id = datePickerClass.propId;
                        control.type = datePickerClass.propType;
                        break;
                }
                controlCollection.addControl(control.id, control.type);
            }
            xmlInteractionClass = new clsXmlInteraction(templateId,
                listViewDesignBgClass,
                controlCollection,
                textBlockCollection,
                imageCollection,
                buttonCollection,
                textBoxCollection,
                comboboxCollection,
                datePickerCollection);
        }
        private void updateData()
        {
            populateCollection();
            xmlInteractionClass.updateData(designClass);
            MessageBox.Show("Data Updated");
        }
        private void saveData()
        {
            populateCollection();
            designClass.description = tbDescription.Text;
            xmlInteractionClass.saveData(designClass);
            countSaveClick = 2;
            MessageBox.Show("Data Saved");
        }
        private void clearDesign()
        {
            populateCollection();
            xmlInteractionClass.clearData();
            listViewDesign.Items.Clear();
            listViewDesignBgClass["propBackground"] = new SolidColorBrush(Colors.Black);
        }

        private void ListViewDesignedItem_Click(object sender, EventArgs e)
        {
            object gridChildren = (object)((Grid)((ListViewItem)sender).Content).Children[0];
            if (gridChildren.ToString().Contains("TextBlock"))
            {
                textBlockClass = new clsTextblock();
                textBlockClass = (clsTextblock)((Grid)((ListViewItem)sender).Content).DataContext;
                ((Grid)((ListViewItem)sender).Content).DataContext = textBlockClass;
                customWindowForControlProperties.Visibility = Visibility.Visible;
                Switcher.Switch(new ucTextBlock(textBlockClass));
            }
            if (gridChildren.ToString().Contains("Image"))
            {
                imageClass = new clsImage();
                imageClass = (clsImage)((Grid)((ListViewItem)sender).Content).DataContext;
                ((Grid)((ListViewItem)sender).Content).DataContext = imageClass;
                customWindowForControlProperties.Visibility = Visibility.Visible;
                Switcher.Switch(new ucImage(imageClass));
            }
            if (gridChildren.ToString().Contains("Button"))
            {
                buttonClass = new clsButton();
                buttonClass["propButtonAction"] = buttonActionClass;
                buttonClass = (clsButton)((Grid)((ListViewItem)sender).Content).DataContext;
                ((Grid)((ListViewItem)sender).Content).DataContext = buttonClass;
                customWindowForControlProperties.Visibility = Visibility.Visible;
                Switcher.Switch(new ucButton(buttonClass));
            }
            if (gridChildren.ToString().Contains("TextBox"))
            {
                textBoxClass = new clsTextBox();
                textBoxClass = (clsTextBox)((Grid)((ListViewItem)sender).Content).DataContext;
                ((Grid)((ListViewItem)sender).Content).DataContext = textBoxClass;
                customWindowForControlProperties.Visibility = Visibility.Visible;
                Switcher.Switch(new ucTextBox(textBoxClass));
            }
            if (gridChildren.ToString().Contains("ComboBox"))
            {
                listViewDesign.SelectedIndex = ((ListView)((Grid)((ListViewItem)sender).Content).Parent).Items.IndexOf(((Grid)((ListViewItem)sender).Content));
                comboboxClass = new clsComboBox();
                comboboxClass = (clsComboBox)((Grid)((ListViewItem)sender).Content).DataContext;
                ((Grid)((ListViewItem)sender).Content).DataContext = comboboxClass;
                customWindowForControlProperties.Visibility = Visibility.Visible;
                Switcher.Switch(new ucComboBox(comboboxClass));
            }

        }

        private void btnConfirmProperties_Click(object sender, RoutedEventArgs e)
        {
            //textBlockCollection.RemoveAt(textBlockCollection.IndexOf(textBlockCollection.Where(x => x.propId == textBlockClass.propId).FirstOrDefault()));
            //textBlockCollection.addTextblock(textBlockClass);
            customWindowForControlProperties.Visibility = Visibility.Collapsed;
        }
        private void listViewDesign_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            customWindowForControlProperties.Visibility = Visibility.Visible;
            Switcher.Switch(new ucMain(listViewDesignBgClass));
        }

        #region Image Creation Event
        private void createImageBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*|Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                customTextBoxForCreateImage.Text = filename;
            }
        }
        private void customTextBoxForCreateImageYesButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(customTextBoxForCreateImage.Text))
            {
                MessageBox.Show("Please enter the directory of your image");
            }
            else
            {
                try
                {
                    Grid imageGrid = (Grid)controlCreator.createCompleteImage(customTextBoxForCreateImage.Text);
                    listViewDesign.Items.Add(imageGrid);

                    customWindowForImageSource.Visibility = Visibility.Collapsed;
                    customTextBoxForCreateImage.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please make sure your directory is correct");
                    customTextBoxForCreateImage.Focus();
                    customTextBoxForCreateImage.SelectAll();
                }
            }
        }
        private void customTextBoxForCreateImageNoButton_Click(object sender, RoutedEventArgs e)
        {
            customWindowForImageSource.Visibility = Visibility.Collapsed;
            customTextBoxForCreateImage.Text = String.Empty;

        }
        #endregion

        #region Data Entry Event

        private void btnCloseCreateDataEntry_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            customWindowForDataEntryTemplate.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// User Choose Database Name
        /// </summary>
        private void createDataEntryTemplate()
        {
            cbDatabaseName.Items.Clear();
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; Integrated Security=True;"))
            {
                con.Open();
                foreach (DataRow database in con.GetSchema("Databases").Rows)
                {
                    cbDatabaseName.Items.Add(database.Field<String>("database_name"));
                }
                con.Close();
            }
            customWindowForDataEntryTemplate.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Based on Database Name in the combobox - get the name of tables and placed it in the tables name combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDatabaseName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDatabaseName.Items.Count > 1)
            {
                cbTableName.Items.Clear();
                gridDbFields.Visibility = Visibility.Collapsed;
                btnGenerateDataEntryDesign.Visibility = Visibility.Collapsed;
                connectionStrings = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=" + cbDatabaseName.SelectedItem.ToString() + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionStrings))
                    {
                        con.Open();
                        foreach (DataRow row in con.GetSchema("Tables").Rows)
                        {
                            ComboBoxItem cbitem = new ComboBoxItem();
                            string tablename = (string)row[2];
                            cbitem.Content = tablename;
                            cbTableName.Items.Add(cbitem);
                        }
                        con.Close();
                    }
                }
                catch (Exception ex) { }
                tBlockChooseTable.Visibility = Visibility.Visible;
                cbTableName.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// If table name changes - we need to clear the grid - so that the old one is not shown in the grid (Field name)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTableName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridDbFieldsName.Children.Clear();
            gridDbFieldsName.RowDefinitions.Clear();
            gridDbFieldInputType.Children.Clear();
            gridDbFieldInputType.RowDefinitions.Clear();
            if (cbTableName.Items.Count > 0) populateDbFieldName();
        }

        /// <summary>
        /// Based on table name - populate the grid for both Field name and Input Type
        /// </summary>
        private void populateDbFieldName()
        {
            dictionaryForFieldNameAndInputType.Clear();
            dictionaryForFieldNameAndId.Clear();
            dictionaryInputDatatype.Clear();
            dictionaryForComboboxItem.Clear();
            tableName = ((ComboBoxItem)cbTableName.SelectedItem).Content.ToString();
            DataTable schema = null;
            using (SqlConnection con = new SqlConnection(connectionStrings))
            {
                using (var schemaCommand = new SqlCommand("SELECT * FROM " + ((ComboBoxItem)cbTableName.SelectedItem).Content.ToString() + ";", con))
                {
                    con.Open();
                    using (var reader = schemaCommand.ExecuteReader(CommandBehavior.SchemaOnly))
                    {
                        schema = reader.GetSchemaTable();
                    }
                    con.Close();
                }
                for (int i = 0; i < schema.Rows.Count; i++)
                {
                    DataRow col = schema.Rows[i];

                    string inputDataType = col.ItemArray[12].ToString().Replace("System.", "");
                    bool autoIncrement = col.ItemArray[18].Equals(true);
                    using (SqlConnection connection = new SqlConnection(connectionStrings))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("SELECT COUNT(DISTINCT " + col.Field<String>("ColumnName") + ") FROM " + ((ComboBoxItem)cbTableName.SelectedItem).Content.ToString(), connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    countDistinct = int.Parse(reader[0].ToString());
                                }
                                reader.Close();
                            }
                        }
                        connection.Close();
                    }
                    if (!autoIncrement)
                    {
                        i--;
                        RowDefinition gridRow = new RowDefinition();
                        gridRow.Height = new GridLength(30);
                        gridDbFieldsName.RowDefinitions.Add(gridRow);

                        TextBlock tblock = new TextBlock();
                        tblock.Text = col.Field<String>("ColumnName");
                        tblock.Margin = new Thickness(5);
                        Grid.SetRow(tblock, i);
                        gridDbFieldsName.Children.Add(tblock);

                        RowDefinition gridRowInputType = new RowDefinition();
                        gridRowInputType.Height = new GridLength(30);
                        gridDbFieldInputType.RowDefinitions.Add(gridRowInputType);

                        ComboBox cBoxInputType = new ComboBox();
                        cBoxInputType.Name = tblock.Text;
                        cBoxInputType.Margin = new Thickness(0, 2, 0, 2);
                        cBoxInputType.Items.Add("TextBox");
                        cBoxInputType.Items.Add("DatePicker");
                        cBoxInputType.Items.Add("ComboBox");
                        cBoxInputType.SelectionChanged += CBoxInputType_SelectionChanged;

                        if (countDistinct < 30)
                        {
                            comboboxOption = new List<string>();
                            using (SqlConnection connection = new SqlConnection(connectionStrings))
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("SELECT DISTINCT " + col.Field<String>("ColumnName") + " FROM " + ((ComboBoxItem)cbTableName.SelectedItem).Content.ToString(), connection))
                                {
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            comboboxOption.Add(reader[0].ToString());
                                        }
                                        reader.Close();
                                    }
                                }
                                connection.Close();
                            }
                            dictionaryForComboboxItem.Add(tblock.Text, comboboxOption);
                            dictionaryForFieldNameAndInputType.Add(tblock.Text, "ComboBox");
                            dictionaryForFieldNameAndId.Add(tblock.Text, i);
                            dictionaryInputDatatype.Add(tblock.Text, inputDataType);
                            cBoxInputType.SelectedIndex = 2;
                            cBoxInputType.IsEnabled = false;
                            Grid.SetRow(cBoxInputType, i);
                            gridDbFieldInputType.Children.Add(cBoxInputType);
                            i++;
                        }
                        else
                        {
                            Grid.SetRow(cBoxInputType, i);
                            gridDbFieldInputType.Children.Add(cBoxInputType);
                            dictionaryForFieldNameAndInputType.Add(tblock.Text, null);
                            dictionaryForFieldNameAndId.Add(tblock.Text, i);
                            dictionaryInputDatatype.Add(tblock.Text, inputDataType);
                            i++;
                        }
                    }
                }
            }
            gridDbFields.Visibility = Visibility.Visible;
            btnGenerateDataEntryDesign.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// If the user choose combobox as the input type - then they need to populate the comboboxItemValue - open customWindowForComboboxItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBoxInputType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cboxInputTypeValue = (ComboBox)sender;
            dictionaryForFieldNameAndInputType[cboxInputTypeValue.Name] = cboxInputTypeValue.SelectedItem.ToString();
            currentDictionaryKey = cboxInputTypeValue.Name;
            if (cboxInputTypeValue.SelectedItem.ToString() == "ComboBox" && !dictionaryForComboboxItem.ContainsKey(cboxInputTypeValue.Name))
            {
                customWindowForComboboxItem.Visibility = Visibility.Visible;
                tblockComboboxTitle.Text = cboxInputTypeValue.Name;
            }
        }

        private void btnGenerateDataEntryDesign_Click(object sender, RoutedEventArgs e)
        {
            bool x = false;
            foreach (KeyValuePair<string, string> entry in dictionaryForFieldNameAndInputType)
            {
                if (entry.Value == null)
                {
                    x = true;
                }
            }
            if (x == true)
            {
                MessageBox.Show("Make sure each data has its own input type");
            }
            else
            {
                customWindowForDataEntryTemplate.Visibility = Visibility.Collapsed;
                tBlockChooseTable.Visibility = Visibility.Collapsed;
                cbTableName.Visibility = Visibility.Collapsed;
                gridDbFields.Visibility = Visibility.Collapsed;
                btnGenerateDataEntryDesign.Visibility = Visibility.Collapsed;
                createDataEntryDesign();
            }
        }

        private void createDataEntryDesign()
        {
            foreach (KeyValuePair<string, string> entry in dictionaryForFieldNameAndInputType)
            {
                Grid textBlockGrid = (Grid)controlCreator.createCompleteTextblock(entry.Key);
                listViewDesign.Items.Add(textBlockGrid);
                switch (entry.Value)
                {
                    case "TextBox":
                        Grid textBoxGrid = (Grid)controlCreator.createCompleteTextBox(entry.Key, dictionaryInputDatatype[entry.Key], dictionaryForFieldNameAndId[entry.Key]);
                        listViewDesign.Items.Add(textBoxGrid);
                        break;
                    case "ComboBox":
                        Grid comboboxGrid = (Grid)controlCreator.createCompleteCombobox(entry.Key, dictionaryInputDatatype[entry.Key], dictionaryForComboboxItem[entry.Key], dictionaryForFieldNameAndId[entry.Key]);
                        listViewDesign.Items.Add(comboboxGrid);
                        break;
                    case "DatePicker":
                        Grid datePickerGrid = (Grid)controlCreator.createCompleteDatePicker(entry.Key, dictionaryForFieldNameAndId[entry.Key]);
                        listViewDesign.Items.Add(datePickerGrid);
                        break;
                }
            }
            buttonActionClass.propTableName = tableName;
            buttonActionClass.propConnectionString = connectionStrings;
            var vFieldName = dictionaryForFieldNameAndInputType.Keys.ToArray();
            string fieldName = "";
            string valuesName = "";
            foreach (string sFName in vFieldName)
            {
                fieldName += sFName + ",";
                valuesName += "@" + sFName + ",";
            }
            fieldName = fieldName.Remove(fieldName.Count() - 1, 1);
            valuesName = valuesName.Remove(valuesName.Count() - 1, 1);
            string query = "INSERT INTO " + tableName + " (" + fieldName + ")" + " VALUES " + "(" + valuesName + ")";
            buttonActionClass.propQuery = query;
            Grid buttonGrid = (Grid)controlCreator.createCompleteButton(buttonActionClass);
            listViewDesign.Items.Add(buttonGrid);
        }
        #endregion

        #region User Input Value For Combobox
        private void customTextBoxForCreateCbYesButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(customTextBoxForCreateComboBox.Text))
            {
                MessageBox.Show("Please insert the combobox value separated by ; !");
            }
            else
            {
                List<string> value = customTextBoxForCreateComboBox.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                dictionaryForComboboxItem.Add(currentDictionaryKey, value);

                customTextBoxForCreateComboBox.Text = String.Empty;
                customWindowForComboboxItem.Visibility = Visibility.Collapsed;
            }
        }
        private void customTextBoxForCreateCbNoButton_Click(object sender, RoutedEventArgs e)
        {
            customWindowForComboboxItem.Visibility = Visibility.Collapsed;
            customTextBoxForCreateComboBox.Text = String.Empty;
        }


        #endregion

        #region Rearrange ListView Design
        private void rearrangeListViewUp_Click(object sender, MouseButtonEventArgs e)
        {
            var selectedIndex = this.listViewDesign.SelectedIndex;

            if (selectedIndex > 0)
            {
                object itemToMoveUp = (object)listViewDesign.Items[selectedIndex];
                listViewDesign.Items.RemoveAt(selectedIndex);
                listViewDesign.Items.Insert(selectedIndex - 1, itemToMoveUp);
                listViewDesign.SelectedIndex = selectedIndex - 1;
            }
        }
        private void rearrangeListViewDown_Click(object sender, MouseButtonEventArgs e)
        {
            var selectedIndex = this.listViewDesign.SelectedIndex;
            if (selectedIndex + 1 < listViewDesign.Items.Count && selectedIndex != -1)
            {
                object itemToMoveDown = (object)listViewDesign.Items[selectedIndex];
                listViewDesign.Items.RemoveAt(selectedIndex);
                listViewDesign.Items.Insert(selectedIndex + 1, itemToMoveDown);
                listViewDesign.SelectedIndex = selectedIndex + 1;
            }

        }
        #endregion

        #region ListView Item Right Button Click Event
        public void ListViewDesignedItemRightButton_Click(object sender, MouseButtonEventArgs e)
        {
            SelectItemOnRightClick(e);
            ContextMenu cm = this.FindResource("rightButtonListBoxItemContextMenu") as ContextMenu;
            cm.PlacementTarget = sender as Grid;
            cm.IsOpen = true;
        }

        private void deleteListViewItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Remove?", "Remove Item", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                listViewDesign.Items.RemoveAt(listViewDesign.SelectedIndex);
            }
        }

        private void SelectItemOnRightClick(System.Windows.Input.MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(listViewDesign);
            object element = listViewDesign.InputHitTest(clickPoint);
            if (element != null)
            {
                ListBoxItem clickedListBoxItem = GetVisualParent<ListBoxItem>(element);
                if (clickedListBoxItem != null)
                {
                    listViewDesign.SelectedItem = clickedListBoxItem.Content;
                }
            }
        }

        public T GetVisualParent<T>(object childObject) where T : Visual
        {
            DependencyObject child = childObject as DependencyObject;
            while ((child != null) && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }
        #endregion

        #region Properties Switch Navigation
        public void Navigate(UserControl nextPage)
        {
            propertiesContentSection.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            propertiesContentSection.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
        #endregion

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            populateCollection();
            designClass.description = tbDescription.Text;
            xmlInteractionClass.saveData(designClass);
            this.Close();
        }
    }
}
