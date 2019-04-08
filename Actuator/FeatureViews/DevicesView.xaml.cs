using Actuator.Model.DeviceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using ComponentManager;

namespace Actuator.FeatureViews
{
    /// <summary>
    /// Interaction logic for DevicesView.xaml
    /// </summary>
    public partial class DevicesView : UserControl
    {
        #region Initalize Devices
        public Device dev { get; set; }
        public DeviceCollection devcol { get; set; }
        public DeviceCapabilities devCap { get; set; }
        public DeviceEventCollection devEventColl { get; set; }
        public DeviceFunctionCollection devfuncColl { get; set; }
        public DevicePropertyCollection devPropColl { get; set; }
        public DeviceEvent devevent { get; set; }
        public DeviceFunction devFunc { get; set; }
        public DeviceProperty devProp { get; set; }
        public FunctionParameterCollection fpc { get; set; }

        #endregion
        public DevicesView()
        {
            InitializeComponent();
            initAllDevComp();
            LoadAllDevices();
            //ActionMenu.ItemsSource = typeof(ComponentManager.typeofComponent).GetFields().Select(f => (DescriptionAttribute)f.GetCustomAttribute(typeof(DescriptionAttribute))).Where(a => a != null).Select(x => x.Description); ;
            //ActionMenu.SelectedIndex = 0;
            DataContext = this;
        }

        private void initAllDevComp()
        {
            fpc = new FunctionParameterCollection();
            devCap = new DeviceCapabilities();

            dev = new Device();
            devcol = new DeviceCollection();
            devEventColl = new DeviceEventCollection();
            devfuncColl = new DeviceFunctionCollection();
            devPropColl = new DevicePropertyCollection();
            devevent = new DeviceEvent();
            devFunc = new DeviceFunction();
            devProp = new DeviceProperty();

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

                    devcol.AddDevice(xeDevices.Attribute("id").Value,
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

                    devcol.AddDevice(xeDevices.Attribute("id").Value,
                        xeDevices.Attribute("name").Value, devCap);
                    return true;
                });
            }
            AMAActionableObjects.FixedDeviceCollection = devcol;
        }
        private void ActionMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DevicesItemsCtrl.ItemsSource = null;
            //if (ActionMenu.SelectedItem.ToString().ToUpper().Contains("Driver".ToUpper()))
            //{
            //    DevicesItemsCtrl.ItemsSource = devcol;
            //}
            //else if(ActionMenu.SelectedItem.ToString().ToUpper().Contains("Smart Soft Device".ToUpper()))
            //{
            //    MessageBox.Show("Get List of SSDs please");
            //}
            //will
        }

    }
}
