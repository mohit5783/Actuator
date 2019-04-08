using Actuator.Model.ActionModel;
using Actuator.Model.DeviceModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionView : UserControl
    {
        #region Initalize Devices
        public Device dev { get; set; }
        public DeviceCollection AllDevices { get; set; }
        public DeviceEvent devevent { get; set; }
        public DeviceFunction devFunc { get; set; }
        public DeviceProperty devProp { get; set; }
        public FunctionParameterCollection fpc { get; set; }

        #endregion
        public WorkflowActionCollection WorkflowActions { get; set; }
        public int UpdatedWorkflowActionID { get; set; }
        public ActionView()
        {
            InitializeComponent();
            initAllDevComp();
            LoadAllDevices();
            LoadAllWorkflowActions();
            DataContext = this;
        }
        private void LoadAllWorkflowActions()
        {
            if (File.Exists("WorkflowActions.xml"))
            {
                XElement root = XElement.Load("WorkflowActions.xml");
                root.Elements("WorkflowAction").All<XElement>(xe =>
                {
                    WorkflowActions.AddWorkflowActions(Convert.ToInt32(xe.Attribute("id").Value),
                            xe.Attribute("ActionName").Value,
                            xe.Attribute("ObjectName").Value,
                            xe.Attribute("VarName").Value,
                            xe.Attribute("VarValue").Value);
                    return true;
                });
            }
            AMAActionableObjects.FixedWorkflowActionCollection = WorkflowActions;
        }
        private void initAllDevComp()
        {
            fpc = new FunctionParameterCollection();
            //devCap = new DeviceCapabilities();

            dev = new Device();
            AllDevices = new DeviceCollection();

            devevent = new DeviceEvent();
            devFunc = new DeviceFunction();
            devProp = new DeviceProperty();

            WorkflowActions = new WorkflowActionCollection();
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
        private void AddAction_Click(object sender, RoutedEventArgs e)
        {
            if (WorkflowActions.Count <= 0)
                TxtWorkflowActionName.Text = "Action 1";
            else
                TxtWorkflowActionName.Text = "Action " + (WorkflowActions.Max(x => x.id) + 1).ToString();

            cmbDeviceSOADate.Text = "";
            CmbValueProp.Text = "";
            cmbDeviceSOADateValue.Text = "";


            GrdType.Visibility = Visibility.Visible;
        }
        private void btnClose1_Click(object sender, RoutedEventArgs e)
        {
            GrdType.Visibility = Visibility.Collapsed;
        }
        private void VariableNameChoosen_Click(object sender, RoutedEventArgs e)
        {
            if (CmbValueProp.SelectedIndex == -1)
                CmbValueProp.ItemsSource = (cmbDeviceSOADate.SelectedItem as Device).Capabilities.Properties;

            //TxbValue.Text = (cmbDeviceSOADate.SelectedItem as Device).Capabilities.Properties.FirstOrDefault().PropertyName + "'s Value would be:";
            GrdType.Visibility = Visibility.Collapsed;
            GrdTypeValue.Visibility = Visibility.Visible;
        }
        private void btnClose2_Click(object sender, RoutedEventArgs e)
        {
            GrdTypeValue.Visibility = Visibility.Collapsed;
        }
        private void VariableValueChoosen_Click(object sender, RoutedEventArgs e)
        {
            WorkflowAction wa = new WorkflowAction();
            wa.ActionName = TxtWorkflowActionName.Text;
            wa.ObjectName = cmbDeviceSOADate.Text;
            wa.VariableName = CmbValueProp.Text;
            wa.VariableValue = cmbDeviceSOADateValue.Text;

            if (VariableValueChoosen.Content.ToString().ToUpper() == "done".ToUpper())
            {
                if (WorkflowActions.Count <= 0)
                    wa.id = 1;
                else
                    wa.id = WorkflowActions.Max(x => x.id) + 1;
            }
            else
            {
                wa.id = UpdatedWorkflowActionID;
                WorkflowActions.Remove(WorkflowActions.Where(x => x.id == UpdatedWorkflowActionID).FirstOrDefault());
            }

            WorkflowActions.Add(wa);
            AMAActionableObjects.FixedWorkflowActionCollection = WorkflowActions;

            GrdTypeValue.Visibility = Visibility.Collapsed;
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
                        OnComponentClick(selectedItem as WorkflowAction);
                        break;
                    }
                    elem = (Visual)VisualTreeHelper.GetParent(elem);
                }
            }
        }
        private void OnComponentClick(WorkflowAction wa)
        {
            UpdatedWorkflowActionID = wa.id;
            TxtWorkflowActionName.Text = wa.ActionName;
            cmbDeviceSOADate.Text = wa.ObjectName;
            cmbDeviceSOADateValue.Text = wa.VariableValue;
            CmbValueProp.ItemsSource = (cmbDeviceSOADate.SelectedItem as Device).Capabilities.Properties;
            CmbValueProp.Text = wa.VariableName;
            GrdType.Visibility = Visibility.Visible;
            VariableValueChoosen.Content = "Save!!";
        }
    }
}
