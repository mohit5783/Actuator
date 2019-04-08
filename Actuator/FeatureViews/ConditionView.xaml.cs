using Actuator.Model.ActionableObjectModelCollection;
using Actuator.Model.ActionableObjectsModel;
using Actuator.Model.ActionModel;
using Actuator.Model.ConditionModel;
using Actuator.Model.DeviceModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Condition.xaml
    /// </summary>
    public partial class ConditionView : UserControl
    {
        #region Initalize Devices
        public Device dev { get; set; }
        public DeviceCollection AllDevices { get; set; }
        public DeviceEvent devevent { get; set; }
        public DeviceFunction devFunc { get; set; }
        public DeviceProperty devProp { get; set; }
        public FunctionParameterCollection fpc { get; set; }
        #endregion
        public ConditionsCollection CondColl { get; set; }
        public int UpdatedConditionID { get; set; }
        public ConditionView()
        {
            InitializeComponent();
            initAllDevComp();
            LoadAllDevices();
            LoadAllConditions();
            DataContext = this;
        }
        private void LoadAllConditions()
        {
            if (File.Exists("Conditions.xml"))
            {
                XElement root = XElement.Load("Conditions.xml");
                root.Elements("Conditions").Elements("Condition").All<XElement>(xe =>
                {
                    CondColl.AddCondition(Convert.ToInt32(xe.Attribute("id").Value),
                            xe.Attribute("Description").Value,
                            xe.Attribute("OperandType").Value,
                            xe.Attribute("FirstOperand").Value,
                            xe.Attribute("SecondOperand").Value,
                            xe.Attribute("Operator").Value);
                    return true;
                });
            }
            AMAActionableObjects.FixedConditionCollection = CondColl;
        }
        private void initAllDevComp()
        {
            fpc = new FunctionParameterCollection();
            dev = new Device();
            AllDevices = new DeviceCollection();
            devevent = new DeviceEvent();
            devFunc = new DeviceFunction();
            devProp = new DeviceProperty();

            CondColl = new ConditionsCollection();
        }
        private void LoadAllDevices()
        {
            if (File.Exists(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\Devices.xml"))
            {
                XElement root = XElement.Load(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\Devices.xml");
                root.Elements("Device").All<XElement>(xeDevices =>
                {
                    DeviceCapabilities devCap = new DeviceCapabilities();
                    DeviceEventCollection devEventColl = new DeviceEventCollection();
                    DeviceFunctionCollection devfuncColl = new DeviceFunctionCollection();
                    DevicePropertyCollection devPropColl = new DevicePropertyCollection();
                    xeDevices.Elements("Capabilities").Elements("events").Elements("event").All<XElement>(xe =>
                    {
                        devEventColl.AddDeviceEvent(xe.Attribute("id").Value, xe.Attribute("name").Value);
                        return true;
                    });
                    xeDevices.Elements("Capabilities").Elements("functions").Elements("function").All<XElement>(xe =>
                    {
                        devfuncColl.AddDeviceFunctions(xe.Attribute("id").Value,
                                                       xe.Attribute("functionname").Value,
                                                       fpc, /* this will taken care later*/
                                                       (ACTDataType)Enum.Parse(typeof(ACTDataType), "dt" + xe.Attribute("returntype").Value.ToLower()));
                        return true;
                    });
                    xeDevices.Elements("Capabilities").Elements("properties").Elements("property").All<XElement>(xe =>
                    {
                        devPropColl.AddDeviceProperties(xe.Attribute("id").Value,
                                                        xe.Attribute("name").Value,
                                                        (ACTDataType)Enum.Parse(typeof(ACTDataType), "dt" + xe.Attribute("type").Value.ToLower()),
                                                        Convert.ToBoolean(xe.Attribute("Readonly").Value));
                        return true;
                    });

                    devCap.Events = devEventColl;
                    devCap.Functions = devfuncColl;
                    devCap.Properties = devPropColl;

                    AllDevices.AddDevice(xeDevices.Attribute("id").Value,
                        xeDevices.Attribute("name").Value, devCap);
                    return true;
                });
            }
            if (File.Exists(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\SOA.xml"))
            {
                XElement root = XElement.Load(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\SOA.xml");
                root.Elements("Device").All<XElement>(xeDevices =>
                {
                    DeviceCapabilities devCap = new DeviceCapabilities();
                    DeviceEventCollection devEventColl = new DeviceEventCollection();
                    DeviceFunctionCollection devfuncColl = new DeviceFunctionCollection();
                    DevicePropertyCollection devPropColl = new DevicePropertyCollection();
                    xeDevices.Elements("Capabilities").Elements("events").Elements("event").All<XElement>(xe =>
                    {
                        devEventColl.AddDeviceEvent(xe.Attribute("id").Value, xe.Attribute("name").Value);
                        return true;
                    });
                    xeDevices.Elements("Capabilities").Elements("functions").Elements("function").All<XElement>(xe =>
                    {
                        devfuncColl.AddDeviceFunctions(xe.Attribute("id").Value,
                                                       xe.Attribute("functionname").Value,
                                                       fpc, /* this will taken care later*/
                                                       (ACTDataType)Enum.Parse(typeof(ACTDataType), "dt" + xe.Attribute("returntype").Value.ToLower()));
                        return true;
                    });
                    xeDevices.Elements("Capabilities").Elements("properties").Elements("property").All<XElement>(xe =>
                    {
                        devPropColl.AddDeviceProperties(xe.Attribute("id").Value,
                                                        xe.Attribute("name").Value,
                                                        (ACTDataType)Enum.Parse(typeof(ACTDataType), "dt" + xe.Attribute("type").Value.ToLower()),
                                                        Convert.ToBoolean(xe.Attribute("Readonly").Value));
                        return true;
                    });

                    devCap.Events = devEventColl;
                    devCap.Functions = devfuncColl;
                    devCap.Properties = devPropColl;

                    AllDevices.AddDevice(xeDevices.Attribute("id").Value,
                        xeDevices.Attribute("name").Value, devCap);
                    return true;
                });
            }
        }
        private void AddCondition_Click(object sender, RoutedEventArgs e)
        {
            txtCondName.Text = "Condition " + (CondColl.Max(x => x.id) + 1).ToString();
            GrdType.Visibility = Visibility.Visible;
        }
        private void FirstOperandChoosen_Click(object sender, RoutedEventArgs e)
        {
            //TxbValue.Text = (cmbDeviceSOADate.SelectedItem as Device).Capabilities.Properties[cmbDeviceSOADate.SelectedIndex].PropertyName;
            CmbValueProp.ItemsSource = (cmbDeviceSOADate.SelectedItem as Device).Capabilities.Properties;

            GrdType.Visibility = Visibility.Collapsed;
            GrdTypeValue.Visibility = Visibility.Visible;
        }
        private void LoadcbOperator(ACTDataType propertyType)
        {
            cbOperator.Items.Clear();
            switch (propertyType)
            {
                case ACTDataType.dtstring:
                    LoadStringOperator();
                    break;
                case ACTDataType.dtint:
                    LoadIntOperator();
                    break;
                case ACTDataType.dtbool:
                    LoadBoolOperator();
                    break;
                default:
                    LoadIntOperator();
                    break;
            }
        }
        private void LoadBoolOperator()
        {
            cbOperator.Items.Add("True");
            cbOperator.Items.Add("False");
        }
        private void LoadIntOperator()
        {
            cbOperator.Items.Add("Smaller");
            cbOperator.Items.Add("Smaller or Equals to");
            cbOperator.Items.Add("Greater");
            cbOperator.Items.Add("Greater or Equals to");
            cbOperator.Items.Add("Equals to");
            cbOperator.Items.Add("Not Equals to");
        }
        private void LoadStringOperator()
        {
            cbOperator.Items.Add("Equals to");
            cbOperator.Items.Add("Not Equals to");
        }
        private void btnCloseFirstOpGrid_Click(object sender, RoutedEventArgs e)
        {
            GrdType.Visibility = Visibility.Collapsed;
        }
        private void btnCloseSecondOpGrid_Click(object sender, RoutedEventArgs e)
        {
            GrdTypeValue.Visibility = Visibility.Collapsed;
        }
        private void SecondOperandChoosen_Click(object sender, RoutedEventArgs e)
        {
            TxtFirstOp.Text = cmbDeviceSOADate.Text;
            TxtSecondOp.Text = cmbDeviceSOADateValue.Text;
            GrdTypeValue.Visibility = Visibility.Collapsed;
            OperatorGrid.Visibility = Visibility.Visible;
        }
        private void SaveCondition_Click(object sender, RoutedEventArgs e)
        {
            Model.ConditionModel.Condition cond = new Model.ConditionModel.Condition();
            if (SaveCondition.Content.ToString().ToUpper() != "Save!!".ToUpper())
            {
                if (CondColl.Count <= 0)
                    cond.id = 1;
                else
                    cond.id = CondColl.Max(x => x.id) + 1;
            }
            else
            {
                cond.id = UpdatedConditionID;
                CondColl.Remove(CondColl.Where(x => x.id == UpdatedConditionID).FirstOrDefault());
            }

            cond.OperandType = (CmbValueProp.SelectedItem as DeviceProperty).PropertyType.ToString().Replace("dt", "");
            cond.FirstOperand = cmbDeviceSOADate.Text;
            cond.SecondOperand = cmbDeviceSOADateValue.Text;
            cond.Operator = cbOperator.Text;
            cond.Description = txtCondName.Text;

            CondColl.Add(cond);
            AMAActionableObjects.FixedConditionCollection = CondColl;

            OperatorGrid.Visibility = Visibility.Collapsed;
        }
        private void btnCloseOperatorGrid_Click(object sender, RoutedEventArgs e)
        {
            OperatorGrid.Visibility = Visibility.Collapsed;
        }
        private void CmbValueProp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadcbOperator((CmbValueProp.SelectedItem as DeviceProperty).PropertyType);
        }
        private void ItemsControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                        OnComponentClick(selectedItem as Model.ConditionModel.Condition);
                        break;
                    }
                    elem = (Visual)VisualTreeHelper.GetParent(elem);
                }
            }
        }
        private void OnComponentClick(Model.ConditionModel.Condition cond)
        {
            UpdatedConditionID = cond.id;


            cmbDeviceSOADate.Text = cond.FirstOperand;
            cmbDeviceSOADateValue.Text = cond.SecondOperand;
            CmbValueProp.Text = cond.OperandType;
            //LoadcbOperator((CmbValueProp.SelectedItem as DeviceProperty).PropertyType);
            cbOperator.Text = cond.Operator;
            txtCondName.Text = cond.Description;

            SaveCondition.Content = "Save!!";
            GrdType.Visibility = Visibility.Visible;
        }
    }
}
