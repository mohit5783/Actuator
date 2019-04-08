using Actuator.Model.ActionModel;
using Actuator.Model.ConditionalActionModel;
using Actuator.Model.ConditionModel;
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
    /// Interaction logic for ConditionalActionView.xaml
    /// </summary>
    public partial class ConditionalActionView : UserControl
    {
        public WorkflowActionCollection AllWorkflowActions { get; set; }
        public ConditionsCollection AllConditions { get; set; }
        public string LastStatement { get; set; }
        public int LastDepth { get; set; }
        public List<ConditionAction> ListConditionAction { get; set; }
        public int LastConditionID { get; set; }
        public AllConditionsActions ObjAllConditionsActions { get; set; }

        public ConditionalActionView()
        {
            InitializeComponent();
            AllWorkflowActions = new WorkflowActionCollection();
            AllWorkflowActions = AMAActionableObjects.FixedWorkflowActionCollection;
            AllConditions = new ConditionsCollection();
            AllConditions = AMAActionableObjects.FixedConditionCollection;
            LastDepth = 0;
            ListConditionAction = new List<ConditionAction>();
            LastConditionID = 0;
            ObjAllConditionsActions = new AllConditionsActions();
            ObjAllConditionsActions = AMAActionableObjects.FixedAllConditionsActions;

            DataContext = this;
        }

        private void AddConditionalAction_Click(object sender, RoutedEventArgs e)
        {
            GrdConditionalAction.Visibility = Visibility.Visible;
        }

        private void AddCondition_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCondition.SelectedIndex != -1)
            {
                Model.ConditionModel.Condition conditionStatement = cmbCondition.SelectedItem as Model.ConditionModel.Condition;

                ConditionAction ca = new ConditionAction();
                ca.id = ListConditionAction.Count + 1;
                ca.CondAct = conditionStatement;
                ca.ParentID = LastConditionID;
                LastConditionID = ca.id;

                ListConditionAction.Add(ca);

                WriteCode(conditionStatement.Description, "Condition");
            }


            GrdAddCondition.Visibility = Visibility.Collapsed;
            cmbCondition.SelectedIndex = -1;
        }

        private void WriteCode(string Statement, string ComingFrom)
        {
            string Code = TxtCode.Text;
            if (ComingFrom == "Condition")
            {
                if (Code == "")
                    TxtCode.Text = "IF " + Statement + " THEN\r\n-\r\nENDIF";
                else
                {
                    string newinsertablecode = new String(' ', 2 * LastDepth) + "IF " + Statement + " THEN\r\n-\r\n" + new String(' ', 2 * LastDepth) + "ENDIF";
                    Code = Code.Replace("-", newinsertablecode);
                    TxtCode.Text = Code;
                }
                LastStatement = "Condition";
            }
            else
            {
                string newinsertablecode = new String(' ', 2 * LastDepth) + Statement + "\r\n-";
                Code = Code.Replace("-", newinsertablecode);
                TxtCode.Text = Code;
                LastStatement = "Action";
                LastDepth--;
            }
            LastDepth++;
        }
        private void ChooseCondition_Click(object sender, RoutedEventArgs e)
        {
            GrdAddCondition.Visibility = Visibility.Visible;
        }

        private void ChooseAction_Click(object sender, RoutedEventArgs e)
        {
            GrdAddAction.Visibility = Visibility.Visible;
        }

        private void btnClose2_Click(object sender, RoutedEventArgs e)
        {
            GrdAddAction.Visibility = Visibility.Collapsed;
        }

        private void AddAction_Click(object sender, RoutedEventArgs e)
        {
            if (cmbWorkflowActions.SelectedIndex != -1)
            {
                WorkflowAction ActStat = cmbWorkflowActions.SelectedItem as WorkflowAction;

                ConditionAction ca = new ConditionAction();
                ca.id = ListConditionAction.Count + 1;
                ca.CondAct = ActStat;
                ca.ParentID = LastConditionID;

                ListConditionAction.Add(ca);

                string ActionStatement = ActStat.ActionName;
                WriteCode(ActionStatement, "Action");
            }
            GrdAddAction.Visibility = Visibility.Collapsed;
            cmbWorkflowActions.SelectedIndex = -1;
        }

        private void btnClose1_Click(object sender, RoutedEventArgs e)
        {
            GrdAddCondition.Visibility = Visibility.Collapsed;
        }

        private void SaveConditionalAction_Click(object sender, RoutedEventArgs e)
        {
            ConditionActionCollection ConditionsActions = new ConditionActionCollection();
            ConditionsActions.AddConditionAction(ListConditionAction);
            ConditionsActions.ConditionActionID = ObjAllConditionsActions.Count + 1;
            ObjAllConditionsActions.AddConditionActionCollection(ConditionsActions);

            AMAActionableObjects.FixedAllConditionsActions = ObjAllConditionsActions;

            ListConditionAction.Clear();
            GrdConditionalAction.Visibility = Visibility.Collapsed;
            TxtCode.Text = "";
        }
    }
}
