using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enumerable = System.Linq.Enumerable;

namespace Iviz.MsgsGen
{
    public static class ActionGenerator
    {
        public static ClassInfo[]? GenerateFor(string package, string path)
        {
            string[] lines = File.ReadAllLines(path);
            string actionName = Path.GetFileNameWithoutExtension(path);
            List<IElement> elements = MsgParser.ParseFile(lines, actionName);

            List<int> separatorIndices = new List<int>();
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Type == ElementType.ServiceSeparator)
                {
                    separatorIndices.Add(i);
                }
            }

            if (separatorIndices.Count != 2)
            {
                Console.WriteLine("EE Action for file '" + path + "' does not have the expected two --- separators");
                return null;
            }

            List<IElement> goalElements = elements.GetRange(0, separatorIndices[0]);
            List<IElement> resultElements =
                elements.GetRange(separatorIndices[0] + 1, separatorIndices[1] - separatorIndices[0] - 1);
            List<IElement> feedbackElements =
                elements.GetRange(separatorIndices[1] + 1, elements.Count - separatorIndices[1] - 1);

            ClassInfo goalInfo = new ClassInfo(package, $"{actionName}Goal", goalElements, actionName,
                ActionMessageType.Goal);

            ClassInfo resultInfo = new ClassInfo(package, $"{actionName}Result", resultElements, actionName,
                ActionMessageType.Result);

            ClassInfo feedbackInfo = new ClassInfo(package, $"{actionName}Feedback", feedbackElements, actionName,
                ActionMessageType.Feedback);

            IElement[] actionGoalElements =
            {
                new VariableElement("", "Header", "header", serializeAsProperty: true),
                new VariableElement("", "actionlib_msgs/GoalID", "goal_id", serializeAsProperty: true),
                new VariableElement("", $"{actionName}Goal", "goal", null, goalInfo, serializeAsProperty: true),
            };
            ClassInfo actionGoalInfo = new ClassInfo(package, $"{actionName}ActionGoal", actionGoalElements, actionName,
                ActionMessageType.ActionGoal);

            IElement[] actionResultElements =
            {
                new VariableElement("", "Header", "header", serializeAsProperty: true),
                new VariableElement("", "actionlib_msgs/GoalStatus", "status", serializeAsProperty: true),
                new VariableElement("", $"{actionName}Result", "result", null, resultInfo, serializeAsProperty: true),
            };
            ClassInfo actionResultInfo = new ClassInfo(package, $"{actionName}ActionResult", actionResultElements,
                actionName, ActionMessageType.ActionResult);

            IElement[] actionFeedbackElements =
            {
                new VariableElement("", "Header", "header", serializeAsProperty: true),
                new VariableElement("", "actionlib_msgs/GoalStatus", "status", serializeAsProperty: true),
                new VariableElement("", $"{actionName}Feedback", "feedback", null, feedbackInfo,
                    serializeAsProperty: true),
            };
            ClassInfo actionFeedbackInfo = new ClassInfo(package, $"{actionName}ActionFeedback", actionFeedbackElements,
                actionName, ActionMessageType.ActionFeedback);

            IElement[] actionElements =
            {
                new VariableElement("", $"{actionName}ActionGoal", "action_goal", serializeAsProperty: true),
                new VariableElement("", $"{actionName}ActionResult", "action_result", serializeAsProperty: true),
                new VariableElement("", $"{actionName}ActionFeedback", "action_feedback", serializeAsProperty: true),
            };
            ClassInfo actionInfo = new ClassInfo(package, $"{actionName}Action", actionElements, actionName,
                ActionMessageType.Action);

            return new[]
            {
                goalInfo, feedbackInfo, resultInfo,
                actionGoalInfo, actionFeedbackInfo, actionResultInfo,
                actionInfo
            };
        }
    }
}