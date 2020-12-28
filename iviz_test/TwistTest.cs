using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Roslib;
using Iviz.Roslib.MarkerHelper;

namespace iviz_test
{
    public class TwistTest
    {
        public static async Task TwistMarkerTest()
        {
            Uri masterUri = new Uri("http://localhost:11311");
            Uri callerUri = new Uri("http://localhost:7600");
            await using RosClient client = new RosClient(masterUri, callerUri: callerUri);
            await using RosChannelWriter<InteractiveMarkerInit> init = new RosChannelWriter<InteractiveMarkerInit>
                {LatchingEnabled = true};
            await using RosChannelWriter<InteractiveMarkerUpdate> update =
                new RosChannelWriter<InteractiveMarkerUpdate> {LatchingEnabled = true};
            await using RosChannelReader<InteractiveMarkerFeedback> feedback =
                new RosChannelReader<InteractiveMarkerFeedback>();
            await using RosMarkerHelper helper = new RosMarkerHelper();

            await init.StartAsync(client, "markers/update_full");
            await update.StartAsync(client, "markers/update");
            await feedback.StartAsync(client, "markers/feedback");
            await helper.StartAsync(client);

            await client.CallServiceAsync("iviz_osxeditor/add_module_from_topic",
                new AddModuleFromTopic {Request = {Topic = update.Topic}});
            await client.CallServiceAsync("iviz_osxeditor/add_module_from_topic",
                new AddModuleFromTopic {Request = {Topic = helper.Topic}});

            var sphereMarker = RosMarkerHelper.CreateSphere(color: ColorRGBA.Red);
            var sphereControl = RosInteractiveMarkerHelper.CreateControl("",
                Quaternion.AngleAxis(Math.PI / 2, Vector3.UnitY),
                RosInteractionMode.MovePlane, sphereMarker);

            var cubeMarker = RosMarkerHelper.CreateCube(scale: (4, 1, 1), color: ColorRGBA.Blue);
            var cubeControl = RosInteractiveMarkerHelper.CreateControl("",
                Quaternion.AngleAxis(Math.PI / 2, Vector3.UnitY),
                RosInteractionMode.MoveRotate, cubeMarker);

            Pose twistBasePose = Pose.Identity.WithPosition((0, 0, 1));
            var twistMarkerMsg =
                RosInteractiveMarkerHelper.Create("twist", twistBasePose,
                    controls: sphereControl, scale: 0.5f);
            var twistResetBackUpdateMsg = RosInteractiveMarkerHelper.CreatePoseUpdate(("twist", twistBasePose));

            Vector3 centerPos = (0, 0, 0);
            Quaternion centerRotation = Quaternion.Identity;

            var topLeftMarkerMsg = RosInteractiveMarkerHelper.Create("topLeft",
                Pose.Identity.WithPosition((-1, 1, 0) + centerPos),
                controls: sphereControl, scale: 0.5f);
            var topRightMarkerMsg = RosInteractiveMarkerHelper.Create("topRight",
                Pose.Identity.WithPosition((1, 1, 0) + centerPos),
                controls: sphereControl, scale: 0.5f);
            var bottomRightMarkerMsg = RosInteractiveMarkerHelper.Create("bottomRight",
                Pose.Identity.WithPosition((1, -1, 0) + centerPos),
                controls: sphereControl, scale: 0.5f);
            var bottomLeftMarkerMsg = RosInteractiveMarkerHelper.Create("bottomLeft",
                Pose.Identity.WithPosition((-1, -1, 0) + centerPos),
                controls: sphereControl, scale: 0.5f);
            var centralMarkerMsg = RosInteractiveMarkerHelper.Create("center",
                Pose.Identity.WithPosition(centerPos),
                controls: cubeControl, scale: 0.5f);


            var initMsg = RosInteractiveMarkerHelper.CreateInit(twistMarkerMsg, centralMarkerMsg,
                topLeftMarkerMsg, topRightMarkerMsg, bottomRightMarkerMsg, bottomLeftMarkerMsg);
            await init.WriteAsync(initMsg, RosPublishPolicy.WaitUntilSent);
            var firstUpdateMsg = RosInteractiveMarkerHelper.CreateMarkerUpdate(twistMarkerMsg, centralMarkerMsg,
                topLeftMarkerMsg, topRightMarkerMsg, bottomRightMarkerMsg, bottomLeftMarkerMsg);
            await update.WriteAsync(firstUpdateMsg, RosPublishPolicy.WaitUntilSent);


            var twistPoints = new Point[] {(0, 0, 1), (0, 0, 1)};
            var digPoints = new[]
            {
                topLeftMarkerMsg.Pose.Position, topRightMarkerMsg.Pose.Position,
                bottomRightMarkerMsg.Pose.Position, bottomLeftMarkerMsg.Pose.Position,
                topLeftMarkerMsg.Pose.Position
            };

            helper.CreateLines(twistPoints, color: ColorRGBA.Red, scale: 0.05f, frameId: "map");
            helper.CreateLineStrip(digPoints, color: ColorRGBA.Red, scale: 0.05f, frameId: "map");
            await helper.ApplyChangesAsync();

            await foreach (var feedbackMsg in feedback.ReadAllAsync(default))
            {
                switch ((RosEventType) feedbackMsg.EventType)
                {
                    case RosEventType.PoseUpdate when feedbackMsg.MarkerName == "center":
                        Console.WriteLine(feedbackMsg.Pose.ToJsonString());
                        Vector3 deltaPosition = feedbackMsg.Pose.Position - centerPos;

                        Quaternion deltaRotation = (feedbackMsg.Pose.Orientation * centerRotation.Inverse)
                            .WithX(0).WithY(0).Normalized;
                        Transform t = Transform.RotateAround(deltaRotation, centerPos).Translate(deltaPosition);

                        centerPos = feedbackMsg.Pose.Position;
                        centerRotation = feedbackMsg.Pose.Orientation;

                        digPoints[0] = t * digPoints[0];
                        digPoints[1] = t * digPoints[1];
                        digPoints[2] = t * digPoints[2];
                        digPoints[3] = t * digPoints[3];
                        digPoints[4] = t * digPoints[4];

                        var poseUpdateMsg = RosInteractiveMarkerHelper.CreatePoseUpdate(
                            ("topLeft", Pose.Identity.WithPosition(digPoints[0])),
                            ("topRight", Pose.Identity.WithPosition(digPoints[1])),
                            ("bottomRight", Pose.Identity.WithPosition(digPoints[2])),
                            ("bottomLeft", Pose.Identity.WithPosition(digPoints[3]))
                        );

                        await helper.ApplyChangesAsync();
                        await update.WriteAsync(poseUpdateMsg);
                        break;
                    case RosEventType.PoseUpdate when feedbackMsg.MarkerName == "topLeft":
                        digPoints[0] = feedbackMsg.Pose.Position;
                        digPoints[4] = feedbackMsg.Pose.Position;
                        await helper.ApplyChangesAsync();
                        break;
                    case RosEventType.PoseUpdate when feedbackMsg.MarkerName == "topRight":
                        digPoints[1] = feedbackMsg.Pose.Position;
                        await helper.ApplyChangesAsync();
                        break;
                    case RosEventType.PoseUpdate when feedbackMsg.MarkerName == "bottomRight":
                        digPoints[2] = feedbackMsg.Pose.Position;
                        await helper.ApplyChangesAsync();
                        break;
                    case RosEventType.PoseUpdate when feedbackMsg.MarkerName == "bottomLeft":
                        digPoints[3] = feedbackMsg.Pose.Position;
                        await helper.ApplyChangesAsync();
                        break;
                    case RosEventType.PoseUpdate when feedbackMsg.MarkerName == "twist":
                        twistPoints[1] = feedbackMsg.Pose.Position;
                        await helper.ApplyChangesAsync();
                        break;
                    case RosEventType.MouseUp when feedbackMsg.MarkerName == "twist":
                        twistPoints[1] = (0, 0, 1);
                        await helper.ApplyChangesAsync();
                        update.Write(twistResetBackUpdateMsg);
                        break;
                }
            }

            await Task.Delay(-1);
        }
    }
}