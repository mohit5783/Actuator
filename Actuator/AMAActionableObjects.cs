using Actuator.Model.ActionableObjectModelCollection;
using Actuator.Model.ActionModel;
using Actuator.Model.ConditionalActionModel;
using Actuator.Model.ConditionModel;
using Actuator.Model.DeviceModel;
using Actuator.Model.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actuator
{
    public static class AMAActionableObjects
    {
        public static AllConditionsActions FixedAllConditionsActions { get; set; }
        public static AOMDateCollection FixedDateCollection { get; set; }
        public static AOMTimeCollection FixedTimeCollection { get; set; }
        public static AOMMessageBoxCollection FixedMessageBoxCollection { get; set; }
        public static AOMMessageCollection FixedMessageCollection { get; set; }
        public static AOMEmailCollection FixedEmailCollection { get; set; }
        public static DeviceCollection FixedDeviceCollection { get; set; }
        public static ConditionsCollection FixedConditionCollection { get; set; }
        public static WorkflowActionCollection FixedWorkflowActionCollection { get; set; }
        //Template
        public static clsDesignCollection FixedDesignCollection { get; set; }
        //Template
    }
}
