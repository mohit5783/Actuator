using Actuator.FeatureViews;
using Actuator.Model.ActionableObjectModelCollection;
using Actuator.Model.ActionableObjectsModel;
using Actuator.Model.ActionModel;
using Actuator.Model.ConditionalActionModel;
using Actuator.Model.ConditionModel;
using Actuator.Model.DeviceModel;
using Actuator.Model.Template;
using Actuator.Template;
using ComponentManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
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

namespace Actuator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region AllProperties
        public AOMDateCollection AOMDates { get; set; }
        public AOMTimeCollection AOMTimes { get; set; }
        public AOMMessageBoxCollection AOMMessageBoxes { get; set; }
        public AOMMessageCollection AOMMessages { get; set; }
        public AOMEmailCollection AOMEmails { get; set; }
        public clsDesignCollection designCollection { get; set; }
        public DeviceCollection deviceCollection { get; set; }
        public ConditionsCollection conditionCollection { get; set; }
        public FeatureCollection Features { get; set; }
        public Feature SelectedFeature { get; set; }
        public WorkflowActionCollection WorkflowActions { get; set; }
        public AllConditionsActions objAllCondAction { get; set; }
        public FunctionParameterCollection fpc { get; set; }
        public FileSystemWatcher watcher { get; set; }
        public ClsComponentManager cm { get; set; }
        public ClsComponent cmp { get; set; }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            InitAllProp();
            LoadMenus();
            DataContext = this;
        }
        private void InitAllProp()
        {
            AOMDates = new AOMDateCollection();
            AOMTimes = new AOMTimeCollection();
            AOMMessageBoxes = new AOMMessageBoxCollection();
            AOMMessages = new AOMMessageCollection();
            AOMEmails = new AOMEmailCollection();

            fpc = new FunctionParameterCollection();
            designCollection = new clsDesignCollection();

            deviceCollection = new DeviceCollection();
            conditionCollection = new ConditionsCollection();
            WorkflowActions = new WorkflowActionCollection();
            objAllCondAction = new AllConditionsActions();

            watcher = new FileSystemWatcher();

            cm = new ClsComponentManager();
            cmp = new ClsComponent();

            loadAllDevices();
            loadAllCondition();

            SelectedFeature = new Feature();

            LoadallAOMDates();
            LoadAllAOMTimes();
            LoadAllAOMMessageBoxes();
            LoadAllAOMMessages();
            LoadAllDesigns();
            LoadAllEmails();
            LoadAllWorkflowActions();
            LoadAllConditionalActions();

            watcher.Path = System.IO.Path.GetDirectoryName(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "qweasd.txt";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            cm.AddComponent("ACTUATOR", typeofComponent.ACT_Actuator, StatusofComponent.Active, GetLocalIPAddress());

            cmp.ComponentID = cm.CurrentObjComponentID;
            cmp.ComponentIP = GetLocalIPAddress();
            cmp.ComponentName = "ACTUATOR";
            cmp.ComponentStatus = StatusofComponent.Active;
            cmp.ComponentType = typeofComponent.ACT_Actuator;
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!"); //"Local IP Address Not Found!"
        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;
                Dispatcher.Invoke(() =>
                {
                    string barcodevalue = File.ReadAllText(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\qweasd.txt");
                    XElement xe = new XElement("SOAActions");
                    foreach (ConditionActionCollection cac in AMAActionableObjects.FixedAllConditionsActions)
                    {
                        foreach (ConditionAction ca in cac)
                        {
                            if (ca.ObjType == "Condition")
                            {
                                Model.ConditionModel.Condition cond = new Model.ConditionModel.Condition();
                                cond = (Model.ConditionModel.Condition)ca.CondAct;
                                if (cond.FirstOperand.ToLower().Contains("barcode".ToLower()))
                                {
                                    if (cond.SecondOperand == barcodevalue)
                                    {
                                        WorkflowAction doTask = (WorkflowAction)((ConditionAction)cac.Where(x => x.ParentID == ca.id).FirstOrDefault()).CondAct;

                                        if (doTask.VariableName.ToUpper() == "GMAIL".ToUpper())
                                        {
                                            xe.Add(new XElement("AOMEmail",
                                                new XAttribute("ID", doTask.VariableValue)));
                                        }
                                        else
                                        {
                                            xe.Add(new XElement("Twitter",
                                                new XAttribute("ID", doTask.VariableValue)));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    xe.Save(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\SOAAction.xml");
                });
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
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
        private void LoadAllConditionalActions()
        {
            if (File.Exists("ConditionActions.xml"))
            {
                ConditionAction ca = new ConditionAction();
                List<ConditionAction> lca = new List<ConditionAction>();
                XElement root = XElement.Load("ConditionActions.xml");
                root.Elements("ConditionActions").All<XElement>(xe2 =>
                {
                    xe2.Elements("ConditionAction").All<XElement>(xe =>
                    {
                        lca.Add(new ConditionAction()
                        {
                            id = Convert.ToInt32(xe.Attribute("id").Value),
                            ObjType = xe.Attribute("Type").Value,
                            ParentID = Convert.ToInt32(xe.Attribute("Parent").Value),

                            CondAct = (xe.Attribute("Type").Value == "Condition" ? (object)AMAActionableObjects.FixedConditionCollection.Where(x => x.id == Convert.ToInt32(xe.Attribute("ConditionAction").Value)).FirstOrDefault() : (object)AMAActionableObjects.FixedWorkflowActionCollection.Where(x => x.id == Convert.ToInt32(xe.Attribute("ConditionAction").Value)).FirstOrDefault())
                        });
                        return true;
                    });
                    ConditionActionCollection cac = new ConditionActionCollection();
                    cac.AddConditionAction(lca);
                    lca.Clear();
                    cac.ConditionActionID = Convert.ToInt32(xe2.Attribute("ID").Value);
                    objAllCondAction.AddConditionActionCollection(cac);
                    return true;
                });
                AMAActionableObjects.FixedAllConditionsActions = objAllCondAction;
            }
        }
        private void loadAllCondition()
        {
            if (File.Exists("Conditions.xml"))
            {
                XElement root = XElement.Load("Conditions.xml");
                root.Elements("Conditions").Elements("Condition").All<XElement>(xe =>
                {
                    conditionCollection.AddCondition(Convert.ToInt32(xe.Attribute("id").Value),
                            xe.Attribute("Description").Value,
                            xe.Attribute("OperandType").Value,
                            xe.Attribute("FirstOperand").Value,
                            xe.Attribute("SecondOperand").Value,
                            xe.Attribute("Operator").Value);
                    return true;
                });
            }
            AMAActionableObjects.FixedConditionCollection = conditionCollection;
        }
        private void loadAllDevices()
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

                    deviceCollection.AddDevice(xeDevices.Attribute("id").Value,
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

                    deviceCollection.AddDevice(xeDevices.Attribute("id").Value,
                        xeDevices.Attribute("name").Value, devCap);
                    return true;
                });
            }
            AMAActionableObjects.FixedDeviceCollection = deviceCollection;
        }
        private void LoadAllAOMMessageBoxes()
        {
            if (File.Exists(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml"))
            {
                XElement root = XElement.Load(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml");
                root.Elements("AOMMessageBoxes").Elements("AOMMessageBox").All<XElement>(xe =>
                {
                    AOMMessageBoxes.AddAOMMessageBox(Convert.ToInt32(xe.Attribute("ID").Value),
                        xe.Attribute("MessageValue").Value,
                        TimeSpan.Parse(xe.Attribute("Duration").Value),
                        Convert.ToBoolean(xe.Attribute("TimeBased").Value));
                    return true;
                });
            }
            AMAActionableObjects.FixedMessageBoxCollection = AOMMessageBoxes;
        }
        private void LoadAllAOMMessages()
        {
            if (File.Exists(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml"))
            {
                XElement root = XElement.Load(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml");
                root.Elements("AOMMessages").Elements("AOMMessage").All<XElement>(xe =>
                {
                    AOMMessages.AddAOMMessage(Convert.ToInt32(xe.Attribute("ID").Value),
                        xe.Attribute("MessageValue").Value);
                    return true;
                });
            }
            AMAActionableObjects.FixedMessageCollection = AOMMessages;
        }
        private void LoadAllAOMTimes()
        {
            if (File.Exists(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml"))
            {
                XElement root = XElement.Load(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml");
                root.Elements("AOMTimes").Elements("AOMTime").All<XElement>(xe =>
                {
                    AOMTimes.AddAOMTime(Convert.ToInt32(xe.Attribute("ID").Value),
                        Convert.ToDateTime(xe.Attribute("TimeValue").Value),
                        xe.Attribute("TimeRepeatValue").Value.Split(',').ToList());
                    return true;
                });
            }
            AMAActionableObjects.FixedTimeCollection = AOMTimes;
        }
        private void LoadallAOMDates()
        {
            if (File.Exists(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml"))
            {
                XElement root = XElement.Load(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml");
                root.Elements("AOMDates").Elements("AOMDate").All<XElement>(xe =>
                {
                    AOMDates.AddAOMDate(Convert.ToInt32(xe.Attribute("ID").Value),
                        Convert.ToDateTime(xe.Attribute("DateValue").Value));
                    return true;
                });
            }
            AMAActionableObjects.FixedDateCollection = AOMDates;
        }
        private void LoadAllDesigns()
        {
            if (File.Exists("Design.xml"))
            {
                XElement root = XElement.Load("Design.xml");
                root.Elements("Designs").Elements("Design").All<XElement>(xe =>
                {
                    designCollection.AddDesign(new Guid(xe.Attribute("id").Value),
                            xe.Attribute("Description").Value);
                    return true;
                });
            }
            AMAActionableObjects.FixedDesignCollection = designCollection;
        }
        private void LoadAllEmails()
        {
            if (File.Exists(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml"))
            {
                XElement root = XElement.Load(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml");
                root.Elements("AOMEmails").Elements("AOMEmail").All<XElement>(xe =>
                {
                    MailMessage myMail = new MailMessage();
                    MailAddress from = new MailAddress(xe.Attribute("From").Value);
                    myMail.From = from;
                    myMail.To.Add(xe.Attribute("To").Value);
                    myMail.CC.Add(xe.Attribute("CC").Value);
                    myMail.Bcc.Add(xe.Attribute("Bcc").Value);
                    MailAddress replyto = new MailAddress(xe.Attribute("replyTo").Value);
                    myMail.ReplyToList.Add(replyto);
                    myMail.Subject = xe.Attribute("Subject").Value;
                    myMail.Body = xe.Element("Body").Value;
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    myMail.IsBodyHtml = true;
                    AOMEmails.AddAOMEmail(Convert.ToInt32(xe.Attribute("ID").Value),
                        myMail);
                    return true;
                });
            }
            AMAActionableObjects.FixedEmailCollection = AOMEmails;
        }
        private void LoadMenus()
        {
            Features = new FeatureCollection();
            if (File.Exists("Menu.xml"))
            {
                XElement root = XElement.Load("Menu.xml");
                root.Elements("Menu").All<XElement>(xe =>
                {
                    Features.AddFeature(xe.Attribute("Name").Value.ToUpper(),
                        xe.Attribute("Icon").Value,
                        xe.Attribute("ShortText").Value,
                        Convert.ToBoolean(xe.Attribute("HasChild").Value),
                        xe.Attribute("MenuType").Value);
                    return true;
                });
            }

        }
        private void LstAllFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFeature = (LstAllFeatures.SelectedItem as Feature);
            LoadRespectiveFeature(SelectedFeature);
        }
        private void LoadRespectiveFeature(Feature SelectedFeature)
        {
            DetailsPane.Children.Clear();
            if (SelectedFeature.Name.ToUpper() == "WORKFLOW".ToUpper())
            {
                Workflow wf = new Workflow();
                DetailsPane.Children.Add(wf);
            }
            else if (SelectedFeature.Name.ToUpper() == "Actionable Objects".ToUpper())
            {
                ActionableObjects ao = new ActionableObjects();
                DetailsPane.Children.Add(ao);
            }
            else if (SelectedFeature.Name.ToUpper() == "Sync Now".ToUpper())
            {
                MessageBox.Show("The syncing has been done.", "Syncing Done", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (SelectedFeature.Name.ToUpper() == "Save All".ToUpper())
            {
                //Call what u called on destructor
            }
            else if (SelectedFeature.Name.ToUpper() == "Templates".ToUpper())
            {
                ucDesignParent udp = new ucDesignParent();
                DetailsPane.Children.Add(udp);
            }
            else if (SelectedFeature.Name.ToUpper() == "Smart Services (SOA)".ToUpper())
            {
                SOAView sv = new SOAView();
                DetailsPane.Children.Add(sv);
            }
            else if (SelectedFeature.Name.ToUpper() == "Smart Soft Device (SSD)".ToUpper())
            {
                SSDsView sv = new SSDsView();
                DetailsPane.Children.Add(sv);
            }
            else if (SelectedFeature.Name.ToUpper() == "Devices".ToUpper())
            {
                DevicesView dv = new DevicesView();
                DetailsPane.Children.Add(dv);
            }
            else if (SelectedFeature.Name.ToUpper() == "Personalize".ToUpper())
            {
                MessageBox.Show("Personalize feature is Under Construction. This feature enables you to do changes in the display, colors, fonts and size.", "Under Construction", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (SelectedFeature.Name.ToUpper() == "Settings".ToUpper())
            {
                SettingView sv = new SettingView();
                DetailsPane.Children.Add(sv);
            }
            else if (SelectedFeature.Name.ToUpper() == "Exit".ToUpper())
            {
                Application.Current.Shutdown();
            }
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        ~MainWindow()
        {
            cm.DeleteComponent(cm.GetComponentByID(cm.CurrentObjComponentID));
            XElement xe = new XElement("ActionableObjects");
            xe.Add(new XElement("AOMDates",
                    from AllDates in AMAActionableObjects.FixedDateCollection
                    select new XElement("AOMDate",
                        new XAttribute("ID", AllDates.AOMDateID),
                        new XAttribute("DateValue", AllDates.DateValue))));
            xe.Add(new XElement("AOMTimes",
                    from AllTimes in AMAActionableObjects.FixedTimeCollection
                    select new XElement("AOMTime",
                        new XAttribute("ID", AllTimes.AOMTimeID),
                        new XAttribute("TimeValue", AllTimes.TimeValue),
                        new XAttribute("TimeRepeatValue", String.Join(",", AllTimes.TimeRepeatValues)))));
            xe.Add(new XElement("AOMMessageBoxes",
                    from AllMessageBoxes in AMAActionableObjects.FixedMessageBoxCollection
                    select new XElement("AOMMessageBox",
                        new XAttribute("ID", AllMessageBoxes.AOMMessageBoxID),
                        new XAttribute("MessageValue", AllMessageBoxes.MessageValue),
                        new XAttribute("Duration", AllMessageBoxes.MessageDuration.ToString()),
                        new XAttribute("TimeBased", AllMessageBoxes.TimeBased))));
            xe.Add(new XElement("AOMMessages",
                    from AllMessage in AMAActionableObjects.FixedMessageCollection
                    select new XElement("AOMMessage",
                        new XAttribute("ID", AllMessage.AOMMessageID),
                        new XAttribute("MessageValue", AllMessage.AOMMessageBody))));
            xe.Add(new XElement("AOMEmails",
                    from AllEmails in AMAActionableObjects.FixedEmailCollection
                    select new XElement("AOMEmail",
                        new XAttribute("ID", AllEmails.AOMEmailId),
                        new XAttribute("From", AllEmails.Email.From),
                        new XAttribute("To", AllEmails.Email.To),
                        new XAttribute("CC", AllEmails.Email.CC),
                        new XAttribute("Bcc", AllEmails.Email.Bcc),
                        new XAttribute("replyTo", AllEmails.Email.ReplyToList),
                        new XAttribute("Subject", AllEmails.Email.Subject),
                        new XElement("Body", AllEmails.Email.Body))));
            xe.Save(@"\\192.168.0.37\MY101-Product X\07-Product Line\CSL\ComponentManager\ActionableObjects.xml");

            XElement xe1 = new XElement("AllCondition");
            xe1.Add(new XElement("Conditions",
                    from AllCondition in AMAActionableObjects.FixedConditionCollection
                    select new XElement("Condition",
                        new XAttribute("id", AllCondition.id),
                        new XAttribute("Description", AllCondition.Description),
                        new XAttribute("OperandType", AllCondition.OperandType),
                        new XAttribute("FirstOperand", AllCondition.FirstOperand),
                        new XAttribute("SecondOperand", AllCondition.SecondOperand),
                        new XAttribute("Operator", AllCondition.Operator))));
            xe1.Save("Conditions.xml");

            XElement xe2 = new XElement("WorkflowActions");
            xe2.Add(from AllWokflowAction in AMAActionableObjects.FixedWorkflowActionCollection
                    select new XElement("WorkflowAction",
                        new XAttribute("id", AllWokflowAction.id),
                        new XAttribute("ActionName", AllWokflowAction.ActionName),
                        new XAttribute("ObjectName", AllWokflowAction.ObjectName),
                        new XAttribute("VarName", AllWokflowAction.VariableName),
                        new XAttribute("VarValue", AllWokflowAction.VariableValue)));
            xe2.Save("WorkflowActions.xml");

            XElement xeConditionActions = new XElement("AllConditionActions");
            xeConditionActions.Add(from allcond in AMAActionableObjects.FixedAllConditionsActions
                                   select new XElement("ConditionActions",
                                        new XAttribute("ID", allcond.ConditionActionID),//ConditionalActions.ConditionalActionsID
                                        from conditionsactions in allcond.Where(x => x.id == x.id)
                                        select new XElement("ConditionAction",
                                        new XAttribute("id", conditionsactions.id),
                                        new XAttribute("ConditionAction", conditionsactions.CondAct is Model.ConditionModel.Condition ? ((Model.ConditionModel.Condition)conditionsactions.CondAct).id.ToString() : ((WorkflowAction)conditionsactions.CondAct).id.ToString()),
                                        new XAttribute("Type", conditionsactions.CondAct is Model.ConditionModel.Condition ? "Condition" : "Action"),
                                        new XAttribute("Parent", conditionsactions.ParentID))));
            xeConditionActions.Save("ConditionActions.xml");
        }
    }
}
