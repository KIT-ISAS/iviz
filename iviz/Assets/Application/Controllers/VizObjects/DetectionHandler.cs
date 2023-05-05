#nullable enable

using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.VisionMsgs;

namespace Iviz.Controllers
{
    public class DetectionHandler : VizHandler, IHandles<DetectionBox>, IHandlesArray<DetectionBoxArray>
    {
        public override string Title => "Boundaries";

        public override string BriefDescription
        {
            get
            {
                string vizObjectsStr = vizObjects.Count switch
                {
                    0 => "<b>No detections</b>",
                    1 => "<b>1 detection</b>",
                    _ => $"<b>{vizObjects.Count.ToString()} detection</b>"
                };

                const string errorStr = "No errors";

                return $"{vizObjectsStr}\n{errorStr}";
            }
        }

        public void Handle(DetectionBoxArray msg)
        {
            foreach (var detection in msg.Boxes)
            {
                Handle(detection);
            }
        }

        public void Handle(DetectionBox msg)
        {
            switch ((ActionType)msg.Action)
            {
                case ActionType.Remove:
                    HandleRemove(msg.Id);
                    break;
                case ActionType.RemoveAll:
                    RemoveAll();
                    break;
                case ActionType.Add:
                    HandleAdd(msg);
                    break;
                default:
                    RosLogger.Error(
                        $"{ToString()}: Object '{msg.Id}' requested unknown action {msg.Action.ToString()}");
                    break;
            }
        }

        void HandleAdd(DetectionBox msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{this}: Cannot add dialog with empty id");
                return;
            }

            /*
            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                var dialogObject = (DialogObject)existingObject;
                if (msg.Lifetime.ToTimeSpan() < TimeSpan.Zero)
                {
                    dialogObject.MarkAsExpired();
                    return;
                }

                dialogObject.Dispose();
                vizObjects.Remove(msg.Id);
            }

            var vizObject = new DialogObject(feedback, msg, resourceKey, "Dialog." + (DialogType)msg.Type)
                { Interactable = Interactable, Visible = Visible };
            vizObjects[vizObject.id] = vizObject;
            */
        }
    }
}