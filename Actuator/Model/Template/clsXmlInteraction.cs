using Actuator.Model.Template.Control;
using Actuator.Model.Template.ControlCreator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Actuator.Model.Template
{
    public class clsXmlInteraction
    {
        public clsObject objectClass { get; set; }
        public clsObjects objectCollection { get; set; }
        
        public clsControl control { get; set; }
        public clsTextblock textBlockClass { get; set; }
        public clsImage imageClass { get; set; }
        public clsButton buttonClass { get; set; }
        public clsButtonAction buttonActionClass { get; set; }
        public clsTextBox textBoxClass { get; set; }
        public clsComboBox comboboxClass { get; set; }
        public clsDatePicker datePickerClass { get; set; }
        public createControl controlCreator { get; set; }


        public clsListViewDesignBackground listViewBgClass { get; set; }
        public clsControls controlCollection { get; set; }
        public clsTextblocks textBlockCollection { get; set; }
        public clsImages imageCollection { get; set; }
        public clsButtons buttonCollection { get; set; }
        public clsTextBoxes textBoxCollection { get; set; }
        public clsComboBoxes comboboxCollection { get; set; }
        public clsDatePickers datePickerCollection { get; set; }

        public string templateId { get; set; }
        static string designXmlFilePath = Directory.GetCurrentDirectory() + @"\Design.xml";

        public clsXmlInteraction(Guid templateId,
            clsListViewDesignBackground listViewBgClass, 
            clsControls controlCollection, 
            clsTextblocks textBlockCollection, 
            clsImages imageCollection, 
            clsButtons buttonCollection, 
            clsTextBoxes textBoxCollection, 
            clsComboBoxes comboboxCollection,
            clsDatePickers datePickerCollection)
        {
            this.templateId = templateId.ToString();
            this.listViewBgClass = listViewBgClass;
            this.controlCollection = controlCollection;
            this.textBlockCollection = textBlockCollection;
            this.imageCollection = imageCollection;
            this.buttonCollection = buttonCollection;
            this.textBoxCollection = textBoxCollection;
            this.comboboxCollection = comboboxCollection;
            this.datePickerCollection = datePickerCollection;
        }

        public clsXmlInteraction(Guid templateId)
        {
            this.templateId = templateId.ToString();
            controlCreator = new createControl();
            objectCollection = new clsObjects();
        }

        public void createDesignTag(string description)
        {
            XDocument xmlDocument = XDocument.Load(designXmlFilePath);
            XElement design = xmlDocument.Descendants("Designs").FirstOrDefault();
            design.Add(new XElement("Design", new XAttribute("id", templateId), new XAttribute("Description", description)));
            xmlDocument.Save(designXmlFilePath);
        }

        public void saveData(clsDesign designClass)
        {
            clearData();
            XDocument xmlDocument = XDocument.Load(designXmlFilePath);

            XElement designRoot = xmlDocument.Descendants("Design")
                             .Where(mi => mi.Attribute("id").Value == templateId)
                             .FirstOrDefault();
            designRoot.Attribute("Description").Value = designClass.description;
            if (listViewBgClass.propBackground.ToString() == "#00FFFFFF")
            {
                designRoot.Add(new XElement("Background", "#FFFFFF"));
            }
            else
            {
                designRoot.Add(new XElement("Background", listViewBgClass.propBackground.ToString()));
            }
            foreach (clsControl cc in controlCollection)
            {
                designRoot.Add(new XElement("Control", new XAttribute("id", cc.id), new XAttribute("type", cc.type)));
            }

            XElement textblockRoot = xmlDocument.Descendants("TextBlocks").FirstOrDefault();
            foreach (clsTextblock ctb in textBlockCollection)
            {
                textblockRoot.Add(new XElement("TextBlock", new XAttribute("id", ctb.propId.ToString()),
                                        new XElement("Foreground", ctb.propForeground),
                                        new XElement("Text", ctb.propContent),
                                        new XElement("Margin", ctb.propMargin),
                                        new XElement("HorizontalAlignment", ctb.propAlignment),
                                        new XElement("FontFamily", ctb.propFontFamily),
                                        new XElement("FontSize", ctb.propFontSize.ToString())
                                              )
                                 );
            }

            XElement imageRoot = xmlDocument.Descendants("Images").FirstOrDefault();
            foreach (clsImage cI in imageCollection)
            {
                if (!File.Exists(cI.propImageLocation))
                {
                    imageRoot.Add(new XElement("Image", new XAttribute("id", cI.propId.ToString()),
                                            new XElement("ImageLocation", cI.propImageLocation),
                                            new XElement("ImageData", cI.propImageData),
                                            new XElement("Height", cI.propImageHeight),
                                            new XElement("Margin", cI.propMargin),
                                            new XElement("Width", cI.propImageWidth.ToString()),
                                            new XElement("HorizontalAlignment", cI.propAlignment)
                                                  )
                                     );
                }
                else
                {
                    BitmapImage x = new BitmapImage(new Uri(cI.propImageLocation));
                    byte[] bits = getJPGFromImageControl(x);
                    string imgSource = Convert.ToBase64String(bits);
                    cI["propImageData"] = imgSource;
                    imageRoot.Add(new XElement("Image", new XAttribute("id", cI.propId.ToString()),
                                            new XElement("ImageLocation", cI.propImageLocation),
                                            new XElement("ImageData", cI.propImageData),
                                            new XElement("Height", cI.propImageHeight),
                                            new XElement("Margin", cI.propMargin),
                                            new XElement("Width", cI.propImageWidth.ToString()),
                                            new XElement("HorizontalAlignment", cI.propAlignment)
                                                  )
                                     );
                }
            }

            XElement buttonRoot = xmlDocument.Descendants("Buttons").FirstOrDefault();
            foreach (clsButton cbtn in buttonCollection)
            {
                buttonRoot.Add(new XElement("Button", new XAttribute("id", cbtn.propId.ToString()),
                                        new XElement("Foreground", cbtn.propForeground),
                                        new XElement("Background", cbtn.propBtnBackground),
                                        new XElement("Content", cbtn.propContent),
                                        new XElement("Height", cbtn.propHeight.ToString()),
                                        new XElement("Margin", cbtn.propMargin.ToString()),
                                        new XElement("Width", cbtn.propWidth.ToString()),
                                        new XElement("HorizontalAlignment", cbtn.propAlignment.ToString()),
                                        new XElement("FontFamily", cbtn.propFontFamily),
                                        new XElement("FontSize", cbtn.propFontSize.ToString()),
                                        new XElement("Action",
                                                new XElement("TableName", cbtn.propButtonAction == null ? "" : cbtn.propButtonAction.propTableName),
                                                new XElement("ConnectionString", cbtn.propButtonAction == null ? "" : cbtn.propButtonAction.propConnectionString),
                                                new XElement("Query", cbtn.propButtonAction == null ? "" : cbtn.propButtonAction.propQuery)
                                                    )
                                            )
                                );
            }

            XElement textBoxRoot = xmlDocument.Descendants("TextBoxes").FirstOrDefault();
            foreach (clsTextBox ctb in textBoxCollection)
            {
                textBoxRoot.Add(new XElement("TextBox", new XAttribute("id", ctb.propId.ToString()),
                                        new XElement("Name", ctb.propName),
                                        new XElement("DataType", ctb.propInputDataType),
                                        new XElement("Content", ctb.propContent),
                                        new XElement("Height", ctb.propHeight.ToString()),
                                        new XElement("Margin", ctb.propMargin.ToString()),
                                        new XElement("Width", ctb.propWidth.ToString()),
                                        new XElement("HorizontalAlignment", ctb.propAlignment.ToString())
                                            )
                                );
            }

            XElement comboboxRoot = xmlDocument.Descendants("ComboBoxes").FirstOrDefault();
            foreach (clsComboBox ccb in comboboxCollection)
            {
                string cbItemCollection = "";
                foreach(string item in ccb.propComboboxItem)
                {
                    cbItemCollection += item + ";";
                }
                comboboxRoot.Add(new XElement("ComboBox", new XAttribute("id", ccb.propId.ToString()),
                                        new XElement("Name", ccb.propName),
                                        new XElement("DataType", ccb.propInputType),
                                        new XElement("Height", ccb.propHeight.ToString()),
                                        new XElement("Margin", ccb.propMargin.ToString()),
                                        new XElement("ComboboxItem", cbItemCollection)
                                            )
                                );
            }

            XElement datePickerRoot = xmlDocument.Descendants("DatePickers").FirstOrDefault();
            foreach (clsDatePicker cdp in datePickerCollection)
            {
                datePickerRoot.Add(new XElement("DatePicker", new XAttribute("id", cdp.propId.ToString()),
                                        new XElement("Name", cdp.propName),
                                        new XElement("DataType", cdp.propInputType),
                                        new XElement("Height", cdp.propHeight.ToString()),
                                        new XElement("Width", cdp.propWidth.ToString())
                                            )
                                );
            }

            xmlDocument.Save(designXmlFilePath);
        }

        public void updateData(clsDesign designClass)
        {
            clearData();
            saveData(designClass);
        }

        public void clearData()
        {
            XDocument xmlDocument = XDocument.Load(designXmlFilePath);
            IEnumerable<XElement> designRoot = xmlDocument.Descendants("Design")
                             .Where(mi => mi.Attribute("id").Value == templateId)
                             .Elements();
            foreach (XElement control in designRoot)
            {
                if(control.Name != "Background")
                {
                    string id = control.Attribute("id").Value;
                    string type = control.Attribute("type").Value;

                    switch (type)
                    {
                        case "ComboBox":
                            XElement cboxRoot = xmlDocument.Descendants(type + "es").FirstOrDefault();
                            cboxRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).Remove();
                            break;
                        case "TextBox":
                            XElement tboxRoot = xmlDocument.Descendants(type + "es").FirstOrDefault();
                            tboxRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).Remove();
                            break;
                        default:
                            XElement controlRoot = xmlDocument.Descendants(type + "s").FirstOrDefault();
                            controlRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).Remove();
                            break;
                    }
                }
            }
            designRoot.Remove();
            xmlDocument.Save(designXmlFilePath);
        }

        public void loadDesignedTemplate(clsListViewDesignBackground lv)
        {
            XDocument xmlDocument = XDocument.Load(designXmlFilePath);
            IEnumerable<XElement> designRoot = xmlDocument.Descendants("Design")
                             .Where(mi => mi.Attribute("id").Value == templateId)
                             .Elements();
            foreach (XElement control in designRoot)
            {
                if (control.Name == "Background")
                {
                    lv["propBackground"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(control.Value));
                }
                else
                {
                    string id = control.Attribute("id").Value;
                    string type = control.Attribute("type").Value;

                    switch (type)
                    {
                        case "ComboBox":
                            XElement cboxRoot = xmlDocument.Descendants(type + "es").FirstOrDefault();
                            XElement cboxDetails = cboxRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).FirstOrDefault();
                            comboboxClass = new clsComboBox();
                            comboboxClass["propId"] = new Guid(id);
                            comboboxClass["propName"] = cboxDetails.Element("Name").Value;
                            comboboxClass["propInputType"] = cboxDetails.Element("DataType").Value;
                            comboboxClass["propMargin"] = cboxDetails.Element("Margin").Value == "NaN" ? convertToDoubleFromNaN(cboxDetails.Element("Margin").Value) : convertToDouble(cboxDetails.Element("Margin").Value);
                            comboboxClass["propHeight"] = cboxDetails.Element("Height").Value == "NaN" ? convertToDoubleFromNaN(cboxDetails.Element("Height").Value) : convertToDouble(cboxDetails.Element("Height").Value);
                            Grid comboboxGrid = (Grid)controlCreator.createCompleteComboboxWithClass(cboxDetails.Element("ComboboxItem").Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList(), comboboxClass);
                            objectClass = new clsObject();
                            objectClass["controlObject"] = comboboxGrid;
                            objectCollection.addObject(objectClass);
                            break;
                        case "TextBox":
                            XElement tboxRoot = xmlDocument.Descendants(type + "es").FirstOrDefault();
                            XElement tboxDetails = tboxRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).FirstOrDefault();
                            textBoxClass = new clsTextBox();
                            textBoxClass["propId"] = new Guid(id);
                            textBoxClass["propName"] = tboxDetails.Element("Name").Value;
                            textBoxClass["propContent"] = tboxDetails.Element("Content").Value;
                            textBoxClass["propHeight"] = tboxDetails.Element("Height").Value == "NaN" ? convertToDoubleFromNaN(tboxDetails.Element("Height").Value) : convertToDouble(tboxDetails.Element("Height").Value);
                            textBoxClass["propMargin"] = tboxDetails.Element("Margin").Value == "NaN" ? convertToDoubleFromNaN(tboxDetails.Element("Margin").Value) : convertToDouble(tboxDetails.Element("Margin").Value);
                            textBoxClass["propWidth"] = tboxDetails.Element("Width").Value == "NaN" ? convertToDoubleFromNaN(tboxDetails.Element("Width").Value) : convertToDouble(tboxDetails.Element("Width").Value);
                            textBoxClass["propAlignment"] = tboxDetails.Element("HorizontalAlignment").Value;
                            textBoxClass["propInputDataType"] = tboxDetails.Element("DataType").Value;
                            Grid textBoxGrid = (Grid)controlCreator.createCompleteTextBoxWithClass(textBoxClass);
                            objectClass = new clsObject();
                            objectClass["controlObject"] = textBoxGrid;
                            objectCollection.addObject(objectClass);
                            break;
                        case "TextBlock":
                            XElement tblockRoot = xmlDocument.Descendants(type + "s").FirstOrDefault();
                            XElement tblockDetails = tblockRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).FirstOrDefault();
                            textBlockClass = new clsTextblock();
                            textBlockClass["propId"] = new Guid(id);
                            textBlockClass["propContent"] = tblockDetails.Element("Text").Value;
                            textBlockClass["propAlignment"] = tblockDetails.Element("HorizontalAlignment").Value;
                            textBlockClass["propFontFamily"] = tblockDetails.Element("FontFamily").Value;
                            textBlockClass["propFontSize"] = double.Parse(tblockDetails.Element("FontSize").Value);
                            textBlockClass["propForeground"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(tblockDetails.Element("Foreground").Value));
                            textBlockClass["propMargin"] = tblockDetails.Element("Margin").Value == "NaN" ? convertToDoubleFromNaN(tblockDetails.Element("Margin").Value) : convertToDouble(tblockDetails.Element("Margin").Value); ;
                            Grid textBlockGrid = (Grid)controlCreator.createCompleteTextblockWithClass(textBlockClass);
                            objectClass = new clsObject();
                            objectClass["controlObject"] = textBlockGrid;
                            objectCollection.addObject(objectClass);
                            break;
                        case "Button":
                            XElement buttonRoot = xmlDocument.Descendants(type + "s").FirstOrDefault();
                            XElement buttonDetails = buttonRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).FirstOrDefault();
                            buttonClass = new clsButton();
                            buttonActionClass = new clsButtonAction();
                            buttonClass["propId"] = new Guid(id);
                            buttonClass["propContent"] = buttonDetails.Element("Content").Value;
                            buttonClass["propBtnBackground"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDetails.Element("Background").Value));
                            buttonClass["propForeground"] = (SolidColorBrush)(new BrushConverter().ConvertFrom(buttonDetails.Element("Foreground").Value));
                            buttonClass["propAlignment"] = buttonDetails.Element("HorizontalAlignment").Value;
                            buttonClass["propFontFamily"] = buttonDetails.Element("FontFamily").Value;

                            buttonClass["propHeight"] = buttonDetails.Element("Height").Value == "NaN" ? convertToDoubleFromNaN(buttonDetails.Element("Height").Value) : convertToDouble(buttonDetails.Element("Height").Value);
                            buttonClass["propMargin"] = buttonDetails.Element("Margin").Value == "NaN" ? convertToDoubleFromNaN(buttonDetails.Element("Margin").Value) : convertToDouble(buttonDetails.Element("Margin").Value);
                            buttonClass["propWidth"] = buttonDetails.Element("Width").Value == "NaN" ? convertToDoubleFromNaN(buttonDetails.Element("Width").Value) : convertToDouble(buttonDetails.Element("Width").Value); ;
                            buttonClass["propFontSize"] = double.Parse(buttonDetails.Element("FontSize").Value);
                            //To do - open action class - assign the value
                            Grid buttonGrid = (Grid)controlCreator.createCompleteButtonWithClass(buttonClass, buttonActionClass);
                            objectClass = new clsObject();
                            objectClass["controlObject"] = buttonGrid;
                            objectCollection.addObject(objectClass);
                            break;
                        case "DatePicker":
                            XElement datePickerRoot = xmlDocument.Descendants(type + "s").FirstOrDefault();
                            XElement datePickerDetails = datePickerRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).FirstOrDefault();
                            datePickerClass = new clsDatePicker();
                            datePickerClass["propId"] = new Guid(id);
                            datePickerClass["propName"] = datePickerDetails.Element("Name").Value;
                            datePickerClass["propInputType"] = datePickerDetails.Element("DataType").Value;
                            datePickerClass["propHeight"] = datePickerDetails.Element("Height").Value == "NaN" ? convertToDoubleFromNaN(datePickerDetails.Element("Height").Value) : convertToDouble(datePickerDetails.Element("Height").Value);
                            datePickerClass["propWidth"] = datePickerDetails.Element("Width").Value == "NaN" ? convertToDoubleFromNaN(datePickerDetails.Element("Width").Value) : convertToDouble(datePickerDetails.Element("Width").Value);
                            Grid datePickerGrid = (Grid)controlCreator.createCompleteDatePickerWithClass(datePickerClass);
                            objectClass = new clsObject();
                            objectClass["controlObject"] = datePickerGrid;
                            objectCollection.addObject(objectClass);
                            break;
                        case "Image":
                            XElement imageRoot = xmlDocument.Descendants(type + "s").FirstOrDefault();
                            XElement imageDetails = imageRoot.Descendants(type).Where(mi => mi.Attribute("id").Value == id).FirstOrDefault();
                            imageClass = new clsImage();
                            imageClass["propId"] = new Guid(id);
                            imageClass["propMargin"] = imageDetails.Element("Margin").Value == "NaN" ? convertToDoubleFromNaN(imageDetails.Element("Margin").Value) : convertToDouble(imageDetails.Element("Margin").Value);
                            imageClass["propImageHeight"] = imageDetails.Element("Height").Value == "NaN" ? convertToDoubleFromNaN(imageDetails.Element("Height").Value) : convertToDouble(imageDetails.Element("Height").Value);
                            imageClass["propImageWidth"] = imageDetails.Element("Width").Value == "NaN" ? convertToDoubleFromNaN(imageDetails.Element("Width").Value) : convertToDouble(imageDetails.Element("Width").Value);
                            imageClass["propAlignment"] = imageDetails.Element("HorizontalAlignment").Value;
                            imageClass["propImageData"] = imageDetails.Element("ImageData").Value;
                            imageClass["propImageLocation"] = imageDetails.Element("ImageLocation").Value;
                            Grid imageGrid = (Grid)controlCreator.createCompleteImageWithClass(imageClass);
                            objectClass = new clsObject();
                            objectClass["controlObject"] = imageGrid;
                            objectCollection.addObject(objectClass);
                            break;
                    }
                }
            }
        }


        #region COnvertImageToByteArray
        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
        #endregion

        #region Convert To Double For Loading Template
        private double convertToDouble(string value)
        {
            double dVal = Double.Parse(value);
            return dVal;
        }

        private double convertToDoubleFromNaN(string value)
        {
            double dVal = Double.Parse("NaN", CultureInfo.InvariantCulture);
            return dVal;
        }
        #endregion
    }
}
