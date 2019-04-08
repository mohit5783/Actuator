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
using System.IO;
using Microsoft.Win32;
using Actuator.Model.ActionableObjectsModel;
using System.Net.Mail;
using Actuator.Model.Helper;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using Actuator.Model.ActionableObjectModelCollection;

namespace Actuator.FeatureViews
{
    /// <summary>
    /// Interaction logic for AOMEmail.xaml
    /// </summary>
    public partial class AOMEmailView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public AOMEmailCollection AOMEmails { get; } = AMAActionableObjects.FixedEmailCollection;

        private IMarkupConverter markupConverter;
        public AOMEmail emailClsHolder { get; set; }

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

        public AOMEmailView()
        {
            InitializeComponent();
            markupConverter = new MarkupConverter();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            DataContext = this;
        }

        private void AddEmail_Click(object sender, RoutedEventArgs e)
        {
            btnContent = "Create!!";
            EmailGrid.Visibility = Visibility.Visible;
            clearAllText();
        }

        private void btnCloseGrid_Click(object sender, RoutedEventArgs e)
        {
            EmailGrid.Visibility = Visibility.Collapsed;
        }

        private void btnCreateEmail_Click(object sender, RoutedEventArgs e)
        {
            switch(btnContent)
            {
                case "Create!!":
                    saveAomEmail();
                    break;
                case "Save!!":
                    updateAomEmail();
                    break;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            switch(cb.Name)
            {
                case "cbCc":
                    spCC.Visibility = Visibility.Visible;
                    break;
                case "cbBcc":
                    spBcc.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            switch (cb.Name)
            {
                case "cbCc":
                    spCC.Visibility = Visibility.Collapsed;
                    break;
                case "cbBcc":
                    spBcc.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnAttachment_Click(object sender, RoutedEventArgs e)
        {
            // to do - figure out how to display in rtb, save in xml? 
        }

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            if (toolBarEditor.Visibility == Visibility.Collapsed)
            {
                toolBarEditor.Visibility = Visibility.Visible;
            }
            else
            {
                toolBarEditor.Visibility = Visibility.Collapsed;
            }
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Paragraph.TextAlignmentProperty);
            btnCenter.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextAlignment.Center));
            btnRight.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextAlignment.Right));
            btnLeft.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextAlignment.Left));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num1;
            bool res = int.TryParse(cmbFontSize.Text, out num1);
            if (res == false)
            {

            }
            else
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
            }
        }

        private void EmailItemControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                        OnComponentClick(selectedItem as AOMEmail);
                        break;
                    }
                    elem = (Visual)VisualTreeHelper.GetParent(elem);
                }
            }
        }

        private void OnComponentClick(AOMEmail aOMEmail)
        {
            EmailGrid.Visibility = Visibility.Visible;
            btnContent = "Save!!";
            emailClsHolder = new AOMEmail();
            emailClsHolder = aOMEmail;

            cbBcc.IsChecked = true;
            cbCc.IsChecked = true;
            tboxEmailTo.Text = aOMEmail.Email.To.ToString();
            tboxEmailCc.Text = aOMEmail.Email.CC.ToString();
            tboxEmailBcc.Text = aOMEmail.Email.Bcc.ToString();
            tboxEmailSubject.Text = aOMEmail.Email.Subject;
            string flowDoc = markupConverter.ConvertHtmlToXaml(aOMEmail.Email.Body.ToString());
            rtbEditor.Document = (FlowDocument)XamlReader.Parse(flowDoc);
        }

        private void btnAlignment_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = (ToggleButton)sender;
            switch(tb.Name)
            {
                case "btnCenter":
                    rtbEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
                    btnRight.IsChecked = false;
                    btnLeft.IsChecked = false;
                    break;
                case "btnRight":
                    rtbEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
                    btnCenter.IsChecked = false;
                    btnLeft.IsChecked = false;
                    break;
                case "btnLeft":
                    rtbEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
                    btnCenter.IsChecked = false;
                    btnRight.IsChecked = false;
                    break;
            } 
        }

        private void updateAomEmail()
        {
            var holder = AMAActionableObjects.FixedEmailCollection.Where(x => x.AOMEmailId == emailClsHolder.AOMEmailId);
            foreach(AOMEmail email in holder)
            {
                MailAddress from = new MailAddress("test@example.com");
                TextRange tr = new TextRange(rtbEditor.Document.ContentStart,
                                rtbEditor.Document.ContentEnd);
                MemoryStream ms = new MemoryStream();
                tr.Save(ms, DataFormats.Xaml);
                string xamlText = ASCIIEncoding.Default.GetString(ms.ToArray());
                xamlText = "<FlowDocument>" + xamlText + "</FlowDocument>";

                string htmlText = markupConverter.ConvertXamlToHtml(xamlText);

                email.Email.From = from;
                email.Email.To.Clear();
                email.Email.To.Add(tboxEmailTo.Text);

                email.Email.CC.Clear();
                email.Email.CC.Add(tboxEmailCc.Text);
                email.Email.Bcc.Clear();
                email.Email.Bcc.Add(tboxEmailBcc.Text);

                // add ReplyTo
                MailAddress replyto = new MailAddress("reply@example.com");
                email.Email.ReplyToList.Clear();
                email.Email.ReplyToList.Add(replyto);

                // set subject and encoding
                email.Email.Subject = tboxEmailSubject.Text;
                email.Email.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                email.Email.Body = htmlText;
                email.Email.BodyEncoding = System.Text.Encoding.UTF8;
                email.Email.IsBodyHtml = true;

                EmailGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void saveAomEmail()
        {
            List<string> emailToCollection = new List<string>();
            MailMessage myMail = new MailMessage();

            // add from,to mailaddresses
            MailAddress from = new MailAddress("test@example.com");

            //MailAddress to = null;
            //check if tboxEmailTo.Text is more than 1 - separated by ;
            //bool checkIfMoreThan1 = tboxEmailTo.Text.Contains(";");
            //if (checkIfMoreThan1 == false)
            //{
            //    to = new MailAddress(tboxEmailTo.Text);
            //}
            //else
            //{
            //    emailToCollection = tboxEmailTo.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            //}

            //if(emailToCollection.Count>0)
            //{
            //    foreach(string address in emailToCollection)
            //    {
            //        myMail.To.Add(address);
            //    }
            //}
            //else
            //{
            //    myMail.To.Add(to);
            //}


            TextRange tr = new TextRange(rtbEditor.Document.ContentStart,
                                rtbEditor.Document.ContentEnd);
            MemoryStream ms = new MemoryStream();
            tr.Save(ms, DataFormats.Xaml);
            string xamlText = ASCIIEncoding.Default.GetString(ms.ToArray());
            xamlText = "<FlowDocument>" + xamlText + "</FlowDocument>";

            string htmlText = markupConverter.ConvertXamlToHtml(xamlText);

            myMail.From = from;
            myMail.To.Add(tboxEmailTo.Text);

            myMail.CC.Add(tboxEmailCc.Text);
            myMail.Bcc.Add(tboxEmailBcc.Text);

            // add ReplyTo
            MailAddress replyto = new MailAddress("reply@example.com");
            myMail.ReplyToList.Add(replyto);

            // set subject and encoding
            myMail.Subject = tboxEmailSubject.Text;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = htmlText;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.IsBodyHtml = true;


            EmailGrid.Visibility = System.Windows.Visibility.Collapsed;
            int GenID = 0;
            if (AMAActionableObjects.FixedEmailCollection.Count > 0)
            {
                GenID = AMAActionableObjects.FixedEmailCollection.Max(x => x.AOMEmailId) + 1;
            }
            AMAActionableObjects.FixedEmailCollection.AddAOMEmail(GenID, myMail);
        }

        private void clearAllText()
        {
            tboxEmailBcc.Text = "";
            tboxEmailCc.Text = "";
            tboxEmailSubject.Text = "";
            tboxEmailTo.Text = "";
            cbBcc.IsChecked = false;
            cbCc.IsChecked = false;
            rtbEditor.Document.Blocks.Clear();
            toolBarEditor.Visibility = Visibility.Collapsed;
        }
    }
}
