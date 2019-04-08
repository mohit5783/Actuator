using Actuator.Model.Template.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Actuator.Model.Template.ControlCreator
{
    public class createControl
    {
        public clsTextblock textBlockClass { get; set; }
        public clsTextBox textBoxClass { get; set; }
        public clsComboBox comboboxClass { get; set; }
        public clsDatePicker datePickerClass { get; set; }
        public clsButton buttonClass { get; set; }
        public clsImage imageClass { get; set; }

        public object createGrid()
        {
            Grid DynamicGrid = new Grid();
            DynamicGrid.Width = double.NaN;
            DynamicGrid.Height = double.NaN;
            DynamicGrid.Background = new SolidColorBrush(Colors.Transparent);
            RowDefinition gridRow1 = new RowDefinition();
            DynamicGrid.RowDefinitions.Add(gridRow1);

            object sendGridAsObject = (object)DynamicGrid;
            return sendGridAsObject;
        }

        public object createTextblock()
        {
            TextBlock tBlock = new TextBlock();
            tBlock.MinWidth = 10;
            tBlock.MinHeight = 10;

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("propMargin");
            myBinding.Mode = BindingMode.OneWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding2 = new Binding();
            myBinding2.Path = new PropertyPath("propContent");
            myBinding2.Mode = BindingMode.OneWay;
            myBinding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding4 = new Binding();
            myBinding4.Path = new PropertyPath("propFontSize");
            myBinding4.Mode = BindingMode.OneWay;
            myBinding4.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding5 = new Binding();
            myBinding5.Path = new PropertyPath("propAlignment");
            myBinding5.Mode = BindingMode.OneWay;
            myBinding5.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding6 = new Binding();
            myBinding6.Path = new PropertyPath("propFontFamily");
            myBinding6.Mode = BindingMode.OneWay;
            myBinding6.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding7 = new Binding();
            myBinding7.Path = new PropertyPath("propForeground");
            myBinding7.Mode = BindingMode.OneWay;
            myBinding7.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            BindingOperations.SetBinding(tBlock, TextBlock.MarginProperty, myBinding);
            BindingOperations.SetBinding(tBlock, TextBlock.TextProperty, myBinding2);
            BindingOperations.SetBinding(tBlock, TextBlock.FontSizeProperty, myBinding4);
            BindingOperations.SetBinding(tBlock, TextBlock.HorizontalAlignmentProperty, myBinding5);
            BindingOperations.SetBinding(tBlock, TextBlock.FontFamilyProperty, myBinding6);
            BindingOperations.SetBinding(tBlock, TextBlock.ForegroundProperty, myBinding7);

            object sendTextBlockAsObject = (object)tBlock;
            return sendTextBlockAsObject;
        }

        public object createButton()
        {
            Button btn = new Button();
            btn.MinWidth = 10;
            btn.MinHeight = 10;

            TextBlock tb = new TextBlock();
            btn.Content = tb;

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("propMargin");
            myBinding.Mode = BindingMode.OneWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding2 = new Binding();
            myBinding2.Path = new PropertyPath("propContent");
            myBinding2.Mode = BindingMode.OneWay;
            myBinding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding3 = new Binding();
            myBinding3.Path = new PropertyPath("propWidth");
            myBinding3.Mode = BindingMode.OneWay;
            myBinding3.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding4 = new Binding();
            myBinding4.Path = new PropertyPath("propHeight");
            myBinding4.Mode = BindingMode.OneWay;
            myBinding4.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding5 = new Binding();
            myBinding5.Path = new PropertyPath("propAlignment");
            myBinding5.Mode = BindingMode.OneWay;
            myBinding5.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding6 = new Binding();
            myBinding6.Path = new PropertyPath("propFontFamily");
            myBinding6.Mode = BindingMode.OneWay;
            myBinding6.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding7 = new Binding();
            myBinding7.Path = new PropertyPath("propFontSize");
            myBinding7.Mode = BindingMode.OneWay;
            myBinding7.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding8 = new Binding();
            myBinding8.Path = new PropertyPath("propForeground");
            myBinding8.Mode = BindingMode.OneWay;
            myBinding8.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding9 = new Binding();
            myBinding9.Path = new PropertyPath("propBtnBackground");
            myBinding9.Mode = BindingMode.OneWay;
            myBinding9.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            BindingOperations.SetBinding(btn, Button.MarginProperty, myBinding);
            BindingOperations.SetBinding(tb, TextBlock.TextProperty, myBinding2);
            BindingOperations.SetBinding(btn, Button.WidthProperty, myBinding3);
            BindingOperations.SetBinding(btn, Button.HeightProperty, myBinding4);
            BindingOperations.SetBinding(btn, Button.HorizontalAlignmentProperty, myBinding5);
            BindingOperations.SetBinding(tb, TextBlock.FontFamilyProperty, myBinding6);
            BindingOperations.SetBinding(tb, TextBlock.FontSizeProperty, myBinding7);
            BindingOperations.SetBinding(tb, TextBlock.ForegroundProperty, myBinding8);
            BindingOperations.SetBinding(btn, Button.BackgroundProperty, myBinding9);
            
            object sendButtonAsObject = (object)btn;
            return sendButtonAsObject;
        }

        public object createImage()
        {
            Image img = new Image();
            img.MinWidth = 10;
            img.MinHeight = 10;

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("propMargin");
            myBinding.Mode = BindingMode.OneWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding3 = new Binding();
            myBinding3.Path = new PropertyPath("propImageWidth");
            myBinding3.Mode = BindingMode.OneWay;
            myBinding3.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding5 = new Binding();
            myBinding5.Path = new PropertyPath("propAlignment");
            myBinding5.Mode = BindingMode.OneWay;
            myBinding5.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding4 = new Binding();
            myBinding4.Path = new PropertyPath("propImageHeight");
            myBinding4.Mode = BindingMode.OneWay;
            myBinding4.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding6 = new Binding();
            myBinding6.Path = new PropertyPath("propImageLocation");
            myBinding6.Mode = BindingMode.OneWay;
            myBinding6.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            BindingOperations.SetBinding(img, Image.MarginProperty, myBinding);
            BindingOperations.SetBinding(img, Image.HeightProperty, myBinding4);
            BindingOperations.SetBinding(img, Image.WidthProperty, myBinding3);
            BindingOperations.SetBinding(img, Image.HorizontalAlignmentProperty, myBinding5);
            BindingOperations.SetBinding(img, Image.SourceProperty, myBinding6);
            
            object sendImageAsObject = (object)img;
            return sendImageAsObject;
        }
        

        public object createTextBox()
        {
            TextBox tBox = new TextBox();
            tBox.MinWidth = 10;
            tBox.MinHeight = 10;

            tBox.BorderBrush = Brushes.Black;
            tBox.BorderThickness = new Thickness(1);

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("propMargin");
            myBinding.Mode = BindingMode.OneWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding2 = new Binding();
            myBinding2.Path = new PropertyPath("propContent");
            myBinding2.Mode = BindingMode.OneWay;
            myBinding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding3 = new Binding();
            myBinding3.Path = new PropertyPath("propWidth");
            myBinding3.Mode = BindingMode.OneWay;
            myBinding3.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding4 = new Binding();
            myBinding4.Path = new PropertyPath("propHeight");
            myBinding4.Mode = BindingMode.OneWay;
            myBinding4.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding5 = new Binding();
            myBinding5.Path = new PropertyPath("propAlignment");
            myBinding5.Mode = BindingMode.OneWay;
            myBinding5.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            BindingOperations.SetBinding(tBox, TextBox.MarginProperty, myBinding);
            BindingOperations.SetBinding(tBox, TextBox.TextProperty, myBinding2);
            BindingOperations.SetBinding(tBox, TextBox.WidthProperty, myBinding3);
            BindingOperations.SetBinding(tBox, TextBox.HeightProperty, myBinding4);
            BindingOperations.SetBinding(tBox, TextBox.HorizontalAlignmentProperty, myBinding5);

            object sendTextBoxAsObject = (object)tBox;
            return sendTextBoxAsObject;
        }

        public object createCombobox(List<string> cbItem)
        {
            ComboBox cbox = new ComboBox();
            cbox.MinWidth = 10;
            cbox.MinHeight = 10;

            foreach (string item in cbItem)
            {
                ComboBoxItem cboxitem = new ComboBoxItem();
                cboxitem.Content = item;
                cbox.Items.Add(cboxitem);
            }

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("propMargin");
            myBinding.Mode = BindingMode.OneWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding4 = new Binding();
            myBinding4.Path = new PropertyPath("propHeight");
            myBinding4.Mode = BindingMode.OneWay;
            myBinding4.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            BindingOperations.SetBinding(cbox, ComboBox.MarginProperty, myBinding);
            BindingOperations.SetBinding(cbox, ComboBox.HeightProperty, myBinding4);

            object sendComboboxAsObject = (object)cbox;
            return sendComboboxAsObject;
        }

        public object createDatePicker()
        {
            DatePicker dPicker = new DatePicker();
            dPicker.MinWidth = 10;
            dPicker.MinHeight = 10;

            dPicker.FirstDayOfWeek = DayOfWeek.Monday;
            dPicker.IsTodayHighlighted = true;

            Binding myBinding3 = new Binding();
            myBinding3.Path = new PropertyPath("propWidth");
            myBinding3.Mode = BindingMode.OneWay;
            myBinding3.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding myBinding4 = new Binding();
            myBinding4.Path = new PropertyPath("propHeight");
            myBinding4.Mode = BindingMode.OneWay;
            myBinding4.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            BindingOperations.SetBinding(dPicker, DatePicker.WidthProperty, myBinding3);
            BindingOperations.SetBinding(dPicker, DatePicker.HeightProperty, myBinding4);

            object sendDatePickerAsObject = (object)dPicker;
            return sendDatePickerAsObject;
        }


        #region Create Control With Grid Included
        public object createCompleteTextblock()
        {
            Grid textBlockGrid = (Grid)createGrid();
            textBlockGrid.Name = "TextBlock";
            textBlockClass = new clsTextblock();
            textBlockClass["propId"] = Guid.NewGuid();
            textBlockGrid.DataContext = textBlockClass;
            textBlockGrid.Children.Add((TextBlock)createTextblock());

            object sendtextBlockGridAsObject = (object)textBlockGrid;
            return sendtextBlockGridAsObject;
        }

        public object createCompleteTextblock(string content)
        {
            Grid textBlockGrid = (Grid)createGrid();
            textBlockGrid.Name = "TextBlock";
            textBlockClass = new clsTextblock();
            textBlockClass["propId"] = Guid.NewGuid();
            textBlockClass["propContent"] = content;
            textBlockGrid.DataContext = textBlockClass;
            textBlockGrid.Children.Add((TextBlock)createTextblock());

            object sendtextBlockGridAsObject = (object)textBlockGrid;
            return sendtextBlockGridAsObject;
        }

        public object createCompleteTextBox(string dicEntryKey, string inputDataType, int inputId)
        {
            Grid textBoxGrid = (Grid)createGrid();
            textBoxGrid.Name = "TextBox";
            textBoxClass = new clsTextBox();
            textBoxClass["propId"] = Guid.NewGuid();
            textBoxClass["propName"] = inputId.ToString();
            //textBoxClass["propName"] = dicEntryKey;
            textBoxClass["propInputDataType"] = inputDataType;
            textBoxGrid.DataContext = textBoxClass;
            textBoxGrid.Children.Add((TextBox)createTextBox());

            object sendtextBoxGridAsObject = (object)textBoxGrid;
            return sendtextBoxGridAsObject;
        }

        public object createCompleteCombobox(string dicEntryKey, string inputDataType, List<string> comboboxItem, int inputId)
        {
            Grid comboboxGrid = (Grid)createGrid();
            comboboxGrid.Name = "ComboBox";
            comboboxClass = new clsComboBox();
            comboboxClass["propId"] = Guid.NewGuid();
            comboboxClass["propName"] = inputId.ToString();
            //textBoxClass["propName"] = dicEntryKey;
            comboboxClass["propInputType"] = inputDataType;
            comboboxGrid.DataContext = comboboxClass;
            comboboxGrid.Children.Add((ComboBox)createCombobox(comboboxItem));
            comboboxClass["propComboboxItem"] = comboboxItem;

            object sendcomboboxGridAsObject = (object)comboboxGrid;
            return sendcomboboxGridAsObject;
        }
        
        public object createCompleteDatePicker(string dicEntryKey, int inputId)
        {
            Grid datePickerGrid = (Grid)createGrid();
            datePickerGrid.Name = "DatePicker";
            datePickerClass = new clsDatePicker();
            datePickerClass["propId"] = Guid.NewGuid();
            datePickerClass["propName"] = inputId.ToString();
            //textBoxClass["propName"] = dicEntryKey;
            datePickerGrid.DataContext = datePickerClass;
            datePickerGrid.Children.Add((DatePicker)createDatePicker());

            object senddatePickerGridAsObject = (object)datePickerGrid;
            return senddatePickerGridAsObject;
        }

        public object createCompleteButton()
        {
            Grid buttonGrid = (Grid)createGrid();
            buttonGrid.Name = "Button";
            buttonClass = new clsButton();
            buttonClass["propId"] = Guid.NewGuid();
            buttonGrid.DataContext = buttonClass;
            buttonGrid.Children.Add((Button)createButton());

            object sendbuttonGridAsObject = (object)buttonGrid;
            return sendbuttonGridAsObject;
        }

        public object createCompleteButton(clsButtonAction buttonAction)
        {
            Grid buttonGrid = (Grid)createGrid();
            buttonGrid.Name = "Button";
            buttonClass = new clsButton();
            buttonClass["propId"] = Guid.NewGuid();
            buttonClass["propButtonAction"] = buttonAction;
            buttonGrid.DataContext = buttonClass;
            buttonGrid.Children.Add((Button)createButton());

            object sendbuttonGridAsObject = (object)buttonGrid;
            return sendbuttonGridAsObject;
        }

        public object createCompleteImage(string imageSource)
        {
            Grid imageGrid = (Grid)createGrid();
            imageGrid.Name = "Image";
            imageClass = new clsImage();
            imageClass["propId"] = Guid.NewGuid();
            imageGrid.DataContext = imageClass;
            imageClass["propImageLocation"] = imageSource;
            imageGrid.Children.Add((Image)createImage());

            object sendimageGridAsObject = (object)imageGrid;
            return sendimageGridAsObject;
        }
        #endregion

        #region create Control With Class
        public object createCompleteComboboxWithClass(List<string> comboboxItem, clsComboBox clsCb)
        {
            Grid comboboxGrid = (Grid)createGrid();
            comboboxGrid.Name = "ComboBox";
            comboboxClass = clsCb;
            comboboxGrid.DataContext = comboboxClass;
            comboboxGrid.Children.Add((ComboBox)createCombobox(comboboxItem));
            comboboxClass["propComboboxItem"] = comboboxItem;

            object sendcomboboxGridAsObject = (object)comboboxGrid;
            return sendcomboboxGridAsObject;
        }
        public object createCompleteTextBoxWithClass(clsTextBox clsTbox)
        {
            Grid textBoxGrid = (Grid)createGrid();
            textBoxGrid.Name = "TextBox";
            textBoxClass = clsTbox;
            textBoxGrid.DataContext = textBoxClass;
            textBoxGrid.Children.Add((TextBox)createTextBox());

            object sendtextBoxGridAsObject = (object)textBoxGrid;
            return sendtextBoxGridAsObject;
        }
        public object createCompleteImageWithClass(clsImage clsImg)
        {
            Grid imageGrid = (Grid)createGrid();
            imageGrid.Name = "Image";
            imageClass = clsImg;
            imageGrid.DataContext = imageClass;

            Image img = (Image)createImage();
            imageGrid.Children.Add(img);

            object sendimageGridAsObject = (object)imageGrid;
            return sendimageGridAsObject;
        }
        public object createCompleteButtonWithClass(clsButton clsBtn, clsButtonAction clsBtnAction)
        {
            Grid buttonGrid = (Grid)createGrid();
            buttonGrid.Name = "Button";
            buttonClass = clsBtn;
            buttonClass["propButtonAction"] = clsBtnAction;
            buttonGrid.DataContext = buttonClass;
            buttonGrid.Children.Add((Button)createButton());

            object sendbuttonGridAsObject = (object)buttonGrid;
            return sendbuttonGridAsObject;
        }
        public object createCompleteDatePickerWithClass(clsDatePicker clsDp)
        {
            Grid datePickerGrid = (Grid)createGrid();
            datePickerGrid.Name = "DatePicker";
            datePickerClass = clsDp;
            datePickerGrid.DataContext = datePickerClass;
            datePickerGrid.Children.Add((DatePicker)createDatePicker());

            object senddatePickerGridAsObject = (object)datePickerGrid;
            return senddatePickerGridAsObject;
        }
        public object createCompleteTextblockWithClass(clsTextblock clsTb)
        {
            Grid textBlockGrid = (Grid)createGrid();
            textBlockGrid.Name = "TextBlock";
            textBlockClass = clsTb;
            textBlockGrid.DataContext = textBlockClass;
            textBlockGrid.Children.Add((TextBlock)createTextblock());

            object sendtextBlockGridAsObject = (object)textBlockGrid;
            return sendtextBlockGridAsObject;
        }
        #endregion

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
